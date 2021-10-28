using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Grading_Administration_Server.Helper;

namespace Grading_Administration_Server.Helper.Tests
{
    [TestClass]
    public class JSONUtilTests
    {
        /// <summary>
        /// Testing the TryParse extension method with a correct string
        /// </summary>
        [TestMethod]
        public void TryParseTestSucces()
        {
            // Arrange
            JObject jObject = new JObject();
            string testString = "{\"command\": \"login\",\"data\": {\"username\": \"jesse\",\"password\": \"123\"}}";

            // Act
            JObject resultObject;

            bool canParse = jObject.TryParse(testString, out resultObject);

            // Assert
            Assert.IsTrue(canParse);
            Assert.IsNotNull(resultObject);
        }

        /// <summary>
        /// Testing the TryParse extension method with a faulty string
        /// </summary>
        [TestMethod]
        public void TryParseTestFail()
        {
            // Arrange
            JObject jObject = new JObject();
            string testString = "{\"command\": login\",\"data\": \"username\": \"jesse\",\"password\": \"123\"}}";

            // Act
            JObject resultObject;

            bool canParse = jObject.TryParse(testString, out resultObject);

            // Assert
            Assert.IsFalse(canParse);
            Assert.IsNull(resultObject);
        }
    }
}