using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogParserDN
{
    public class Command
    {
        public Command(string command) {
            Arguments = Regex.Match(command, @"arguments=\[.*\]").Value.Substring(10).Split(new char[] {'[',']', ','},StringSplitOptions.RemoveEmptyEntries);
            Name = Regex.Match(command, "name=(get|start)Rendering").Value.Substring(5);
        }
    
        public string Name { get; set; }
        public string[] Arguments { get; set; }
    }
}
