using System;
using System.Text.RegularExpressions;
using System.IO;

namespace ManagerRepresentor
{
    class Program
    {
        static void Main()
        {
            string[] commandArr = { "" };   //Массив содержит команду и параметры, если такие имеются
            Regex regex = new Regex("[0-9a-f]{32}"); //Регулярное выражение проверяющее hash сумму
            ManagerRepresentor managerRepresentor = new ManagerRepresentor(".\\private$\\ReplyQueue", ".\\private$\\RequestQueue");
            managerRepresentor.PrintResult();
            while (true)
            {
                var command = Console.ReadLine();//Считывание команды с параметрами с консоли
                if (command != null) commandArr = command.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);//Если command существует разбиваем по разделителю(пробелу)
                switch (commandArr[0])
                {
                    case ("exit"):
                        return ;
                    case ("start")://Инициализиция ычислений
                        managerRepresentor.startCalculation();
                        while (managerRepresentor._calcultationFlag)
                        { 
                            System.Threading.Thread.Sleep(300);//Приостанавливаем работу основного потока пока идут вычисления
                        }
                        break;
                    case ("add_hash"): //Добавляет хеш сумму введенную в консоли(add_hash hash)
                        if (commandArr.Length == 2 && regex.IsMatch(commandArr[1])) // Проверка валидности хеша
                            managerRepresentor.pushNewHash(commandArr[1]);
                        else
                            Console.WriteLine("Не правильно введен hash");
                        break;
                    case ("add_hashfile")://Добавляет хеш суммы из файла(строки файла)
                        if (commandArr.Length == 2 && File.Exists(commandArr[1]))//Проверка того, что файл существует
                        {
                            var hashFromFile = File.ReadAllLines(commandArr[1]); //Чтение файла построчно
                            foreach (string hash in hashFromFile)
                                if(regex.IsMatch(hash)) managerRepresentor.pushNewHash(hash); //Если строка md5, то добавляем в List managerRepresentor 
                        }
                        else
                            Console.WriteLine("Не правильно введен путь к файлу с хешами");
                        break;
                }

            }
        }
    }
}