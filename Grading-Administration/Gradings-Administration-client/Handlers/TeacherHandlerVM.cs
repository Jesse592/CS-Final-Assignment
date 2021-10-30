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
    /// <summary>
    /// Class handles the data and communication for the teacher view model
    /// </summary>
    public class TeacherHandlerVM
    {
        private ConnectionManager manager;
        private TeacherViewModel vm;

        /// <summary>
        /// Constructor for the teachermanager 
        /// </summary>
        /// <param name="view">The view given</param>
        public TeacherHandlerVM(TeacherViewModel view)
        {
            this.manager = ConnectionManager.GetConnectionManager();
            this.vm = view;

            // Getting the modules of the teacher
            GetModules();
        }

        /// <summary>
        /// Command to tell the server to add a grade to the list
        /// </summary>
        /// <param name="module">The module to add the grade to</param>
        /// <param name="grade">The grade to add</param>
        /// <param name="student">The student to add the grade to</param>
        public void AddGrade(Module module, Grade grade, User student)
        {
            this.manager.SendCommand(
                 JObject.FromObject(
                     JSONWrapper.WrapHeader("AddGrade", JSONWrapper.WrapGradeModuleUser(grade, module, student))
                     ), AddGradeCallback);
        }

        /// <summary>
        /// Mehtod that handles the response to the AddGrade command
        /// </summary>
        /// <param name="jObject"></param>
        public void AddGradeCallback(JObject jObject)
        {
            //Tells the user the grade was added
            this.vm.CloseWindow("Cijfer succesvol opgeslagen");
        }

        /// <summary>
        /// Command to tell the server to get all the modules of this user
        /// </summary>
        public void GetModules()
        {
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetModules", new JObject())
                    ), GetModulesCallback);
        }

        /// <summary>
        /// Mehtod handles the response from the server to a GetModules command
        /// </summary>
        /// <param name="jObject">The data from the server</param>
        public void GetModulesCallback(JObject jObject)
        {
            // getting the JSON array
            JToken ModulesArray = jObject.SelectToken("data") as JArray;

            // Going trough all the modules in the array
            foreach(JObject j in ModulesArray)
            {
                // Created the array
                this.vm.AddModule(new Module(j));
            }

            // Sorting the lis by module name
            this.vm.Modules = new ObservableCollection<Module>(this.vm.Modules.OrderByDescending(module => module.Name).ToList());
        }

        /// <summary>
        /// Sends a command to get detailed information for all students in one course
        /// </summary>
        /// <param name="module"></param>
        public void GetStudentsPerModule(Module module)
        {
            this.manager.SendCommand(
                JObject.FromObject(
                    JSONWrapper.WrapHeader("GetDataFromModule", JSONWrapper.WrapModule(module))
                    ), GetStudentsCallback);
        }

        /// <summary>
        /// Mehtod handles the response from the server to a GetStudentsPerModule command
        /// </summary>
        /// <param name="jObject">The data from the server</param>
        public void GetStudentsCallback(JObject jObject)
        {
            JToken StudentArray = jObject.SelectToken("data") as JArray;

            // Checking if the array has been parse
            if (StudentArray == null) return;

            // Clearing the list
            this.vm.ClearStudent();

            foreach (JObject j in StudentArray)
            {
                this.vm.AddStudent(new User(j));
            }

            this.vm.Students = new ObservableCollection<User>(this.vm.Students.OrderByDescending(student => student.FirstName).ToList());
        }
    }
}
