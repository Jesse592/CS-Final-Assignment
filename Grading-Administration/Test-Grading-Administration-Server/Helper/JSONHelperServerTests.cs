using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Helper.Tests
{
    [TestClass]
    public class JSONHelperServerTests
    {

        [TestMethod]
        public static void GetIDFromJSONTest()
        {
            // Arrange
            int expectedID = 12;

            JObject jObject = JObject.FromObject(new
            {
                user = new
                {
                    userID = expectedID
                }
            });

            string path = "user.userID";


            // Act
            int actualID = JSONHelperServer.GetIDFromJSON(jObject, path);

            // Assert
            Assert.Equals(expectedID, actualID);
        }

        [TestMethod]
        public static void UserToSharedTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public static void GradesToSharedTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public static void ModulesToSharedTest()
        {
            // Arrange

            // Act

            // Assert
        }

    }
}
