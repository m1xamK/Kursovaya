using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace ManagerRepresentor
{
    class ManagerRepresentor
    {

        private readonly Manager.Manager _manager;
        private List<string> _hashList;

        public ManagerRepresentor(string requestResource, string replyResourse)
        {
            _hashList = new List<string>();
            _manager = new Manager.Manager(requestResource, replyResourse);
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
       public void getLog() 
       {
            string log = "";

            while(!string.Equals(log, "end"))
            {
                string lastLog = _manager.ReciveSync();
                if(!string.Equals(lastLog, log))
                {
                    Console.WriteLine(lastLog);
                    log = lastLog;
                }

                Thread.Sleep(150);                   
            }
       }
    }
    }