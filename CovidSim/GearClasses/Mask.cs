namespace CovidSim.GearClasses
{
    public class Mask : IGear
    {
        /// <summary>
        /// How well the gear protects you
        /// </summary>
        public Modifier ProtectionModifier { get; }

        /// <summary>
        /// How well the gear protects others from you
        /// </summary>
        public Modifier PreventionModifier { get; }

        public Mask(short protection = 20, short prevention = 95)
        {
            ProtectionModifier = new Modifier {Value = protection};
            PreventionModifier = new Modifier {Value = prevention};
        }
    }
}
