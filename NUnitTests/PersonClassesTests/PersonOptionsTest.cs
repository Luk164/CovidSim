using CovidSim.PersonClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace NUnitTests
{
    [TestFixture]
    class PersonOptionsTest
    {
        private PersonOptions personOptions { get; set; } = new PersonOptions();
        private Dictionary<string, short> defaultValues = new Dictionary<string, short>();

        [SetUp]
        public void Setup()
        {
            defaultValues.Add("quarantineComplianceValue", 100);
            defaultValues.Add("gearComplianceValue", 80);
            defaultValues.Add("cureCountdownValue", 14);
            defaultValues.Add("symptomCountdownValue", 5);
            defaultValues.Add("escalatedSymptomsValue", 20);
            defaultValues.Add("deathRateValue", 8);
            defaultValues.Add("asymptomaticProbabilityValue", 30);
        }

        [Test]
        public void OptionsTest()
        {
           Assert.AreEqual(personOptions.QuarantineCompliance.Value, defaultValues["quarantineComplianceValue"]);
           Assert.AreEqual(personOptions.GearCompliance.Value, defaultValues["gearComplianceValue"]);
           Assert.AreEqual(personOptions.CureCountdown, defaultValues["cureCountdownValue"]);
           Assert.AreEqual(personOptions.SymptomCountdown, defaultValues["symptomCountdownValue"]);
           Assert.AreEqual(personOptions.EscalatedSymptoms.Value, defaultValues["escalatedSymptomsValue"]);
           Assert.AreEqual(personOptions.DeathRate.Value, defaultValues["deathRateValue"]);
           Assert.AreEqual(personOptions.AsymptomaticProbability.Value, defaultValues["asymptomaticProbabilityValue"]);
        }
    }
}
