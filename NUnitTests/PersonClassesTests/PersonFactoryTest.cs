using CovidSim.PersonClasses;
using NUnit.Framework;
using System;

namespace NUnitTests.PersonClassesTests
{
    [TestFixture]
    class PersonFactoryTest
    {
        private Person personCitizen = PersonFactory.GenerateCitizen();
        private Person personMedicalStaff = PersonFactory.GenerateMedicalStaff();
        private Person personMilitary = PersonFactory.GenerateMedicalStaff();
        private Person personFirstResponder = PersonFactory.GenerateFirstResponder();

        [Test]
        public void MakePerson()
        {
            Assert.IsNotNull(personCitizen);
            Assert.IsNotNull(personMedicalStaff);
            Assert.IsNotNull(personMilitary);
            Assert.IsNotNull(personFirstResponder);
        }
    }
}
