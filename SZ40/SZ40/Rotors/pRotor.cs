using System.Collections.Generic;

namespace Rotors
{
	/// <summary>
	/// класс занимающийся первыми 5 колесами с которых считывается гамма 
	/// и первым машинным колесом ибо поворачиваются каждый такт
	/// </summary>
	public class PRotor : IRotor
	{
		public PRotor(List<int> arr, int startPosition, int shift)
		{
			_shift = shift;
			_pins = arr;
			_position = startPosition - 1;
		}

		public void Turn(int valid)
		{
			if (valid == 0)
				return;

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
