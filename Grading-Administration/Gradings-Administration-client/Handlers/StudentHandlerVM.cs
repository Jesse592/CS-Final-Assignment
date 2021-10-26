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
using System.Threading;
using System.Diagnostics;

namespace GradingAdmin_client.Handlers
{
    class StudentHandlerVM
    {
        private ConnectionManager manager;
        private StudentViewModel vm;

        private User currentUser;

        public StudentHandlerVM(User student, StudentViewModel view)
        {
            this.manager = ConnectionManager.GetConnectionManager();
            this.vm = view;

            this.currentUser = student;

            GetStudentData();
        }

        public void GetStudentData()
        {
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetAllGrades", JSONWrapper.WrapUser(this.currentUser))
                    ), StudentDataCallback);

        }

        public void StudentDataCallback(JObject jObject)
        {
            List<Grade> grades = new List<Grade>();
            List<Module> modules = new List<Module>();

            JToken array = jObject.SelectToken("data");
            Trace.WriteLine(array);

            foreach (JObject o in array)
            {
                grades.Add(new Grade(o));
                modules.Add(new Module(o));
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
