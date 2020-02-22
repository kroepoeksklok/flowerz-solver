namespace Flowerz.Model {
	public class Field {
		public Piece Piece { get; set; }

		public Field() {
			Piece = Piece.Empty;
		}

		public Field( Piece piece ) {
			Piece = piece;
		}

		public byte[] GetBytes() {
			return PieceTranslator.PieceToByte( Piece );
		}
	}
}