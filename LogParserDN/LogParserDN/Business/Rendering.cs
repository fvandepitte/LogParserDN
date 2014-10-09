using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserDN
{
    public class Rendering
    {
        public int ID { get; set; }
        public int Page { get; set; }
        public string UID { get; set; }
        public List<DateTime> StartRendering { get; private set; }
        public List<DateTime> GetRendering { get; private set; }

        public Rendering() {
            StartRendering = new List<DateTime>();
            GetRendering = new List<DateTime>();
        }
    }
}
