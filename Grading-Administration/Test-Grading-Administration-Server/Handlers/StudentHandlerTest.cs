using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Moq.Protected;
using Microsoft.EntityFrameworkCore;
using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using System;
using System.Collections.Generic;
using Moq;
using Test_Grading_Administration_Server.DatabaseMock;

namespace Grading_Administration_Server.Handlers.Test
{
    [TestClass]
    public class StudentHandlerTest
    {
        private GradingDBContext DataBaseMock;
        private User user;

        [TestInitialize]
        public void Initialize()
        {
            this.DataBaseMock = new DbMock().GetDBMock();
            this.user = this.DataBaseMock.Users.Find(1);
        }
        

        [TestMethod]
        public void GetAllGradesTest()
        {
            // Arrange
            JObject testObject = JObject.FromObject(new
            { user = new
            {
                UserId = 1
            }
            });

            StudentHandler handler = new StudentHandler(this.DataBaseMock, this.user, (o) => {

                JArray grades = o.SelectToken("data.grades") as JArray;

                //Assert
                Assert.IsNotNull(grades);
                Assert.AreEqual(grades.Count, 1); // Test database holds 1 grade for this user
            });

            // Act
            handler.Invoke("GetAllGrades", testObject, 0);
        }

        [TestMethod]
        public void GetModulesTest()
        {
            // Arrange
            JObject testObject = JObject.FromObject(new
            {
                user = new
                {
                    UserId = 1
                }
            });

            StudentHandler handler = new StudentHandler(this.DataBaseMock, this.user, (o) => {
                JArray modulesArray = o.SelectToken("data.modules") as JArray;

                //Assert
                Assert.IsNotNull(modulesArray);
                Assert.AreEqual(modulesArray.Count, 1); // Test database holds 1 module for this user
            });

            // Act
            handler.Invoke("grades", testObject, 0);
        }

    }
}
