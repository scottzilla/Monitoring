using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monitoring.Model
{
    public class MonitorRecord
    {
        public int ID { get; set; }

        public int AppID { get; set; }

        public string AppArea { get; set; }

        public string Information { get; set; }

        public string Comments { get; set; }

        public bool Error { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
