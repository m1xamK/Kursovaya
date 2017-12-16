using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using System.Threading;
namespace ManagerRepresentor
{
    class ManagerRepresentor
    {

        private Manager.Manager _manager;
        private List<string> _hashList;

        public ManagerRepresentor(string requestResurce, string replayResurse)
		{
			_manager = new Manager.Manager(requestResurce, replayResurse);
		}

       public  bool pushNewHash(string hash)
        {
            _hashList.Add(hash);
            return true;
        }
       public bool startCalculation()
       {
           if (_hashList.Count == 0)
           {
               Console.WriteLine("biba");
               return false;
           }
           else
           {
          string[] hashArr = _hashList.ToArray<string>();
          _manager.FindHash(hashArr);
          _hashList.Clear();
           return true;
       }
       }
       

       public bool PrintResult()
       {
           Thread _logthread = new Thread(getLog);
           return true;
       }
        static void getLog() 
        {
            string log;
            while(!String.Equals(log, "end"))
            {
                string lastLog = _manager.ResiveSync();
                if(!String.Equals(lastLog, log))
                {
                    Console.WriteLine(lastLog);
                    log = lastLog;
                }
                Thread.Sleep(150);                   
            }
        }
    }
    }