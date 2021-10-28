using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Grading_Administration_Server.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    class TeacherHandler : Handler
    {
        private readonly GradingDBContext GradingDBContext;
        private User user;

        /// <summary>
        /// Constructor for Teacherhandler, sets the dbcontext and user
        /// </summary>
        /// <param name="gradingDBContext">The databse context to use</param>
        /// <param name="user">The user that is connected to this handler</param>
        /// <param name="sendAction">the action that is called on response</param>
        public TeacherHandler(GradingDBContext gradingDBContext, User user, Action<JObject> sendAction) : base(sendAction)
        {
            GradingDBContext = gradingDBContext;
            this.user = user;
        }

        /// <summary>
        /// Returns all the grades from a given student
        /// </summary>
        /// <param name="data">The student to get the grades from</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task GetGradesFromStudent(JObject data, int serial)
        {
            Console.WriteLine(data);

            // Getting the student user
            int studentID = JSONHelperServer.GetIDFromJSON(data, "user.UserId");

            // Ignoring if the userID is -1
            if (studentID == -1) return;

            // Searching the database to get all the grades of the user
            List<ModuleContribution> grades = await(from dt in this.GradingDBContext.moduleContributions
                                                    where dt.User.UserId == studentID
                                                    select dt).ToListAsync();

            // Creating a list that wil be send to the client
            List<object> moduleGradeList = new List<object>();

            // List contains the module name and the highest graded grade
            foreach (ModuleContribution mc in grades)
            {
                moduleGradeList.Add(new
                {
                    module = mc.Module.Name,
                    grade = mc.grades.OrderByDescending(grade => grade.NumericalGrade).FirstOrDefault()?.ToSharedGrade()
                });
            }

            // Sending the data to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GenericList("GetAllGrades", moduleGradeList, serial)));
        }

        /// <summary>
        /// Returns all the students from a given module
        /// </summary>
        /// <param name="data">The module to get the students from</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task GetDataFromModule(JObject data, int serial)
        {
            // Getting the moduleID from the data
            int moduleID = JSONHelperServer.GetIDFromJSON(data, "module.ModuleId");

            // returning if the module ID is not correct (-1)
            if (moduleID == -1) return;

            // Getting all the students that participate in this module
            List<User> students = await (from user in this.GradingDBContext.Users
                                         join con in this.GradingDBContext.moduleContributions on user.UserId equals con.UserId
                                         where con.ModuleId == moduleID &&
                                         user.UserType == ((int)UserType.STUDENT).ToString()
                                         select user).ToListAsync();

            // transforming the list to shared users
            var studentsShared = JSONHelperServer.UserToShared(students);

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllUsers(studentsShared, serial)));
        }

        /// <summary>
        /// Returns all the students
        /// </summary>
        /// <param name="data">The request object</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task GetStudents(JObject data, int serial)
        {
            // Getting all the students
            List<User> students = await (from user in this.GradingDBContext.Users
                                         where user.UserType == ((int)UserType.STUDENT).ToString()
                                         select user).ToListAsync();

            // transforming the list to shared users
            var studentsShared = JSONHelperServer.UserToShared(students);

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllUsers(studentsShared, serial)));
        }

        /// <summary>
        /// Returns all the modules this teacher is in
        /// </summary>
        /// <param name="data">The request object</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task GetModules(JObject data, int serial)
        {
            // Getting all the modules this teacher teaches
            List<ModuleContribution> modules = await (from module in this.GradingDBContext.moduleContributions
                                                      where module.User.UserId == this.user.UserId
                                                      select module).ToListAsync();

            // Transforming the list to a shared module list
            var sharedModules = new List<Grading_Administraton_Shared.Entities.Module>();
            modules.ForEach(mod => sharedModules.Add(mod.Module.ToSharedModule()));

            // Sending the modules to the user
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllModules(sharedModules, serial)));
        }

        /// <summary>
        /// Adds a grade to the database of the given user, module and grade
        /// </summary>
        /// <param name="data">The object that contains: the user, module and grade</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task AddGrade(JObject data, int serial)
        {
            // Getting the ID's
            int userID = JSONHelperServer.GetIDFromJSON(data, "user.UserId");
            int moduleID = JSONHelperServer.GetIDFromJSON(data, "module.ModuleId");

            JObject gradeJSON = data.SelectToken("grade") as JObject;

            // Chechking is they are valid (not -1)
            if (userID == -1 || moduleID == -1 || gradeJSON == null)
            {
                this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeFailed(serial)));
                return;
            }

            // Getting the user and module and moduleconstribution from the database
            ModuleContribution moduleContribution = await (from mc in this.GradingDBContext.moduleContributions
                                                           where mc.ModuleId == moduleID &&
                                                           mc.UserId == userID
                                                           select mc).FirstOrDefaultAsync();

            // Transforming the sharedGrade to database Grade
            var sharedGrade = new Grading_Administraton_Shared.Entities.Grade(gradeJSON);
            Grade grade = new Grade(moduleContribution, sharedGrade.Time, sharedGrade.NumericalGrade, sharedGrade.LetterGrade, sharedGrade.Delimiter);

            // Adding the grade to the database
            moduleContribution.grades.Add(grade);
            await this.GradingDBContext.SaveChangesAsync();

            // Sending acknowledgement of succes to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeSucces(serial)));
        }


        /// <summary>
        /// Sets up all the commands this handler can handle
        /// </summary>
        protected override void Init()
        {
            this.Actions.Add("AddGrade", AddGrade);
            this.Actions.Add("GetModules", GetModules);
            this.Actions.Add("GetStudents", GetStudents);
            this.Actions.Add("GetDataFromModule", GetDataFromModule);
            this.Actions.Add("GetGradesFromStudent", GetGradesFromStudent);
        }
    }
}
