using Grading_Administration_Server.EntityFramework.models;
using GradingAdmin_client.ViewModels;
using Gradings_Administration_client;
using Grading_Administraton_Shared.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.Handlers
{
    class StudentHandlerVM
    {
        private ConnectionManager manager;
        private StudentViewModel vm;

        private User currentUser;

        public StudentHandlerVM(User student, StudentViewModel view)
        {
            this.manager = new ConnectionManager();
            this.vm = view;

            this.currentUser = student;

            GetAllGrades();
        }

        public void GetModules(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetModules", JSONWrapper.WrapUser(u))), ModulesCallback);
        }

        public void ModulesCallback(JObject jObject)
        {

        }

        public void GetGrade(Module m, User u)
        {
            // Sends the command to get all the grades from a given module and the current user
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetGrade", JSONWrapper.WrapModuleUser(m, u))), tGradeCallback);
        }

        public void tGradeCallback(JObject jObject)
        {

        }

        public void GetAllGrades()
        {
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetAllGrades", JSONWrapper.WrapUser(this.currentUser))
                    ), AllGradesCallback);

        }

        public void AllGradesCallback(JObject jObject)
        {
            List<Grade> grades = new List<Grade>();

            JArray array = (JArray)jObject.GetValue("data");
            foreach (JObject o in array)
            {
                grades.Add(new Grade(o));
            }

            this.vm.Grades = grades;
        }

        public void GetTeachersFromModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetTeachers", JSONWrapper.WrapModule(m))), TeachersCallback);
        }

        public void TeachersCallback(JObject jObject)
        {

        }
    }
}
