using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Splunk_Class01
{
    class SplunkDTO
    {
        public string functionName { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public string time { get; set; }
    }
}
