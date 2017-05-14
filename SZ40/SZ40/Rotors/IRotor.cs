namespace Rotors
{
	/// <summary>
	/// Интерфейс для работы с Роторами в машине
	/// </summary>
	interface IRotor
	{
		/// <summary>
		/// Функция возвращающая текущеее значение колеса
		/// </summary>
		/// <returns></returns>
		int GetValue();

		/// <summary>
		/// Функция изменяющее позицию колеса
		/// </summary>
		/// <param name="term"></param>
		void Turn(int term);
	}
}
