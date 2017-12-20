using System;
using System.Text.RegularExpressions;
using System.IO;

namespace ManagerRepresentor
{
    class ManagerRepresentorConsole
    {
        static void Main()
        {
            string[] commandArr = { "" };   //Массив содержит команду и параметры, если такие имеются.
            Regex regex = new Regex("[0-9a-z]{32}"); //Регулярное выражение проверяющее hash сумму.
            ManagerRepresentor managerRepresentor = new ManagerRepresentor(".\\Private$\\RequestQueue", ".\\Private$\\ReplyQueue");
            while (true)
            {
                var command = Console.ReadLine();	//Считывание команды с параметрами с консоли

				//Если command существует разбиваем по разделителю(пробелу).
                if (command != null) commandArr = command.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);
                switch (commandArr[0])
                {
                    case ("exit"):
                        return ;

                    case ("start"):	//Инициализиция вычислений.
                        managerRepresentor.StartCalculation();
                        
                        break;

                    case ("add_hash"): //Добавляет хеш сумму введенную в консоли(add_hash hash).
                        if (commandArr.Length == 2 && regex.IsMatch(commandArr[1])) // Проверка валидности хеша.
                            managerRepresentor.PushNewHash(commandArr[1]);
                        else
                            Console.WriteLine("Не правильно введен hash");
                        break;

                    case ("a"):  //Добавляет хеш суммы из файла(строки файла)
                        if (commandArr.Length == 2 && File.Exists(commandArr[1]))   //Проверка того, что файл существует.
                        {
                            var hashFromFile = File.ReadAllLines(commandArr[1]);    //Чтение файла построчно.
                            foreach (string hash in hashFromFile)
                                if(regex.IsMatch(hash)) managerRepresentor.PushNewHash(hash); //Если строка md5, то добавляем в List managerRepresentor.
                        }
                        else
                            Console.WriteLine("Не правильно введен путь к файлу с хешами");
                        break;
                }

            }
        }
    }
}