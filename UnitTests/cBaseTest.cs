using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class cBaseTest
    {
        private static bool m_FailAllTests;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void InitializeMethod()
        {
            if (m_FailAllTests)
            {
                Assert.Fail("Fail all tests");
            }
        }

        [TestCleanup]
        public void CleanUpMethod()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                m_FailAllTests = true;
            }
        }
    }
}
