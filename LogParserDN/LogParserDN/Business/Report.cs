using Ninject;
using System.Collections.Generic;

namespace LogParserDN
{
    public class Report
    {
        public List<Rendering> Renderings { get; private set; }
        private ISave _saver;

        [Inject]
        public Report(ISave saver) {
            Renderings = new List<Rendering>();
            _saver = saver;
        }

        public void Save(string path) {
            _saver.SaveReport(this, path);
        }
    }
}
