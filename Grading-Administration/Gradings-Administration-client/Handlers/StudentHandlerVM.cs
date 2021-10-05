using Grading_Administration_Server.EntityFramework.models;
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

        public StudentHandlerVM()
        {
            this.manager = new ConnectionManager();
        }

        public void GetModules(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetModules", JSONWrapper.WrapGetModules(u))), ModulesCallback);
        }

        public void ModulesCallback(JObject jObject)
        {

        }

        public void GetGrade(Module m, User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetGrade", JSONWrapper.WrapGetGrade(m, u))), tGradeCallback);
        }

        public void tGradeCallback(JObject jObject)
        {

        }

        public void GetAllGrades(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetAllGrades", JSONWrapper.WrapGetAllGrade(u))), AllGradesCallback);
        }

        public void AllGradesCallback(JObject jObject)
        {

        }

        public void GetTeachersFromModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetTeachers", JSONWrapper.WrapTeachersFromModule(m))), TeachersCallback);
        }

        public void TeachersCallback(JObject jObject)
        {

        }
    }
}
