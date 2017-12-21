using System;
using System.Collections.Generic;

namespace Manager
{
	/// <summary>
	/// структура данных, хранящая информацию о сообщениях, отправленных агентам
	/// </summary>
	public class MsgInProcess
	{
		//Диапазон, отправленный для обработки агенту в сообщении
		public KeyValuePair<string, string> Range { get; private set; }

		//Время, в которое было отправленно сообщение агенту 
		public DateTime Time { get; private set; }

		public MsgInProcess(KeyValuePair<string, string> range, DateTime time)
		{
			Range = range;
			Time = time;
		}
	}
}
