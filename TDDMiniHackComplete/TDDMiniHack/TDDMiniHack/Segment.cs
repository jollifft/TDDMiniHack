using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDMiniHack
{
    public class Segment
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void Start(DateTime startTime)
        {
            StartTime = startTime;
        }

        public void End(DateTime endTime)
        {
            EndTime = endTime;
        }
    }
}
