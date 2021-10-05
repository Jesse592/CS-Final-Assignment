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

        }
    }
}
