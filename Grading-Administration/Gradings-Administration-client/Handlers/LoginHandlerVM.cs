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
    /// <summary>
    /// Class that hanles all tasks needed for login
    /// </summary>
    class LoginHandlerVM
    {
        private ConnectionManager manager;
        
        private LoginViewModel vm;
        public ICommand UpdateViewCommand { get; set; }

        /// <summary>
        /// Construtor for the LoginHandlerVM
        /// </summary>
        /// <param name="view">The viewmodel</param>
        public LoginHandlerVM(LoginViewModel view)
        {
            this.vm = view;
            UpdateViewCommand = new UpdateViewCommand(view);

            // Getting the active connection manager
            this.manager = ConnectionManager.GetConnectionManager();

            // Getting the saved username on startup
            LoadUsername();
        }

        /// <summary>
        /// Async method that gets the saced username
        /// Sets the username in the viewmodel to the loaded username
        /// </summary>
        private async void LoadUsername()
        {
            string username = await FileReadWriter.ReadUsernameAsync();

            // Checking if username is valid
            if (username == null || username == "Not Saved") return;

            this.vm.UserName = username;
        }

        /// <summary>
        /// Mehtod that handles sending the login command to the server
        /// and saving the username to a file if the user checked the box
        /// </summary>
        /// <param name="username">The entered username</param>
        /// <param name="password">The entered passqword</param>
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

        /// <summary>
        /// Mehtod that is called by the Connection manager when the server anwsers the login request
        /// </summary>
        /// <param name="Jobject">The anwser from the server</param>
        public void LoginCallback(JObject Jobject)
       {
            // Checking if the login failed / succeded
            if (Jobject.SelectToken("data.message").ToString() == "Failed login")
            {
                // Showing the user the login failed
                this.vm.ShowError();
            } 
            else if (Jobject.SelectToken("data.message").ToString() == "Succesfull login") 
            {
                // Parsing all the user data in the login
                JToken UserID = Jobject.SelectToken("data.user.UserId");
                JToken FirstName = Jobject.SelectToken("data.user.FirstName");
                JToken LastName = Jobject.SelectToken("data.user.LastName");
                JToken DateOfBirth = Jobject.SelectToken("data.user.DateOfBirth");
                JToken Email = Jobject.SelectToken("data.user.Email");
                JToken type = Jobject.SelectToken("data.user.UserType");

                string stringtype = type.Value<String>();

                // Creating the user
                User u = new User(UserID.Value<Int32>(), FirstName.Value<String>(), LastName.Value<String>(), DateOfBirth.Value<DateTime>(), Email.Value<String>(), stringtype);

                // Updatating the viewmodel
                this.vm.UpdateViewModel(u);
            }
        }
    }
}
