using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidSim.GearClasses
{
    class Immunity : IGear
    {
        public Modifier ProtectionModifier => new Modifier{Value = (short) new Random().Next(0, 20)} ;

        public Modifier PreventionModifier { get; } = new Modifier {Value = 0};
    }
}
