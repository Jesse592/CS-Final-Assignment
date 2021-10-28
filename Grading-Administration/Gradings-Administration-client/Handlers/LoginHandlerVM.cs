using Grading_Administration_Server.EntityFramework.models;
using Grading_Administraton_Shared.Entities;
using GradingAdmin_client.ViewModels;
using Gradings_Administration_client.Commands;
using Gradings_Administration_client.FileIO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GradingAdmin_client.Handlers
{
    class LoginHandlerVM
    {
        private ConnectionManager manager;
        private LoginViewModel vm;
        public ICommand UpdateViewCommand { get; set; }

        public LoginHandlerVM(LoginViewModel view)
        {
            this.vm = view;
            UpdateViewCommand = new UpdateViewCommand(view);
            this.manager = ConnectionManager.GetConnectionManager();

            LoadUsername();
        }

        private async void LoadUsername()
        {
            string username = await FileReadWriter.ReadUsernameAsync();

            // Checking if username is valid
            if (username == null || username == "Not Saved") return;

            this.vm.UserName = username;
        }

        public async void Login(string username, string password)
        {
            // Building and sending the login command to the server
            this.manager.SendCommand(JObject.FromObject(JSONWrapper.WrapHeader("login", JSONWrapper.WrapLogin(password, username))), LoginCallback);

            // Checking if the user wants to save username
            string usernameToSave = this.vm.SavePassword ? username : "Not Saved";
            
            bool saveSuccesfull = await FileReadWriter.SaveUserNameAsync(usernameToSave);
            
            if (!saveSuccesfull)
                Console.WriteLine("Username not saved correctly");

        }

        public void LoginCallback(JObject Jobject)
       {
            if (Jobject.SelectToken("data.message").ToString() == "Failed login")
            {
                this.vm.ShowError();
            } 
            else if (Jobject.SelectToken("data.message").ToString() == "Succesfull login") 
            {
                JToken UserID = Jobject.SelectToken("data.user.UserId");
                JToken FirstName = Jobject.SelectToken("data.user.FirstName");
                JToken LastName = Jobject.SelectToken("data.user.LastName");
                JToken DateOfBirth = Jobject.SelectToken("data.user.DateOfBirth");
                JToken Email = Jobject.SelectToken("data.user.Email");
                JToken type = Jobject.SelectToken("data.user.UserType");

                string stringtype = type.Value<String>();

                User u = new User(UserID.Value<Int32>(), FirstName.Value<String>(), LastName.Value<String>(), DateOfBirth.Value<DateTime>(), Email.Value<String>(), stringtype);

                this.vm.UpdateViewModel(u);
            }
        }
    }
}
