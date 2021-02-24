using CovidSim.GearClasses;

namespace CovidSim.PersonClasses
{
    public static class PersonFactory
    {
        public static Person GenerateCitizen(Person.HealthStatusEnum healthStatus = Person.HealthStatusEnum.Healthy, PersonOptions options = null)
        {
            return new Person(options){Health = healthStatus};
        }

        public static Person GenerateMedicalStaff(PersonOptions options = null)
        {
            var person = new Person(options) {CitizenClass = Person.CitizenClassEnum.MedicalStaff};

            if (options == null)
            {
                //Default medical staff preset
                person.Options.GearCompliance.Value = 90;
                person.Options.QuarantineCompliance.Value = 100;
            }

            person.Gear.Add(new Gloves());
            person.Gear.Add(new Mask());
            person.Gear.Add(new MedicalGown());
            person.Gear.Add(new HandSanitizer());
            return person;
        }

        public static Person GenerateMilitary(PersonOptions options = null)
        {
            var person = new Person(options) {CitizenClass = Person.CitizenClassEnum.Military};

            if (options == null)
            {
                //Default medical staff preset
                person.Options.GearCompliance.Value = 90;
                person.Options.QuarantineCompliance.Value = 100;
            }

            person.Gear.Add(new Gloves());
            person.Gear.Add(new Mask());
            person.Gear.Add(new HandSanitizer());
            return person;
        }

        public static Person GenerateFirstResponder(PersonOptions options = null)
        {
            var person = new Person(options) {CitizenClass = Person.CitizenClassEnum.FirstResponder};

            if (options == null)
            {
                //Default medical staff preset
                person.Options.GearCompliance.Value = 90;
                person.Options.QuarantineCompliance.Value = 100;
            }

            person.Gear.Add(new Gloves());
            person.Gear.Add(new Mask());
            person.Gear.Add(new MedicalGown());
            person.Gear.Add(new HandSanitizer());
            return person;
        }
    }
}
