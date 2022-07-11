using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerIntegrationApp.Data;
using CustomerIntegrationApp.Data.Utils;
using System;

namespace CustomerIntegrationApp.Test
{
    [TestClass]
    public class CustomerIntegrationTest
    {
        [TestMethod]
        public void GenerateToken()
        {
            long testResult = 0;
            testResult = SecurityHelper.GenerateToken(2, 3456, new DateTime(2022, 7, 1));
            Assert.IsTrue(testResult == 202207014536);
        }
    }
}
