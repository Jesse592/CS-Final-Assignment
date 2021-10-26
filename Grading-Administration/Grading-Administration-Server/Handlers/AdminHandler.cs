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
        private async void GetStudents(int serial)
        {
            // Getting all the students
            List<User> students = await (from user in this.GradingDBContext.Users
                                         where user.UserType == ((int)UserType.STUDENT).ToString()
                                         select user).ToListAsync();

            // transforming the list to shared users
            var studentsShared = JSONHelperServer.UserToShared(students);

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllUsers(studentsShared, serial)));
        }

        /// <summary>
        /// Returns all the modules that are saved in the database
        /// </summary>
        /// <param name="serial">The ID-code from the client</param>
        private void GetModules(int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given new user data, method creates a new user in the database
        /// </summary>
        /// <param name="data">The data for the new user, shared.User</param>
        /// <param name="serial">The ID-code from the client</param>
        private void CreateNewUser(JObject data, int serial)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Given new module data, method creates a new module in the database
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serial">The ID-code from the client</param>
        private void CreateNewModule(JObject data, int serial)
        {
            throw new NotImplementedException();
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
            this.Actions.Add("GetStudents", (j, s) => GetStudents(s));
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
