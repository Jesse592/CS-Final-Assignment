using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        private void GetGradesFromStudent(JObject data, int serial)
        {
            throw new NotImplementedException();
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
