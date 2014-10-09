using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParserDN
{
    public interface ISave
    {
        void SaveReport(Report report, string path);
    }
}
