using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserDN
{
    public class Report
    {
        public List<Rendering> Renderings { get; private set; }

        public Report() {
            Renderings = new List<Rendering>();
        }
    }
}
