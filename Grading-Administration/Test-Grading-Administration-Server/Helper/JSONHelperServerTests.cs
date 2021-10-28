using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Grading_Administration_Server.Helper;
using Grading_Administration_Server.EntityFramework.models;

namespace Grading_Administration_Server.Helper.Tests
{
    [TestClass]
    public class JSONHelperServerTests
    {

        [TestMethod]
        public void GetIDFromJSONTest()
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
            Assert.AreEqual(expectedID, actualID);
        }

        [TestMethod]
        public void UserToSharedTest()
        {
            // Arrange
            List<User> users = new List<User>
            {
                new User() { UserId = 1, FirstName = "test", LastName = "test", DateOfBirth = DateTime.MinValue, Email = "test", UserType = "0"},
                new User() { UserId = 2, FirstName = "test2", LastName = "test2", DateOfBirth = DateTime.MinValue, Email = "test2", UserType = "1"},
                new User() { UserId = 3, FirstName = "test2", LastName = "test2", DateOfBirth = DateTime.MinValue, Email = "test2", UserType = "2"}
            };

            List<Grading_Administraton_Shared.Entities.User> expectedUsers = new List<Grading_Administraton_Shared.Entities.User>()
            {
                new Grading_Administraton_Shared.Entities.User(1, "test", "test", DateTime.MinValue, "test", "0"),
                new Grading_Administraton_Shared.Entities.User(2, "test2", "test2", DateTime.MinValue, "test2", "1"),
                new Grading_Administraton_Shared.Entities.User(3, "test3", "test3", DateTime.MinValue, "test3", "2")
            };

            // Act
            List<Grading_Administraton_Shared.Entities.User> actualUsers = JSONHelperServer.UserToShared(users);

            // Assert
            Assert.AreEqual<int>(expectedUsers.Count, actualUsers.Count);
            Assert.AreEqual<int>(expectedUsers[0].UserId, actualUsers[0].UserId);
            Assert.AreEqual<string>(expectedUsers[0].FirstName, actualUsers[0].FirstName);
            Assert.AreEqual<string>(expectedUsers[0].UserType, actualUsers[0].UserType);
        }

        [TestMethod]
        public void GradesToSharedTest()
        {
            // Arrange
            List<Grade> grades = new List<Grade>
            {
                new Grade() { Time = DateTime.MinValue, NumericalGrade = 8.2, LetterGrade = "G", Delimiter = 5.5},
                new Grade() { Time = DateTime.MinValue, NumericalGrade = 1.5, LetterGrade = "O", Delimiter = 5.5},
                new Grade() { Time = DateTime.MinValue, NumericalGrade = 3.2, LetterGrade = "Q", Delimiter = 5.5}
            };

            List<Grading_Administraton_Shared.Entities.Grade> expectedGrades = new List<Grading_Administraton_Shared.Entities.Grade>()
            {
                new Grading_Administraton_Shared.Entities.Grade(DateTime.MinValue, 8.2, "G", 5.5),
                new Grading_Administraton_Shared.Entities.Grade(DateTime.MinValue, 1.5, "O", 5.5),
                new Grading_Administraton_Shared.Entities.Grade(DateTime.MinValue, 3.2, "Q", 5.5)
            };

            // Act
            List<Grading_Administraton_Shared.Entities.Grade> actualGrades = JSONHelperServer.GradesToShared(grades);

            // Assert
            Assert.AreEqual<int>(expectedGrades.Count, actualGrades.Count);
            Assert.AreEqual<double>(expectedGrades[0].NumericalGrade, actualGrades[0].NumericalGrade);
            Assert.AreEqual<double>(expectedGrades[0].Delimiter, actualGrades[0].Delimiter);
            Assert.AreEqual<string>(expectedGrades[0].LetterGrade, actualGrades[0].LetterGrade);
        }

        [TestMethod]
        public void ModulesToSharedTest()
        {
            // Arrange
            List<Module> grades = new List<Module>
            {
                new Module() { ModuleId = 1, Name = "test", StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, ETC = 4, IsNumerical = true},
                new Module() { ModuleId = 2, Name = "test2", StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, ETC = 1, IsNumerical = true},
                new Module() { ModuleId = 3, Name = "test3", StartDate = DateTime.MinValue, EndDate = DateTime.MaxValue, ETC = 2, IsNumerical = true}
            };

            List<Grading_Administraton_Shared.Entities.Module> expectedGrades = new List<Grading_Administraton_Shared.Entities.Module>()
            {
                new Grading_Administraton_Shared.Entities.Module(1, "test", DateTime.MinValue, DateTime.MaxValue, 4, true),
                new Grading_Administraton_Shared.Entities.Module(2, "test2", DateTime.MinValue, DateTime.MaxValue, 1, true),
                new Grading_Administraton_Shared.Entities.Module(3, "test3", DateTime.MinValue, DateTime.MaxValue, 2, true)
            };

            // Act
            List<Grading_Administraton_Shared.Entities.Module> actualGrades = JSONHelperServer.ModulesToShared(grades);

            // Assert
            Assert.AreEqual<int>(expectedGrades.Count, actualGrades.Count);
            Assert.AreEqual<int>(expectedGrades[0].ModuleId, actualGrades[0].ModuleId);
            Assert.AreEqual<string>(expectedGrades[0].Name, actualGrades[0].Name);
            Assert.AreEqual<int>(expectedGrades[0].ETC, actualGrades[0].ETC);
        }

    }
}
