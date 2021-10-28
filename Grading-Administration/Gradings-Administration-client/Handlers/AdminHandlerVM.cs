
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GradingAdmin_client.Handlers
{
    /// <summary>
    /// Class that handles the communication for admin users
    /// </summary>
    class AdminHandlerVM
    {
        private ConnectionManager manager;
        private AdminViewModel view;

        /// <summary>
        /// Constructor for AdminHandler
        /// </summary>
        /// <param name="view">The vieuwModel that holds the users view</param>
        public AdminHandlerVM(AdminViewModel view)
        {
            this.manager = ConnectionManager.GetConnectionManager();
            this.view = view;
        }

        /// <summary>
        /// Sends a command to the server to create a new user
        /// </summary>
        /// <param name="u">The new user created</param>
        /// <param name="username">The username for the user</param>
        /// <param name="password">The password for the user</param>
        public void NewUser(User u, string username, string password)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("CreateNewUser", JSONWrapper.WrapNewUser(u, username, password))), NewUSerCallback);
        }

        /// <summary>
        /// Callback called when the server responds to the NewUser call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void NewUSerCallback(JObject obj)
        {
            this.view.UpdateUserStatus(obj);
        }

        /// <summary>
        /// Sends a command to the server to create a new Module
        /// </summary>
        /// <param name="m">The module to create on the server</param>
        public void NewModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("CreateNewModule", JSONWrapper.WrapModule(m))), NewModuleCallback);
        }

        /// <summary>
        /// The metod called when the server responds to the NewModule command
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void NewModuleCallback(JObject obj)
        {
            this.view.UpdateModuleStatus(obj);
        }

        /// <summary>
        /// Sends a command to the server to get all the users in the database
        /// </summary>
        public void GetAllUsers()
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetUsers", new JObject())), GetAllUsersCallback);
        }

        /// <summary>
        /// Callback called when the server responds to the GetAllUsers call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void GetAllUsersCallback(JObject obj)
        {
            // Setting up the lists
            List<User> UserList = new List<User>();
            List<User> TeacherList = new List<User>();
            List<User> StudentList = new List<User>();

            JToken UserArray = obj.SelectToken("data") as JArray;

            // Looping through all the users in the JArray
            foreach (JObject j in UserArray)
            {
                UserList.Add(new User(j.SelectToken("UserId").Value<int>(),
                    j.SelectToken("FirstName").Value<string>(),
                    j.SelectToken("LastName").Value<string>(),
                    j.SelectToken("DateOfBirth").Value<DateTime>(),
                    j.SelectToken("Email").Value<string>(),
                    j.SelectToken("UserType").Value<string>()));
            }

            // Splitting the users on student and teacher
            foreach (User u in UserList)
            {
                if (u.UserType == "0")
                {
                    StudentList.Add(u);
                }
                else if (u.UserType == "1")
                {
                    TeacherList.Add(u);
                }
            }

            this.view.UserList = UserList;
            this.view.TeacherList = TeacherList;
            this.view.StudentList = StudentList;
        }

        /// <summary>
        /// Sends a command to the server to get all the modules in the database
        /// </summary>
        public void GetAllModules()
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetModules", new JObject())), GetAllModulesCallback);
        }

        public void GetAllModulesCallback(JObject obj)
        {
            List<Module> ModuleList = new List<Module>();
            JToken ModuleArray = obj.SelectToken("data") as JArray;

            foreach (JObject j in ModuleArray)
            {
                ModuleList.Add(new Module(j.SelectToken("ModuleId").Value<int>(), 
                    j.SelectToken("Name").Value<string>(), 
                    j.SelectToken("StartDate").Value<DateTime>(), 
                    j.SelectToken("EndDate").Value<DateTime>(), 
                    j.SelectToken("ETC").Value<int>(), 
                    j.SelectToken("IsNumerical").Value<bool>()));
            }

            this.view.ModuleList = ModuleList;
        }

        /// <summary>
        /// Sends a command to the server to delete a user
        /// </summary>
        public void DeleteUser(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("DeleteUser", JSONWrapper.WrapUser(u))), DeleteUserCallback);
        }

        /// <summary>
        /// Callback called when the server responds to the DeleteUser call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void DeleteUserCallback(JObject obj)
        {
            this.view.UpdateDeleteUserStatus(obj);
        }

        /// <summary>
        /// Sends a command to the server to delete the module
        /// </summary>
        public void DeleteModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("DeleteModule", JSONWrapper.WrapModule(m))), DeleteModuleCallback);
        }

        /// <summary>
        /// Callback called when the server responds to the DeleteModule call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void DeleteModuleCallback(JObject obj)
        {
            this.view.UpdateDeleteModuleStatus(obj);
        }

        /// <summary>
        /// Sends a command to the server to add a teacher to a module
        /// </summary>
        public void AddTeacherToModule(User u, Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddUserToModule", JSONWrapper.WrapModuleUser(m, u))), AddTeacherToModule);
        }

        /// <summary>
        /// Callback called when the server responds to the AddTeacherToModule call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void AddTeacherToModule(JObject obj)
        {
            this.view.UpdateTeacherLinkStatus(obj);
        }

        /// <summary>
        /// Sends a command to the server to add a student to a module
        /// </summary>
        public void AddStudentToModule(User u, Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddUserToModule", JSONWrapper.WrapModuleUser(m, u))), AddStudentCallback);
        }

        /// <summary>
        /// Callback called when the server responds to the AddStudentToModule call
        /// </summary>
        /// <param name="obj">The data received from the server</param>
        public void AddStudentCallback(JObject obj)
        {
            this.view.UpdateStudentLinkStatus(obj);
        }
    }
}
