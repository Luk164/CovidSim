using System;
using CovidSim.GearClasses;

namespace CovidSim.PersonClasses
{
    public static class PersonFactory
    {
        /// <summary>
        /// Generate a new citizen
        /// </summary>
        /// <param name="healthStatus">Health status to be set for new citizen. Default is healthy</param>
        /// <param name="options">Options settings for the new citizen. Default is null, a new object will be instantiated with default constructor</param>
        /// <returns>New citizen</returns>
        public static Person GenerateCitizen(Person.HealthStatusEnum healthStatus = Person.HealthStatusEnum.Healthy, PersonOptions options = null)
        {
            var person = new Person(options){Health = healthStatus};

            var random = new Random();

            //Chance to comply and wear a mask
            if (random.Next(0, 100) < person.Options.GearCompliance.Value)
            {
                person.Gear.Add(new Mask());
            }

            return person;
        }

        /// <summary>
        /// Generate new medical staff member
        /// </summary>
        /// <param name="options">Options settings for the new citizen. Default is null, a new object will be instantiated with default constructor</param>
        /// <returns>New medical staff member</returns>
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

        /// <summary>
        /// Generate military staff member
        /// </summary>
        /// <param name="options">Options settings for the new citizen. Default is null, a new object will be instantiated with default constructor</param>
        /// <returns>New military staff member</returns>
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

        /// <summary>
        /// Generate a first responder
        /// </summary>
        /// <param name="options">Options settings for the new citizen. Default is null, a new object will be instantiated with default constructor</param>
        /// <returns>New first responder</returns>
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
