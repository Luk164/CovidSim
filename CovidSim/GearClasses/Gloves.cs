namespace CovidSim.GearClasses
{
    public class Gloves : IGear
    {
        //TODO Get real numbers
        public Modifier ProtectionModifier { get; } = new Modifier {Value = 30};
        public Modifier PreventionModifier { get; } = new Modifier {Value = 30};
    }
}
