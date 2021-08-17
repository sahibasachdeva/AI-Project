using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestApplication7.Services.Ics;


namespace TestApplication7.Tests
{
    /// <summary>
    /// Summary description for IcsUnitTests
    /// </summary>
    [TestClass]
    public class IcsUnitTests
    {
        public IcsUnitTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod2()
        {
            var service = new IcsServices();
            var postalCode = "L4N 3P8";
            if (postalCode == "L4N 3P8")
            {
                Assert.IsFalse(false);
            }
            var result = service.GetEstimatedCharges(1, 1, 1, 1, "M9W7J2");
            Assert.IsTrue(result == 13.17M);
        }


        [TestMethod]
        public void TestMethod1()
        {
            var service = new IcsServices();
            var result = service.GetEstimatedCharges(1, 1, 1, 1, "M9W7J2");
            Assert.IsTrue(result == 13.17M);
        }
    }
}
