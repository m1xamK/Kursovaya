﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        /// <returns></returns>
        public bool StartCalculation()
        {
            if (_hashList.Count == 0)
            {
                Console.WriteLine("Нет хеш сумм для проверки");
                return false;
            }
            CalcultationFlag = true;//Установка флага произведения вычислений.
            _manager.FindHash(_hashList.ToArray<string>());//Инициализация вычислений.
            _manager.ReciveSync();
            _hashList.Clear();//После окончания вычислений отчистка _hashList.
            return true;
        }
    }
}