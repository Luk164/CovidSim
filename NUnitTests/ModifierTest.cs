using CovidSim;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTests
{
    [TestFixture]
    class ModifierTest
    {
        [TestCase(0, TestName = "Modifier.set = 0", ExpectedResult = 0)]
        [TestCase(20, TestName = "Modifier.set = 20", ExpectedResult = 20)]
        [TestCase(50, TestName = "Modifier.set = 50", ExpectedResult = 50)]
        [TestCase(100, TestName = "Modifier.set = 100", ExpectedResult = 100)]
        public short ModifierSetTest(short value)
        {
            return new Modifier { Value = value }.Value;
        }

        [TestCase(-1, TestName = "Modifier.set = -1", Ignore = "ArgumentOutOfRangeException")]
        [TestCase(101, TestName = "Modifier.set = 101", Ignore = "ArgumentOutOfRangeException")]
        public void ModifierOutOfRangeTest(short value)
        {
            Modifier mod = new Modifier {};
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => mod.Value = value);
            Assert.That(ex.Message, Is.EqualTo("Specified argument was out of the range of valid values."));
        }
    }
}
