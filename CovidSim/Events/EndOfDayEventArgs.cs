using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidSim.Events
{
    public class EndOfDayEventArgs
    {
        public uint HealthyCount { get; set; }
        public uint AsymptomaticCount { get; set; }
        public uint SymptomsCount { get; set; }
        public uint SeriouslyIllCount { get; set; }
        public uint ImmuneCount { get; set; }
        public uint DeceasedCount { get; set; }
    }
}
