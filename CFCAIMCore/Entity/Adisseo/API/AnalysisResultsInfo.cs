using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFCAIMCore.Entity.Adisseo
{
    public class AnalysisResultsInfo
    {
        public int id { get; set; }
        public string sampleCode { get; set; }
        public string materialCode { get; set; }
        public List<string> analysesCodes { get; set; }
        public string spectralFileName { get; set; }
        public string description { get; set; }
        public string origin { get; set; }
        public int submitDate { get; set; }
    }
}
