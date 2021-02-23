namespace CovidSim.GearClasses
{
    public class MedicalGown: IGear
    {
        //TODO Get real numbers
        public Modifier ProtectionModifier { get; } = new Modifier {Value = 80};
        public Modifier PreventionModifier { get; } = new Modifier {Value = 20};
    }
}
