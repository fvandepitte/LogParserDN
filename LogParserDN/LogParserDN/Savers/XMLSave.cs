using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LogParserDN
{
    public class XMLSave : ISave
    {
        public void SaveReport(Report report, string path) {
            XElement root = new XElement("report");
            root.Add(report.Renderings.Where(r => r.StartRendering.Count > 0).Select(ParseRendering));

            root.Add(new XElement("summary", 
                new XElement("count", report.Renderings.Count),
                new XElement("duplicates", report.Renderings.GroupBy(r => r.UID).Where(gr => gr.Count() > 1).Count()),
                new XElement("unnecessary", report.Renderings.Where(r => r.StartRendering.Count > 0 && r.GetRendering.Count == 0).Count())));

            XDocument doc = new XDocument();
            doc.Add(root);
            doc.Save(path);
        }

        private XElement ParseRendering(Rendering rendering) {
            XElement rend = new XElement("rendering",
                    new XElement("document", rendering.ID),
                    new XElement("page", rendering.Page),
                    new XElement("uid", rendering.UID));

            rend.Add(rendering.StartRendering.Select(start => new XElement("start", start.ToString("yyyy-MM-dd HH:mm:ss,fff"))));
            rend.Add(rendering.GetRendering.Select(get => new XElement("get", get.ToString("yyyy-MM-dd HH:mm:ss,fff"))));

            return rend;
        }
    }
}
