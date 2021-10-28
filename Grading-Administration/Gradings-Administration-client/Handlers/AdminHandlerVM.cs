
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
    class AdminHandlerVM
    {
        private ConnectionManager manager;
        private AdminViewModel view;

        public AdminHandlerVM(AdminViewModel view)
        {
            this.manager = ConnectionManager.GetConnectionManager();
            this.view = view;
        }

        public void NewUser(User u, string username, string password)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("CreateNewUser", JSONWrapper.WrapNewUser(u, username, password))), NewUSerCallback);
        }

        public void NewUSerCallback(JObject obj)
        {
            this.view.UpdateUserStatus(obj);
        }

        public void NewModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("CreateNewModule", JSONWrapper.WrapModule(m))), NewModuleCallback);
        }

        public void NewModuleCallback(JObject obj)
        {
            this.view.UpdateModuleStatus(obj);
        }

        public void GetAllUsers()
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetUsers", new JObject())), GetAllUsersCallback);
        }

        public void GetAllUsersCallback(JObject obj)
        {
            List<User> UserList = new List<User>();
            List<User> TeacherList = new List<User>();
            List<User> StudentList = new List<User>();

            JToken UserArray = obj.SelectToken("data") as JArray;

            foreach (JObject j in UserArray)
            {
                UserList.Add(new User(j.SelectToken("UserId").Value<int>(),
                    j.SelectToken("FirstName").Value<string>(),
                    j.SelectToken("LastName").Value<string>(),
                    j.SelectToken("DateOfBirth").Value<DateTime>(),
                    j.SelectToken("Email").Value<string>(),
                    j.SelectToken("UserType").Value<string>()));
            }

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

        public void DeleteUser(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("DeleteUser", JSONWrapper.WrapUser(u))), DeleteUserCallback);
        }

        public void DeleteUserCallback(JObject obj)
        {
            this.view.UpdateDeleteUserStatus(obj);
        }

        public void DeleteModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("DeleteModule", JSONWrapper.WrapModule(m))), DeleteModuleCallback);
        }

        public void DeleteModuleCallback(JObject obj)
        {
            this.view.UpdateDeleteModuleStatus(obj);
        }

        public void AddTeacherToModule(User u, Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddUserToModule", JSONWrapper.WrapModuleUser(m, u))), AddTeacherToModule);
        }

        public void AddTeacherToModule(JObject obj)
        {
            this.view.UpdateTeacherLinkStatus(obj);
        }

        public void AddStudentToModule(User u, Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddUserToModule", JSONWrapper.WrapModuleUser(m, u))), AddStudentCallback);
        }

        public void AddStudentCallback(JObject obj)
        {
            this.view.UpdateStudentLinkStatus(obj);
        }
    }
}
