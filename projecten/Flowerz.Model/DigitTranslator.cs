using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flowerz.Model {
	internal class DigitTranslator {
		public static byte IntToByte( int i ) {
			switch( i ) {
				case 0:
					return 0x30;
				case 1:
					return 0x31;
				case 2:
					return 0x32;
				case 3:
					return 0x33;
				case 4:
					return 0x34;
				case 5:
					return 0x35;
				case 6:
					return 0x36;
				case 7:
					return 0x37;
				case 8:
					return 0x38;
				case 9:
					return 0x39;
			}
			return 0x00;
		}
	}
}
