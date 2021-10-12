using Grading_Administration_Server.EntityFramework.models;
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GradingAdmin_client.Handlers
{
    class LoginHandlerVM
    {
        private ConnectionManager manager;
        private LoginViewModel vm;

        public LoginHandlerVM()
        {
            this.manager = new ConnectionManager();
        }

        public void Login(string username, string password)
        {
            manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("Login", JSONWrapper.WrapLogin(password, username))), LoginCallback);
            
        }

        public void LoginCallback(JObject Jobject)
        {
            JToken UserID = Jobject.GetValue("UserID");
            JToken FirstName = Jobject.GetValue("FirstName");
            JToken LastName = Jobject.GetValue("LastName");
            JToken DateOfBirth = Jobject.GetValue("DateOfBirth");
            JToken Email = Jobject.GetValue("Email");
            JToken type = Jobject.GetValue("UserType");

            UserType UType = (UserType)type.Value<Int32>();
            string stringtype = UType.ToString();

            User u = new User(UserID.Value<Int32>(), FirstName.Value<String>(), LastName.Value<String>(), DateOfBirth.Value<DateTime>(), Email.Value<String>(), stringtype);

            vm.User = u;
        }
    }
}
