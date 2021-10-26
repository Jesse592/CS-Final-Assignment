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
using System.Collections.ObjectModel;

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
            

            /*JObject j = JObject.Parse("{ \"command\": \"GetAllGrades\", \"data\": [ { \"module\": { \"ModuleId\": 10, \"Name\": \"OGP0\", \"StartDate\": \"2020-08-01T00:00:00\", \"EndDate\": \"2020-08-31T00:00:00\", \"ETC\": 2, \"IsNumerical\": true }, \"teachers\": [ { \"UserId\": 8, \"FirstName\": \"Johan\", \"LastName\": \"Talboom\", \"DateOfBirth\": \"1984-07-14T13:26:00\", \"Email\": \"JohanTalboom@avans.nl\", \"UserType\": \"1\" } ], \"mcGrades\": [ { \"Time\": \"2020-08-24T00:00:00\", \"NumericalGrade\": 4.2, \"LetterGrade\": \"O\", \"Delimiter\": 5.5 }, { \"Time\": \"2020-12-24T00:00:00\", \"NumericalGrade\": 9.6, \"LetterGrade\": \"G\", \"Delimiter\": 5.5 } ] }, { \"module\": { \"ModuleId\": 11, \"Name\": \"2DG\", \"StartDate\": \"2021-02-12T00:00:00\", \"EndDate\": \"2020-04-02T00:00:00\", \"ETC\": 6, \"IsNumerical\": true }, \"teachers\": [ { \"UserId\": 8, \"FirstName\": \"Johan\", \"LastName\": \"Talboom\", \"DateOfBirth\": \"1984-07-14T13:26:00\", \"Email\": \"JohanTalboom@avans.nl\", \"UserType\": \"1\" }, { \"UserId\": 10, \"FirstName\": \"Hans\", \"LastName\": \"Van der Linden\", \"DateOfBirth\": \"1972-07-14T13:26:00\", \"Email\": \"HJ.Linden@avans.nl\", \"UserType\": \"1\" } ], \"mcGrades\": [] } ], \"serial\": 1 }");
            StudentDataCallback(j);*/
        }

        public void StudentDataCallback(JObject jObject)
        {
            JToken array = jObject.SelectToken("data") as JArray;
            Trace.WriteLine(array);

            foreach (JObject o in array)
            {
                this.vm.Modules.Add(new Module(o.SelectToken("module") as JObject, o.SelectToken("teachers") as JArray));

                foreach(JObject g in o.SelectToken("mcGrades") as JArray)
                {
                    this.vm.Grades.Add(new Grade(g, o.SelectToken("module.Name")));
                }
            }

            this.vm.Grades = new ObservableCollection<Grade>(this.vm.Grades.OrderByDescending(grade => grade.Time).ToList());
            this.vm.Modules = new ObservableCollection<Module>(this.vm.Modules.OrderBy(module => module.Name));
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
