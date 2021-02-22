namespace CovidSim
{
    public interface IGear
    {
        /// <summary>
        /// Protection modifier expressed as a percentage
        /// </summary>
        public Modifier ProtectionModifier { get; }

        /// <summary>
        /// Prevention modifier expressed as a percentage
        /// </summary>
        public Modifier PreventionModifier { get; }
    }
}