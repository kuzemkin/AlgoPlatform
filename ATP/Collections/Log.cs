using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP.Collections
{
    public class Log
    {
        public List<string> messageLog = new List<string>();
        public Log()
        {
            
        }
        public string LogView()
        {
            string row=messageLog[0];
            for(int i=1; i<messageLog.Count; i++)
            {
                if(messageLog[i]!=messageLog[i-1])
                {
                    row +="\n"+messageLog[i];                   
                }
            }            
            return row;
        }   
        public List<string> Add(string row)
        {
            messageLog.Add(row);
            return messageLog;
        }
    }
}
