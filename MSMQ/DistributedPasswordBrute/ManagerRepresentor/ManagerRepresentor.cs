using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
namespace ManagerRepresentor
{
    class ManagerRepresentor
    {

        private readonly Manager.Manager _manager;
        private List<string> _hashList;
        public bool _calcultationFlag;
        /// <summary>
        /// Конструктор класса ManagerRepresentor
        /// </summary>
        /// <param name="requestResource"></param>
        /// <param name="replyResourсe"></param>
        public ManagerRepresentor(string requestResource, string replyResourсe)
        {
            _hashList = new List<string>(); //инициализация перемееных
            _calcultationFlag = false;
            _manager = new Manager.Manager(requestResource, replyResourсe);
        }
        /// <summary>
        /// Добавляет новый хеш в List
        /// </summary>
        /// <param name="hash">md5 hash summ</param>
        /// <returns></returns>
        public bool PushNewHash(string hash)
        {
            _hashList.Add(hash); //Добавление md5 в List
            return true;
        }
        /// <summary>
        /// Инициализирует процесс вычисления пароля для List'а хеш сумм
        /// </summary>
        /// <returns></returns>
        public bool startCalculation()
        {
            if (_hashList.Count == 0)
            {
                Console.WriteLine("Нет хеш сумм для проверки");
                return false;
            }
            _calcultationFlag = true;//Установка флага произведения вычислений
            _manager.FindHash(_hashList.ToArray<string>());//Инициализация вычислений

	        int temp = 0;
	        
	        _manager.ReciveSync();

            _hashList.Clear();//После окончания вычислений отчистка _hashList
            return true;
        }

        /// <summary>
        /// Функция отвечающая за вывод логов на экран в отдельном потоке
        /// </summary>
        /// <returns></returns>
        public bool PrintResult()
        {
            Thread _logthread = new Thread(GetLog);// Создание потока ввыводяшего результат
            return true;
        }
        /// <summary>
        /// Сама функция выводящая на экран
        /// </summary>
        public void GetLog()
        {
			string log = "";
			StreamWriter logStreamWriter = new StreamWriter(@"../logs.txt", true);
			while (!string.Equals(log, "end"))
            {
                //string lastLog = 
				_manager.ReciveSync();
				//if (!string.Equals(lastLog, log))
				//{   
				//	Console.WriteLine(lastLog);
				//	logStreamWriter.WriteLine(lastLog);
				//	log = lastLog;
				//}

                Thread.Sleep(150);
            }
        }
    }
}