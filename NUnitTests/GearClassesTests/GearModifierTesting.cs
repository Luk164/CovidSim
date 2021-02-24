using CovidSim.GearClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests.GearEntitiesTests
{
    [TestFixture]
    class GearModifierTesting
    {
        private Mask mask;
        private Gloves gloves;
        private HandSanitizer handSanitizer;
        private MedicalGown medicalGown;

        [SetUp]
        public void Setup()
        {
            mask = new Mask();
            gloves = new Gloves();
            handSanitizer = new HandSanitizer();
            medicalGown = new MedicalGown();
        }

        [Test(Description = "Mask testing")]
        public void MaskTest()
        {
            Assert.AreEqual(20, mask.ProtectionModifier.Value);
            Assert.AreEqual(95, mask.PreventionModifier.Value);
        }

        [Test(Description = "Gloves testing")]
        public void GlovesTest()
        {
            Assert.AreEqual(30, gloves.ProtectionModifier.Value);
            Assert.AreEqual(30, gloves.PreventionModifier.Value);
        }

        [Test(Description = "HandSanitizer testing")]
        public void HandSanitizerTest()
        {
            Assert.AreEqual(80, handSanitizer.ProtectionModifier.Value);
            Assert.AreEqual(80, handSanitizer.PreventionModifier.Value);
        }

        [Test(Description = "MedicalGown testing")]
        public void MedicalGownTest()
        {
            Assert.AreEqual(80, medicalGown.ProtectionModifier.Value);
            Assert.AreEqual(20, medicalGown.PreventionModifier.Value);
        }
    }
}
