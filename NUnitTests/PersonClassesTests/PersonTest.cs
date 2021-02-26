using CovidSim.PersonClasses;
using NUnit.Framework;

//https://docs.educationsmediagroup.com/unit-testing-csharp/nunit/lifecycle-of-a-test-fixture

namespace NUnitTests.PersonClassesTests
{
    [TestFixture]
    class PersonTest
    {
        private Person person;

        [SetUp]
        public void Setup()
        {
            person = PersonFactory.GenerateCitizen();
        }

        [TestCase(Person.HealthStatusEnum.Asymptomatic, ExpectedResult = true)]
        [TestCase(Person.HealthStatusEnum.Deceased, ExpectedResult = false)]
        [TestCase(Person.HealthStatusEnum.Healthy, ExpectedResult = false)]
        [TestCase(Person.HealthStatusEnum.Immune, ExpectedResult = false)]
        [TestCase(Person.HealthStatusEnum.SeriouslyIll, ExpectedResult = true)]
        [TestCase(Person.HealthStatusEnum.Symptoms, ExpectedResult = true)]
        public bool IsContagiousTest(Person.HealthStatusEnum healthStatus)
        {
            return new Person {Health = healthStatus}.IsContagious;
        }

        [Test]
        public void PreventionTest()
        {
            Assert.AreEqual(person.Prevention, 0);
            Assert.GreaterOrEqual(PersonFactory.GenerateMedicalStaff().Prevention, 99);
            Assert.GreaterOrEqual(PersonFactory.GenerateMilitary().Prevention, 99);
            Assert.GreaterOrEqual(PersonFactory.GenerateFirstResponder().Prevention, 99);
        }

        [Test]
        public void ProtectionTest()
        {
            Assert.AreEqual(person.Prevention, 0);
            Assert.GreaterOrEqual(PersonFactory.GenerateMedicalStaff().Protection, 97.70);
            Assert.GreaterOrEqual(PersonFactory.GenerateMilitary().Protection, 88.79);
            Assert.GreaterOrEqual(PersonFactory.GenerateFirstResponder().Protection, 97.70);
        }

        [TestCase(Person.HealthStatusEnum.Healthy, Person.HealthStatusEnum.Asymptomatic, ExpectedResult = true)]
        [TestCase(Person.HealthStatusEnum.Asymptomatic, Person.HealthStatusEnum.Healthy, ExpectedResult = false)]
        public bool MeetTest(Person.HealthStatusEnum meetPersonHealth, Person.HealthStatusEnum personHealth)
        {
            Person meetPerson = PersonFactory.GenerateMedicalStaff();
            meetPerson.Health = meetPersonHealth;
            person.Health = personHealth;

            person.Meet(meetPerson);
            return person.IsContagious;
        }
    }
}
