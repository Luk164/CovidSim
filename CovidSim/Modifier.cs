using System;

namespace CovidSim
{
    /// <summary>
    /// Modifier class. Used to save percentages
    /// </summary>
    public class Modifier
    {
        private short _value;

        /// <summary>
        /// Value between 0 to 100
        /// <exception cref="Value">Throws ArgumentOutOfRange</exception>
        /// </summary>
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
