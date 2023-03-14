using RACE2.Logging.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Logging.Service
{
    public interface ILogService
    {
            
        void Write(string message);

        void Error(Exception exception, string message);       
        //void Critical(string category, LogMessage message);

      
        //void Error(string category, LogMessage message);

        
        //void Error(string category, LogMessage message, Exception ex);

      
        //void Warning(string category, LogMessage message);
    }
}
