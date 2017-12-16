using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ManagerRepresentor
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] commandArr;
            Regex regex = new Regex("[0-9a-f]{32}");
           ManagerRepresentor ManagerRepresentor = new ManagerRepresentor(".\\private$\\ReplyQueue", ".\\private$\\RequestQueue");
           
           var command = Console.ReadLine();
            while(!String.Equals(command, "/exit"))
            {
           if (String.Equals(command, "/start"))
              ManagerRepresentor.startCalculation();
             commandArr = command.Split((string[]) null, StringSplitOptions.RemoveEmptyEntries);
           if (commandArr.Length == 2 && String.Equals(commandArr[0], "/hash") && regex.IsMatch(commandArr[1]))
               ManagerRepresentor.pushNewHash(commandArr[1]);
            }
        }
    }
}
