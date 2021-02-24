using CovidSim.PersonClasses;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;
using DescriptionAttribute = NUnit.Framework.DescriptionAttribute;

namespace NUnitTests
{
    [TestFixture]
    class PersonOptionsTest
    {
        public PersonOptions personOptions { get; set; } = new PersonOptions();

        [Test]
        [Description("Default value to QuarantineCompliance")]
        public void QuarantineComplianceTest()
        {
           Assert.AreEqual(personOptions.QuarantineCompliance.Value, 100);
        }
    }
}
