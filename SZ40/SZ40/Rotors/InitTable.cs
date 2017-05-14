namespace Rotors
{
	using System.Collections.Generic;

	public static class InitTable
	{
		public static Dictionary<string, char> ArrDictionary;

		static InitTable()
		{
			ArrDictionary = new Dictionary<string, char>();
			string str = "/E3A9SIU4DRJNFCKTZLWHYPQOBG5MXV8";

			for (int i = 0; i < 32; ++i)
			{
				ArrDictionary.Add(
					((i >> 4) % 2).ToString()+
					((i >> 3) % 2).ToString()+
					((i >> 2) % 2).ToString()+
					((i >> 1) % 2).ToString()+
					(i % 2).ToString(), str[i]);
			}
		}
	}
}