using System;
using System.Text.RegularExpressions;

namespace ManagerRepresentor
{
    class Program
    {
            static void Main(string[] args, object exit)
            {
                string[] commandArr;
                Regex regex = new Regex("[0-9a-f]{32}");
                ManagerRepresentor ManagerRepresentor = new ManagerRepresentor(".\\private$\\ReplyQueue", ".\\private$\\RequestQueue");
           
                var command = Console.ReadLine();
                while(!string.Equals(command, "exit"))
                {
                    if (string.Equals(command, "start"))
                        ManagerRepresentor.startCalculation();
                        commandArr = command.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);

                    if (commandArr.Length == 2 && String.Equals(commandArr[0], "add_hash") && regex.IsMatch(commandArr[1]))
                        ManagerRepresentor.pushNewHash(commandArr[1]);
                }


                while (true)
                {
                    var command = Console.ReadLine();

                    switch (command)
                    {
                        case ("exit"):
                            return ;
                    }
                    
                }
        }
    }
}
