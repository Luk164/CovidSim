using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidSim
{
    public class Modifier
    {
        private short _value;

        public short Value
        {
            get => _value;
            set
            {
                if (Value < 0 || Value > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value), "Value in modifier has to be between 0 and 100");
                }
                _value = value;
            }
        }
    }
}
