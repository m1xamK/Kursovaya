using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ManagerRepresentor
{
    class ManagerRepresentor
    {
        private readonly Manager.Manager _manager;
        private readonly List<string> _hashList;
        public bool CalcultationFlag;

        /// <summary>
        /// Конструктор класса ManagerRepresentor.
        /// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public ManagerRepresentor(string requestResource, string replyResourсe)
        {
            _hashList = new List<string>(); //Инициализация перемееных.
            CalcultationFlag = false;
            _manager = new Manager.Manager(requestResource, replyResourсe);
        }
        /// <summary>
        /// Добавляет новый хеш в List.
        /// </summary>
        /// <param name="hash">md5 hash summ</param>
        /// <returns></returns>
        public bool PushNewHash(string hash)
        {
            _hashList.Add(hash); //Добавление md5 в List.
            return true;
        }
        /// <summary>
        /// Инициализирует процесс вычисления пароля для List'а хеш сумм.
        /// </summary>
        /// <returns>Строковое представление найденных: хешей паролей</returns>
        public string StartCalculation()
        {
            if (_hashList.Count == 0)
               return "Нет хеш сумм для проверки";
            
            CalcultationFlag = true;	//Установка флага произведения вычислений.
            _manager.FindHash(_hashList.ToArray<string>());	//Инициализация вычислений.

	        string strFlag = "";
			while (strFlag == "")
			{
				strFlag = _manager.ReciveSync();
			}

            _hashList.Clear();	//После окончания вычислений отчистка _hashList.
			return strFlag;
        }
    }
}