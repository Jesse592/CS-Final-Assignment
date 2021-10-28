using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Grading_Administration_Server.DatabaseMock
{
    public class DbMock
    {
        private List<Grade> grades = new List<Grade>();
        private List<Module> modules = new List<Module>();
        private List<ModuleContribution> moduleContributions = new List<ModuleContribution>();
        private List<User> users = new List<User>();

        /// <summary>
        /// Creating the mocked database for testing
        /// </summary>
        /// <returns>The mocked database</returns>
        public GradingDBContext GetDBMock()
        {
            FillLists();

            var DbMoq = new Mock<GradingDBContext>();

            DbMoq.Setup(p => p.Users).Returns(DBValuesMock.GetQueryableMockDbSet<User>(users));
            DbMoq.Setup(p => p.Modules).Returns(DBValuesMock.GetQueryableMockDbSet<Module>(modules));
            DbMoq.Setup(p => p.moduleContributions).Returns(DBValuesMock.GetQueryableMockDbSet<ModuleContribution>(moduleContributions));
            DbMoq.Setup(p => p.grades).Returns(DBValuesMock.GetQueryableMockDbSet<Grade>(grades));

            DbMoq.Setup(p => p.SaveChanges()).Returns(1);

            return DbMoq.Object;
        }

        /// <summary>
        /// Creating test data to fill the mocked database with
        /// </summary>
        private void FillLists()
        {
            User testLeerling = new User()
            {
                UserId = 1,
                FirstName = "test",
                LastName = "test",
                DateOfBirth = DateTime.MinValue,
                Email = "test@test.nl",
                UserType = "0",
                Modules = new List<ModuleContribution>()
            };

            User testDocent = new User()
            {
                UserId = 2,
                FirstName = "testDocent",
                LastName = "testDocent",
                DateOfBirth = DateTime.MinValue,
                Email = "testDocent@test.nl",
                UserType = "1",
                Modules = new List<ModuleContribution>()
            };

            Module testModule = new Module()
            {
                ModuleId = 1,
                Name = "test",
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MinValue,
                ETC = 1,
                IsNumerical = true,
                Participants = new List<ModuleContribution>()
            };


            ModuleContribution leerlingContribution = new ModuleContribution()
            {
                ContributionId = 1,
                UserId = testLeerling.UserId,
                User = testLeerling,
                ModuleId = testModule.ModuleId,
                Module = testModule,
                grades = new List<Grade>()
            };

            ModuleContribution docentContribution = new ModuleContribution()
            {
                ContributionId = 2,
                UserId = testDocent.UserId,
                User = testDocent,
                ModuleId = testModule.ModuleId,
                Module = testModule,
                grades = new List<Grade>()
            };

            Grade grade = new Grade()
            {
                gradeID = 1,
                ContributionId = leerlingContribution.ContributionId,
                Contribution = leerlingContribution,
                Time = DateTime.MinValue,
                NumericalGrade = 8.1,
                LetterGrade = "G",
                Delimiter = 5.5
            };

            leerlingContribution.grades.Add(grade);
            testModule.Participants.Add(docentContribution);
            testModule.Participants.Add(leerlingContribution);
            testLeerling.Modules.Add(leerlingContribution);
            testDocent.Modules.Add(docentContribution);

            modules.Add(testModule);
            users.Add(testDocent);
            users.Add(testLeerling);
            moduleContributions.Add(docentContribution);
            moduleContributions.Add(leerlingContribution);
            grades.Add(grade);

        }
    }
}
