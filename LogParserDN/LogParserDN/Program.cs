using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using Ninject;
using System.Globalization;

namespace LogParserDN
{
    class Program
    {
        static IKernel KERNEL;

        static void Main(string[] args) {
            if (args.Length == 2)
            {
                Setup();
                Console.WriteLine("Startup LogParserDN");

                Report report = KERNEL.Get<Report>();
                Dictionary<Entry, Entry> startEntries = new Dictionary<Entry, Entry>();
                List<Entry> getEntries = new List<Entry>();

                Console.WriteLine("Reading {0}", args[0]);

                ParseFile(args[0], startEntries, getEntries);

                Console.WriteLine("Proccessing info");

                ProccessInfo(report, startEntries, getEntries);

                Console.WriteLine("Saving To {0}", args[1]);

                report.Save(args[1]);

                Console.WriteLine("Done");
            }
            else
            {
                Console.WriteLine("Usage: LogParserDN.exe [inputfile] [outputfile]");
                Console.WriteLine("Example: LogParserDN.exe Log/server.log output.xml");
            }
        }

        private static void ProccessInfo(Report report, Dictionary<Entry, Entry> startEntries, List<Entry> getEntries) {
            foreach (KeyValuePair<Entry, Entry> startEntry in startEntries)
            {
                Command cmd = new Command(startEntry.Key.Message);

                Rendering rend;
                if (report.Renderings.Any(r => r.ID == int.Parse(cmd.Arguments[0])))
                {
                    rend = report.Renderings.Single(r => r.ID == int.Parse(cmd.Arguments[0]));
                }
                else
                {
                    rend = new Rendering { ID = int.Parse(cmd.Arguments[0]), Page = int.Parse(cmd.Arguments[1]), UID = startEntry.Value.Message.Split(' ').Last() };
                    report.Renderings.Add(rend);
                }

                rend.StartRendering.Add(startEntry.Key.TimeStamp);
            }

            foreach (var entry in getEntries)
            {
                try
                {
                    Command cmd = new Command(entry.Message);
                    report.Renderings.Last(r => r.UID == cmd.Arguments.First()).GetRendering.Add(entry.TimeStamp);
                }
                catch { }
            }
        }

        private static void ParseFile(string input, Dictionary<Entry, Entry> startEntries, List<Entry> getEntries) {
            Regex cmdStart = new Regex(".*Processing command object.*startRendering.*");
            Regex returnStart = new Regex(".*Service startRendering returned.*");
            Regex cmdGet = new Regex(".*Processing command object.*getRendering.*");

            using (StreamReader reader = new StreamReader(File.OpenRead(input)))
            {
                string line;

                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    if (line.Contains("startRendering") || line.Contains("getRendering"))
                    {
                        if (cmdStart.IsMatch(line))
                        {
                            startEntries.Add(new Entry(line), null);
                        }
                        else if (returnStart.IsMatch(line))
                        {
                            try
                            {
                                Entry entry = new Entry(line);
                                Entry key = startEntries.Single(kv => kv.Value == null && kv.Key.Thread == entry.Thread).Key;
                                startEntries[key] = entry;
                            }
                            catch { }
                            
                        }
                        else if (cmdGet.IsMatch(line))
                        {
                            getEntries.Add(new Entry(line));
                        }
                    }
                }
            }
        }

        private static void Setup() 
        {
            KERNEL = new StandardKernel();
            KERNEL.Bind<ISave>().To<XMLSave>();
        }
    }
}
