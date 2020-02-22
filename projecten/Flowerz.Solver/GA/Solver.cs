using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Flowerz.Solver.GA.Crossover;
using Flowerz.Solver.GA.Mutation;
using Flowerz.SolverModel;
using Board = Flowerz.SolverModel.Board;

namespace Flowerz.Solver.GA {
    public delegate void MovelistCreatedHandler( MovelistCreatedEventArgs args );
    public delegate void GenerationCreatedHandler( GenerationCreatedEventArgs args );
    public delegate void CrossoverSelectedHandler( CrossoverEventArgs args );
    public delegate void SolverCreatedHandler( SolverCreatedEventArgs args );

    public class Solver : IGASolver {
        public GeneticAlgorithmOptions GeneticAlgorithmOptions { get; set; }

        public MoveList GetCurrentBest {
            get {
                return null;
            }
        }

        public MoveList LastGeneratedSolution { get; private set; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        private PiecesQueue PiecesQueue { get; set; }
        private Board Board { get; set; }

        private readonly List<MoveList> _currentGeneration;
        private readonly List<MoveList> _nextGeneration;

        public event MovelistCreatedHandler MovelistCreated;
        public event GenerationCreatedHandler GenerationCreated;
        public event CrossoverSelectedHandler CrossoverSelected;
        public event SolverCreatedHandler SolverCreated;

        public Solver() {
            _currentGeneration = new List<MoveList>();
            _nextGeneration = new List<MoveList>();
        }

        public MoveList Solve( Board board, PiecesQueue queue ) {
            if( queue == null ) {
                throw new ArgumentNullException( "queue" );
            }
            if( board == null ) {
                throw new ArgumentNullException( "board" );
            }
            
            PiecesQueue = queue;
            Board = board;

            return Solve();
        }

        public event EventHandler<SolutionCreatedEventArgs> SolutionCreated;

        private MoveList Solve() {
            var randomizer = RandomizerFactory.CreateRandomizer();
            CreateInitialPopulation( randomizer );

            var numGenerationsCreated = 0;
            foreach( var t in _currentGeneration.Where( t => t.Score == Board.MaximumNumberOfFreeSpaces ) ) {
                //No need to loop!
                return t;
            }

            while( numGenerationsCreated <= GeneticAlgorithmOptions.NumberOfGenerationsToCreate ) {
                while( _nextGeneration.Count < GeneticAlgorithmOptions.PopulationSize ) {
                    var operationToPerform = GeneticOperatorSelector.DetermineGeneticOperation( GeneticAlgorithmOptions.CrossOverChance, GeneticAlgorithmOptions.MutationChance, randomizer );
                    if( operationToPerform == GeneticOperation.Crossover ) {
                        PerformCrossover( randomizer );
                    }

                    if( operationToPerform == GeneticOperation.Mutation ) {
                        PerformMutation( randomizer );
                    }

                    if( operationToPerform == GeneticOperation.Reproduction ) {
                        PerformReproduction( randomizer );
                    }
                }

                //Check if child has maximum score
                var childIndexWithMaximumScore = new int?();
                for( var i = 0; i < _nextGeneration.Count; i++ ) {
                    if( _nextGeneration[ i ].Score == Board.MaximumNumberOfFreeSpaces ) {
                        childIndexWithMaximumScore = i;
                    }
                }

                if( childIndexWithMaximumScore.HasValue ) {
                    _currentGeneration.Clear();
                    foreach( var child in _nextGeneration ) {
                        _currentGeneration.Add( child );
                    }
                    break;
                }

                //Highest score parent generation
                var highestScorePreviousGeneration = _currentGeneration.Select( parent => parent.Score ).Concat( new[] { Int32.MinValue } ).Max();

                //Highest score next genetation
                var highestScoreNextGeneration = Int32.MinValue;
                var indexHighestScoreNextGen = -1;
                for( var i = 0; i < _nextGeneration.Count; i++ ) {
                    var child = _nextGeneration[ i ];
                    if( child.Score > highestScoreNextGeneration ) {
                        highestScoreNextGeneration = child.Score;
                        indexHighestScoreNextGen = i;
                    }
                }

                var moves = _nextGeneration[ indexHighestScoreNextGen ].ToString();

                if( highestScoreNextGeneration < highestScorePreviousGeneration ) {
                    //Preserving parents that are better than their children
                    foreach( var parent in _currentGeneration ) {
                        if( parent.Score == highestScorePreviousGeneration ) {
                            _nextGeneration.Add( parent );
                        }
                    }
                }

                _currentGeneration.Clear();
                foreach( var child in _nextGeneration ) {
                    _currentGeneration.Add( child );
                }

                _nextGeneration.Clear();
                numGenerationsCreated++;
                if( GenerationCreated != null ) {
                    var generationArgs = new GenerationCreatedEventArgs {
                        GenerationNumber = numGenerationsCreated,
                        HighestScoreThisGeneration = highestScoreNextGeneration,
                        HighestPreviousScore = highestScorePreviousGeneration,
                        HighestScoringMoves = moves,
                        MaximumNumberOfFreeSpaces = Board.MaximumNumberOfFreeSpaces
                    };
                    GenerationCreated( generationArgs );
                }
            }

            var indexWithHighestScore = -1;
            var highestScore = Int32.MinValue;
            for( var i = 0; i < _currentGeneration.Count; i++ ) {
                var scoreCurrentMovelist = _currentGeneration[ i ].Score;
                if( scoreCurrentMovelist > highestScore ) {
                    highestScore = scoreCurrentMovelist;
                    indexWithHighestScore = i;
                }
            }

            return _currentGeneration[ indexWithHighestScore ];
        }

        public void ImportMovelist( String ml ) {
            var movelist = MoveList.ParseMoveList( ml );
            _currentGeneration.Add( movelist );
        }

        #region Perform crossover
        private void PerformCrossover( IRandomizer randomizer ) {
            var x = new ChromosomeSelector( _currentGeneration );
            var parent1 = x.Get( randomizer );
            var parent2 = x.Get( randomizer );

            MoveList child1;
            MoveList child2;
            if( parent1.CompareTo( parent2 ) == 0 ) {

                child1 = Perform2ROG( randomizer );
                child2 = Perform2ROG( randomizer );
                _currentGeneration.Add( child1 );
                _currentGeneration.Add( child2 );
            } else {
                if( CrossoverSelected != null ) {
                    var coEventArgs = new CrossoverEventArgs {
                        MovesParent1 = parent1.ToString(),
                        MovesParent2 = parent2.ToString()
                    };
                    CrossoverSelected( coEventArgs );
                }

                //Get crossover strategy
                var strategy = CrossoverFactory.GetCrossoverStrategy( randomizer );

                //Do crossover
                var children = strategy.DoCrossover( parent1, parent2 );

                child1 = children.Item1;
                child2 = children.Item2;

                CheckAndAddValidChild( child1, true, false );
                CheckAndAddValidChild( child2, true, false );
            }
        }

        private MoveList Perform2ROG( IRandomizer r ) {
            MoveList createdList;
            do {
                createdList = CreateRandomMovelist( r );
            } while( createdList == null || createdList.Score < 1 );
            return createdList;
        }
        #endregion

        #region Perform reproduction
        private void PerformReproduction( IRandomizer randomizer ) {
            var x = new ChromosomeSelector( _currentGeneration );
            var parent1 = x.Get( randomizer );
            _nextGeneration.Add( parent1 );
            if( MovelistCreated != null ) {
                var args = new MovelistCreatedEventArgs {
                    CreatedThroughMutation = false,
                    CreatedThroughCrossover = false,
                    CreatedThroughReproduction = true,
                    //NameSolution = parent1.Name,
                    Score = parent1.Score,
                    Moves = parent1.ToString()
                };
                MovelistCreated( args );
            }
        }
        #endregion

        #region Perform mutation
        private void PerformMutation( IRandomizer randomizer ) {
            var x = new ChromosomeSelector( _currentGeneration );
            var parent1 = x.Get( randomizer );
            var strategy = MutationFactory.GetMutationStrategy( randomizer );
            var mutant = strategy.Mutate( parent1 );
            CheckAndAddValidChild( mutant, false, true );
        }
        #endregion

        private void CheckAndAddValidChild( MoveList child, Boolean createdThroughCrossover, Boolean createdThroughMutation ) {
            var childValid = Board.ApplyMoveList( child );
            if( childValid ) {
                //Only valid solutions are applicable.
                var numEmptySpaces = Board.GetNumberOfFreeSpaces();
                child.Score = numEmptySpaces;
                if( child.Score > 0 && _nextGeneration.Count < GeneticAlgorithmOptions.PopulationSize ) {
                    _nextGeneration.Add( child );

                    if( MovelistCreated != null ) {
                        var args = new MovelistCreatedEventArgs {
                            CreatedThroughMutation = createdThroughMutation,
                            CreatedThroughCrossover = createdThroughCrossover,
                            CreatedThroughReproduction = false,
                            //NameSolution = child.Name,
                            Score = child.Score,
                            Moves = child.ToString()
                        };
                        MovelistCreated( args );
                    }
                }
            }
            Board.Reset();
        }

        #region Create movelist
        private MoveList CreateRandomMovelist( IRandomizer random ) {
            var list1 = new MoveList();
            var validMovelist = true;
            for( var i = 0; i < PiecesQueue.Count; i++ ) {
                var piece = PiecesQueue[ i ];
                if( piece.Type == PieceType.Flower ) {
                    var coordinate = Board.GetRandomEmptyCoordinate( random.GetRandom );
                    if( !coordinate.Equals( Coordinate.InvalidCoordinate ) ) {
                        var move = new Move( piece, coordinate );
                        var moveSuccessful = Board.DoMove( move );
                        if( moveSuccessful ) {
                            list1.Add( move );
                        } else {
                            validMovelist = false;
                        }
                    } else {
                        validMovelist = false;
                    }
                    if( !validMovelist ) {
                        break;
                    }
                } else if( piece.Type == PieceType.Butterfly ) {
                    var coordinate = Board.GetRandomNonRockCoordinate( random.GetRandom );
                    if( coordinate != Coordinate.InvalidCoordinate ) {
                        var move = new Move( piece, coordinate );
                        var moveSuccessful = Board.DoMove( move );
                        if( moveSuccessful ) {
                            list1.Add( move );
                        } else {
                            validMovelist = false;
                        }
                    } else {
                        validMovelist = false;
                    }
                    if( !validMovelist ) {
                        break;
                    }
                }
            }

            if( validMovelist ) {
                list1.Score = Board.GetNumberOfFreeSpaces();
                if( MovelistCreated != null ) {
                    var args = new MovelistCreatedEventArgs {
                        CreatedThroughMutation = false,
                        CreatedThroughCrossover = false,
                        Score = list1.Score,
                        Moves = list1.ToString()
                    };
                    MovelistCreated( args );
                }
            } else {
                list1 = null;
            }
            Board.Reset();
            return list1;
        }
        #endregion

        #region Events
        private void HookupEvents( Solver childSolver ) {
            childSolver.MovelistCreated += childSolver_MovelistCreated;
            childSolver.GenerationCreated += childSolver_GenerationCreated;
            childSolver.SolverCreated += childSolver_SolverCreated;
        }

        void childSolver_SolverCreated( SolverCreatedEventArgs args ) {
            if( SolverCreated != null ) {
                SolverCreated( args );
            }
        }

        void childSolver_GenerationCreated( GenerationCreatedEventArgs args ) {
            if( GenerationCreated != null ) {
                GenerationCreated( args );
            }
        }

        private void childSolver_MovelistCreated( MovelistCreatedEventArgs args ) {
            if( MovelistCreated != null ) {
                MovelistCreated( args );
            }
        }
        #endregion

        private void CreateInitialPopulation( IRandomizer r ) {
            var childrenCreated = 0;
            while( childrenCreated < GeneticAlgorithmOptions.PopulationSize ) {
                var createdList = CreateRandomMovelist( r );
                if( createdList != null ) {
                    _currentGeneration.Add( createdList );
                    childrenCreated++;
                }
            }
        }
    }
}
