using NUnit.Framework;
using TestApplication7.Services.Ics;
using System;

namespace TestApplication7.Tests
{
    [TestFixture]
    public class RateModelTest
    {

        private IcsServices _icsServices;

        [SetUp]
        public void SetUp()
        {
            _icsServices = new IcsServices();
        }

        [Test]
        public void TestMethod2()
        {           
            var postalCode = "L4N 3P8";
            if (postalCode == "L4N 3P8")
            {
                Assert.IsFalse(false);
            }
            var result = _icsServices.GetEstimatedCharges(1, 1, 1, 1, "M9W7J2");
            Assert.IsTrue(result == 13.17M);
        }
    }

}
