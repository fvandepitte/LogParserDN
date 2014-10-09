using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserDN
{
    public class Entry
    {
        public DateTime TimeStamp { get; private set; }
        public string ThreadLevel { get; private set; }
        public LogLevel LogLevel { get; set; }
        public string Class { get; private set; }
        public string Message { get; private set; }
        public string[] StackTrace { get; private set; }

        public Entry(params string[] logEntry) {
            string[] logInfo = logEntry[0].Split('[', ']', ':');
            
            TimeStamp = DateTime.Parse(logInfo[0]);
            ThreadLevel = logInfo[1];
            LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), logInfo[2], true);
            Class = logInfo[3];
            Message = logInfo[4];

            if (logEntry.Length > 1)
            {
                StackTrace = logEntry.Skip(1).ToArray();
            }
            else
            {
                StackTrace = new string[0];
            }
        }
    }

    public enum LogLevel
    {
        DEBUG, 
        INFO, 
        WARN, 
        ERROR
    }
}
