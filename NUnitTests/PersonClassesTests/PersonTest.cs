using CovidSim.PersonClasses;
using NUnit.Framework;

//https://docs.educationsmediagroup.com/unit-testing-csharp/nunit/lifecycle-of-a-test-fixture

namespace NUnitTests.PersonClassesTests
{
    [TestFixture]
    class PersonTest
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
            personMilitary = PersonFactory.GenerateMedicalStaff();
            personFirstResponder = PersonFactory.GenerateFirstResponder();
        }
    }
}
