using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Grading_Administration_Server.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    class AdminHandler : Handler
    {
        private readonly GradingDBContext GradingDBContext;
        private User User;

        public AdminHandler(GradingDBContext gradingDBContext, User user, Action<JObject> sendAction) : base(sendAction)
        {
            this.GradingDBContext = gradingDBContext;
            this.User = user;
        }

        /// <summary>
        /// Returns all the students
        /// </summary>
        /// <param name="serial">The ID-code from the client</param>
        private async void GetUsers(int serial)
        {
            // transforming to shared users
            var studentsShared = JSONHelperServer.UserToShared(await this.GradingDBContext.Users.ToListAsync());

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllUsers(studentsShared, serial)));
        }

        /// <summary>
        /// Returns all the modules that are saved in the database
        /// </summary>
        /// <param name="serial">The ID-code from the client</param>
        private async void GetModules(int serial)
        {
            // transforming to shared modules
            var studentsShared = JSONHelperServer.ModulesToShared(await this.GradingDBContext.Modules.ToListAsync());

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllModules(studentsShared, serial)));
        }

        /// <summary>
        /// Given new user data, method creates a new user in the database
        /// </summary>
        /// <param name="data">The data for the new user, shared.User</param>
        /// <param name="serial">The ID-code from the client</param>
        private async void CreateNewUser(JObject data, int serial)
        {
            // Getting the values from the json data
            string fname = data.SelectToken("FirstName")?.ToString();
            string lname = data.SelectToken("LastName")?.ToString();
            string email = data.SelectToken("Email")?.ToString();
            string userType = data.SelectToken("UserType")?.ToString();
            DateTime dob = DateTime.Parse(data.SelectToken("DateOfBirth")?.ToString());

            // Checking if all required data is retreived
            if (fname == null || lname == null || email == null || userType == null) return;

            User user = new User()
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                UserType = userType,
                DateOfBirth = dob
            };

            this.GradingDBContext.Users.Add(user);

            // Saving the user to the database
            await this.GradingDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Given new module data, method creates a new module in the database
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serial">The ID-code from the client</param>
        private async void CreateNewModule(JObject data, int serial)
        {
            // Getting the values from the json data
            string name = data.SelectToken("Name")?.ToString();
            DateTime startDate = DateTime.Parse(data.SelectToken("StartDate")?.ToString());
            DateTime endDate = DateTime.Parse(data.SelectToken("EndDate")?.ToString());

            int etc = data.SelectToken("ETC").ToObject<int>();
            bool numerical = bool.Parse(data.SelectToken("IsNumerical").ToString());

            // Checking if all required data is retreived
            if (name == null) return;

            Module module = new Module()
            {
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                ETC = etc,
                IsNumerical = numerical
            };

            this.GradingDBContext.Modules.Add(module);

            // Saving the user to the database
            await this.GradingDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Given a user and module this method adds the user to the module
        /// </summary>
        /// <param name="data">The data for he module and user</param>
        /// <param name="serial">The ID-code from the client</param>
        private void AddUserToModule(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given user data this method removes that users data from the database
        /// </summary>
        /// <param name="data">The user ID to be deleted</param>
        /// <param name="serial">The ID-code from the client</param>
        private void DeleteUser(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given module data this method deletes the module
        /// </summary>
        /// <param name="data">The module data to be deleted</param>
        /// <param name="serial">The ID-code from the client</param>
        private void DeleteModule(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets up all the commands this handler can handle
        /// </summary>
        protected override void Init()
        {
            // Get actions
            this.Actions.Add("GetUsers", (j, s) => GetUsers(s));
            this.Actions.Add("GetModules", (j,s) => GetModules(s));

            // Create actions
            this.Actions.Add("CreateNewUser", CreateNewUser);
            this.Actions.Add("CreateNewModule", CreateNewModule);

            // Add actions
            this.Actions.Add("AddUserToModule", AddUserToModule);

            // Delete actions
            this.Actions.Add("DeleteUser", DeleteUser);
            this.Actions.Add("DeleteModule", DeleteModule);
        }
    }
}
