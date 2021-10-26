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
        private async void GetGradesFromStudent(JObject data, int serial)
        {
            Console.WriteLine(data);

            // Getting the student user
            int studentID = JSONHelperServer.JsonToUser(data);

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
                    grade = mc.grades.OrderByDescending(g => g.NumericalGrade).FirstOrDefault()?.ToSharedGrade()
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
        private void GetStudentsFromModule(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all the students this teacher shares a module with
        /// </summary>
        /// <param name="data">The request object</param>
        /// <param name="serial">The ID-code from the client</param>
        private void GetStudents(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all the modules this teacher is in
        /// </summary>
        /// <param name="data">The request object</param>
        /// <param name="serial">The ID-code from the client</param>
        private void GetModules(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a grade to the database of the given user, module and grade
        /// </summary>
        /// <param name="data">The object that contains: the user, module and grade</param>
        /// <param name="serial">The ID-code from the client</param>
        private void AddGrade(JObject data, int serial)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Sets up all the commands this handler can handle
        /// </summary>
        protected override void Init()
        {
            this.Actions.Add("AddGrade", AddGrade);
            this.Actions.Add("GetModules", GetModules);
            this.Actions.Add("GetStudents", GetStudents);
            this.Actions.Add("GetDataFromModule", GetStudentsFromModule);
            this.Actions.Add("GetGradesFromStudent", GetGradesFromStudent);
        }
    }
}
