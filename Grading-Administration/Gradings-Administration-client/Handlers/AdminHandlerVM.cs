
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.view.UpdateStatus(obj);
        }

        public void NewModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("CreateNewModule", JSONWrapper.WrapModule(m))), NewModuleCallback);
        }

        public void GetAllUsers()
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("GetUsers", new JObject())), GetAllUsersCallback);
        }

        public void GetAllUsersCallback(JObject obj)
        {
            List<User> UserList = new List<User>();
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

            this.view.UserList = UserList;
        }

        public void NewModuleCallback(JObject obj)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void AddTeacherToModule(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddTeacherToModule", JSONWrapper.WrapUser(u))), AddTeacherToModule);
        }

        public void AddTeacherToModule(JObject obj)
        {
            throw new NotImplementedException();
        }

        public void AddStudentToModule(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("AddStudentToModule", JSONWrapper.WrapUser(u))), AddStudentCallback);
        }

        public void AddStudentCallback(JObject obj)
        {
            throw new NotImplementedException();
        }
    }
}
