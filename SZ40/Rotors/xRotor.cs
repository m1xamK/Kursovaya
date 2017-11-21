using System.Collections.Generic;

namespace Rotors
{
	public class XRotor : IRotor
	{
		public XRotor(List<int> arr, int startPosition, int shift)
		{
			_shift = shift;
			_pins = arr;
			_position = startPosition - 1;
		}

		/// <summary>
		/// немного говнокода ибо 1 нужна для интерфейса
		/// </summary>
		/// <param name="?"></param>
		public void Turn(int t)
		{
			int count = _pins.Count;

			_position = (++_position) % count;
		}

		public int GetValue()
		{
			int count = _pins.Count;

			return _pins[(_position + _shift) % count];
		}

		private int _position { set; get; }
		private int _shift { set; get; }
		private List<int> _pins;
	}
}
