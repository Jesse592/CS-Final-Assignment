
using Grading_Administraton_Shared.Entities;
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

        public AdminHandlerVM()
        {
            this.manager = ConnectionManager.GetConnectionManager();
        }

        public void NewUser(User u)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("NewUser", JSONWrapper.WrapUser(u))), NewUSerCallback);
        }

        public void NewUSerCallback(JObject obj)
        {
            
        }

        public void NewModule(Module m)
        {
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("NewModule", JSONWrapper.WrapModule(m))), NewModuleCallback);
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
            throw new NotImplementedException();
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
