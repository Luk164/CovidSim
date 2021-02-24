using CovidSim.GearClasses;
using NUnit.Framework;

namespace NUnitTests.GearEntitiesTests
{
    [TestFixture]
    class MaskTest
    {
        private Mask mask;

        //TODO - set via reference?
        private short _protectionModifierValue = 20;
        private short _preventionModifierValue = 95;


        [SetUp]
        public void Setup()
        {
            mask = new Mask();
        }

        [Test]
        public void ProtectionModifierTest()
        {
            Assert.AreEqual(_protectionModifierValue, mask.ProtectionModifier.Value);
        }

        [Test]
        public void PreventionModifierTest()
        {
            Assert.AreEqual(_preventionModifierValue, mask.PreventionModifier.Value);
        }
    }
}
