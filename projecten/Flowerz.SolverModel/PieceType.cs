namespace Flowerz.SolverModel {
	/// <summary>
	/// The type of the piece.
	/// </summary>
	public enum PieceType {
		/// <summary>
		/// Item is a flower. There's always a first color, but a second one is optional
		/// </summary>
		Flower,

		/// <summary>
		/// Item is a shovel, colors are irrelevant
		/// </summary>
		Shovel,
		
		/// <summary>
		/// Item is a butterfly, only outer colour is relevant
		/// </summary>
		Butterfly,
		
		/// <summary>
		/// Item is empty. Not applicable for items in the queue.
		/// </summary>
		Empty,
		
		/// <summary>
		/// Item type is a rock. Not applicable for items in the queue.
		/// </summary>
		Rock
	}
}