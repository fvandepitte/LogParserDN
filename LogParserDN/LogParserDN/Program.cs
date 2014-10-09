using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace LogParserDN
{
    class Program
    {
        static void Main(string[] args) {
            List<string> entries = new List<string>();

            using (StreamReader reader = new StreamReader(File.OpenRead(args[0])))
            {
                Report r = new Report();

                string line;
                
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line.Contains("startRendering") || line.Contains("getRendering")) 
                    {
                        entries.Add(line);
                    }
                }
            }
        }
    }
}
