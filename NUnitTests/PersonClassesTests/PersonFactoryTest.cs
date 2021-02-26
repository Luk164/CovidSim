using CovidSim.PersonClasses;
using NUnit.Framework;
using System;

namespace NUnitTests.PersonClassesTests
{
    [TestFixture]
    class PersonFactoryTest
    {
        private Person personCitizen;
        private Person personMedicalStaff;
        private Person personMilitary;
        private Person personFirstResponder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            personCitizen = PersonFactory.GenerateCitizen();
            personMedicalStaff = PersonFactory.GenerateMedicalStaff();
            personMilitary = PersonFactory.GenerateMilitary();
            personFirstResponder = PersonFactory.GenerateFirstResponder();
        }

        [Test]
        public void MakePersonTest()
        {
            Assert.IsNotNull(personCitizen);
            Assert.AreEqual(Person.CitizenClassEnum.Citizen, personCitizen.CitizenClass);
            
            Assert.IsNotNull(personMedicalStaff);
            Assert.AreEqual(Person.CitizenClassEnum.MedicalStaff, personMedicalStaff.CitizenClass);

            Assert.IsNotNull(personMilitary);
            Assert.AreEqual(Person.CitizenClassEnum.Military, personMilitary.CitizenClass);

            Assert.IsNotNull(personFirstResponder);
            Assert.AreEqual(Person.CitizenClassEnum.FirstResponder, personFirstResponder.CitizenClass);
        }

        [Test]
        public void SetOptionsAndGearToPersonTest()
        {
            Assert.IsNotNull(personCitizen.Health);
            Assert.AreEqual(personCitizen.Gear.Count, 0);

            Assert.AreEqual(90, personMedicalStaff.Options.GearCompliance.Value);
            Assert.AreEqual(100, personMedicalStaff.Options.QuarantineCompliance.Value);
            Assert.AreEqual(4, personMedicalStaff.Gear.Count);

            Assert.AreEqual(90, personMilitary.Options.GearCompliance.Value);
            Assert.AreEqual(100, personMilitary.Options.QuarantineCompliance.Value);
            Assert.AreEqual(3, personMilitary.Gear.Count);

            Assert.AreEqual(90, personFirstResponder.Options.GearCompliance.Value);
            Assert.AreEqual(100, personFirstResponder.Options.QuarantineCompliance.Value);
            Assert.AreEqual(4, personFirstResponder.Gear.Count);
        }
    }
}
