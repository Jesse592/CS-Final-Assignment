using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client.Handlers
{
    class TeacherHandlerVM
    {
        private ConnectionManager manager;
        private TeacherViewModel vm;

        public TeacherHandlerVM(TeacherViewModel view)
        {
            this.manager = ConnectionManager.GetConnectionManager();
            this.vm = view;

            GetModules();
        }

        public void AddGrade(Module module, Grade grade, User student)
        {
           
        }

        public void AddGradeCallback()
        {

        }

        public void GetModules()
        {
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetModules", new JObject())
                    ), GetModulesCallback);
        }

        public void GetModulesCallback(JObject jObject)
        {
            JToken ModulesArray = jObject.SelectToken("data") as JArray;

            foreach(JObject j in ModulesArray)
            {
                this.vm.Modules.Add(new Module(j));
            }

            this.vm.Modules = new ObservableCollection<Module>(this.vm.Modules.OrderByDescending(module => module.Name).ToList());
        }

        public void GetStudentsPerModule(Module module)
        {

        }

        public void GetStudentsCallback()
        {

        }
    }
}
