using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserDN
{
    public class Entry
    {
        public DateTime TimeStamp { get; private set; }
        public string Thread { get; private set; }
        public LogLevel LogLevel { get; set; }
        public string Class { get; private set; }
        public string Message { get; private set; }
        public string[] StackTrace { get; private set; }

        public Entry(params string[] logEntry) {
            string[] logInfo = logEntry[0].Split(new char[] { '[', ']' }, 5);
            
            TimeStamp = DateTime.ParseExact(logInfo[0].Trim(), "yyyy-MM-dd HH:mm:ss,fff", CultureInfo.InvariantCulture);
            Thread = logInfo[1];
            LogLevel = (LogLevel)Enum.Parse(typeof(LogLevel), logInfo[2], true);
            Class = logInfo[3];
            Message = logInfo[4].Substring(2);

            if (logEntry.Length > 1)
            {
                StackTrace = logEntry.Skip(1).ToArray();
            }
            else
            {
                StackTrace = new string[0];
            }
        }

        public override string ToString() {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss,fff} [{1}] {2} [{3}]: {4}", TimeStamp, Thread, LogLevel, Class, Message);
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
