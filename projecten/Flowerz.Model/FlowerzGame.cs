using System.IO;

namespace Flowerz.Model {
    public class FlowerzGame {
        public Board Board { get; set; }
        public FlowerQueue Queue { get; set; }

        public FlowerzGame() {
            Board = new Board();
            Queue = new FlowerQueue();
        }

        public void AddToQueue( Piece piece ) {
            Queue.AddToQueue( piece );
        }

        public void SetField( int row, int col, Piece piece ) {
            Board.SetPieceToField( row, col, piece );
        }

        public static FlowerzGame CreateGameFromFile( string fileLocation ) {
            var game = new FlowerzGame();
            using( var stringReader = new StreamReader( new FileStream( fileLocation, FileMode.Open ) ) ) {
                string s;
                var lineNumber = 1;
                while( ( s = stringReader.ReadLine() ) != null ) {

                    if( lineNumber == 1 ) {
                        //Line 1 -> Queue, Items are separates by spaces
                        //Cannot contain Rock (X) or empty field (_)
                        var queueItems = s.Split( ' ' );
                        foreach( var queueItem in queueItems ) {
                            if( queueItem == PieceStrings.Empty || queueItem == PieceStrings.Rock ) {
                                throw new InvalidQueueException( "'" + queueItem + "' is not a valid piece for the queue" );
                            }

                            game.AddToQueue( PieceTranslator.StringToPiece( queueItem ) );
                        }
                    } else if( lineNumber > 1 ) {
                        //Lines 2-8 -> board layout
                        //Cannot contain shovel (S) or Butterfly (*F)
                        var fieldItems = s.Split( ' ' );
                        for( int i = 0; i < fieldItems.Length; i++ ) {
                            if( fieldItems[ i ] != "S" ) {
                                game.SetField( lineNumber - 2, i, PieceTranslator.StringToPiece( fieldItems[ i ] ) );
                            }
                        }
                    }

                    lineNumber++;
                }
            }

            return game;
        }
    }
}