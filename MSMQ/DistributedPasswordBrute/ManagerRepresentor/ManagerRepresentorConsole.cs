using System;
using System.Text.RegularExpressions;
using System.IO;

namespace ManagerRepresentor
{
    class ManagerRepresentorConsole
    {
        static void Main()
        {
            Regex regex = new Regex("[0-9a-z]{32}"); //Регулярное выражение проверяющее hash сумму.
            ManagerRepresentor managerRepresentor = new ManagerRepresentor(Environment.MachineName + "\\Private$\\RequestQueue", "DESKTOP-OUP4I3U\\Private$\\ReplyQueue");
            while (true)
            {
                var command = Console.ReadLine();	//Считывание команды с параметрами с консоли

				//Если command существует разбиваем по разделителю(пробелу).
                var parametr = "";
	            if (command == null)
		            continue;
                  
				string[] commandArr = command.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);

                command = commandArr[0];
                if (commandArr.Length == 2)
                {
                    parametr = commandArr[1];
                }
                switch (command)
                {
                    case ("exit"):
                        return ;

                    case ("start"):	//Инициализиция вычислений.
                        Console.WriteLine(managerRepresentor.StartCalculation());
                        
                        break;

                    case ("add_hash"): //Добавляет хеш сумму введенную в консоли(add_hash hash).
                        if (parametr != "" && regex.IsMatch(parametr)) // Проверка валидности хеша.
                            managerRepresentor.PushNewHash(parametr);
                        else
                            Console.WriteLine("Не правильно введен hash");
                        break;

                    case ("a"):  //Добавляет хеш суммы из файла(строки файла)
                        if (parametr != "" && File.Exists(parametr))   //Проверка того, что файл существует.
                        {
                            var hashFromFile = File.ReadAllLines(parametr);    //Чтение файла построчно.
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