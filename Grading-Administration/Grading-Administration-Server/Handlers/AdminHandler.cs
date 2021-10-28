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
        private async Task GetUsers(int serial)
        {
            // transforming to shared users
            List<User> users = await this.GradingDBContext.Users.ToListAsync();

            var studentsShared = JSONHelperServer.UserToShared(users);

            // Sending it to the client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllUsers(studentsShared, serial)));
        }

        /// <summary>
        /// Returns all the modules that are saved in the database
        /// </summary>
        /// <param name="serial">The ID-code from the client</param>
        private async Task GetModules(int serial)
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
        private async Task CreateNewUser(JObject data, int serial)
        {
            // Getting the values from the json data
            string fname = data.SelectToken("user.FirstName")?.ToString();
            string lname = data.SelectToken("user.LastName")?.ToString();
            string email = data.SelectToken("user.Email")?.ToString();
            string userType = data.SelectToken("user.UserType")?.ToString();
            DateTime dob = DateTime.Parse(data.SelectToken("user.DateOfBirth")?.ToString());

            //Getting the default username and password
            string username = data.SelectToken("username")?.ToString();
            string password = data.SelectToken("password")?.ToString();

            // Checking if all required data is retreived, send failed if not
            if (fname == null || lname == null || email == null || userType == null || username == null || password == null)
            {
                this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeFailed(serial)));
                return;
            }

            User user = new User()
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                UserType = userType,
                DateOfBirth = dob
            };

            LoginDetail loginDetail = new LoginDetail()
            {
                UserId = user.UserId,
                User = user,
                UserName = username,
                Password = password
            };

            this.GradingDBContext.Users.Add(user);
            this.GradingDBContext.LoginDetails.Add(loginDetail);

            // Saving the user to the database
            await this.GradingDBContext.SaveChangesAsync();

            // Sending acknowledgement of succes to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeSucces(serial)));
        }

        /// <summary>
        /// Given new module data, method creates a new module in the database
        /// </summary>
        /// <param name="data"></param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task CreateNewModule(JObject data, int serial)
        {
            // Getting the values from the json data
            string name = data.SelectToken("module.Name")?.ToString();
            DateTime startDate = DateTime.Parse(data.SelectToken("module.StartDate")?.ToString());
            DateTime endDate = DateTime.Parse(data.SelectToken("module.EndDate")?.ToString());

            int etc = data.SelectToken("module.ETC").ToObject<int>();
            bool numerical = bool.Parse(data.SelectToken("module.IsNumerical").ToString());

            // Checking if all required data is retreived
            if (name == null)
            {
                this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeFailed(serial)));
                return;
            }

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

            // Sending acknowledgement of succes to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeSucces(serial)));
        }

        /// <summary>
        /// Given a user and module this method adds the user to the module
        /// </summary>
        /// <param name="data">The data for he module and user</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task AddUserToModule(JObject data, int serial)    
        {
            // Getting the module and user IDs
            int userID = JSONHelperServer.GetIDFromJSON(data, "UserId");
            int moduleID = JSONHelperServer.GetIDFromJSON(data, "ModuleId");

            // Chechking is they are valid (not -1)
            if (userID == -1 || moduleID == -1)
            {
                return;
            }
            // Getting the objects from the database
            User user = await this.GradingDBContext.Users.FindAsync(userID);
            Module module = await this.GradingDBContext.Modules.FindAsync(moduleID);

            // Checking if the objects can be found
            if (user == null || module == null) return;

            // Creating the contribution object
            ModuleContribution contribution = new ModuleContribution(user, module, new List<Grade>());

            this.GradingDBContext.moduleContributions.Add(contribution);

            await this.GradingDBContext.SaveChangesAsync();
        }

        /// <summary>
        /// Given user data this method removes that users data from the database
        /// </summary>
        /// <param name="data">The user ID to be deleted</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task DeleteUser(JObject data, int serial)
        {
            // Getting the userID
            int userID = JSONHelperServer.GetIDFromJSON(data, "user.UserId");

            // Checking if the value is oke
            if (userID == -1)
            {
                this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeFailed(serial)));
                return;
            }

            // Getting the user from the databse if exists
            User user = await this.GradingDBContext.Users.FindAsync(userID);

            if (user == null) return;

            this.GradingDBContext.Users.Remove(user);
            
            await this.GradingDBContext.SaveChangesAsync();

            // Sending acknowledgement of succes to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeSucces(serial)));
        }

        /// <summary>
        /// Given module data this method deletes the module
        /// </summary>
        /// <param name="data">The module data to be deleted</param>
        /// <param name="serial">The ID-code from the client</param>
        private async Task DeleteModule(JObject data, int serial)
        {
            // Getting the moduleID
            int moduleID = JSONHelperServer.GetIDFromJSON(data, "module.ModuleId");

            // Checking if the value is oke
            if (moduleID == -1)
            {
                this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeFailed(serial)));
                return;
            }

            // Getting the module from the databse if exists
            Module module = await this.GradingDBContext.Modules.FindAsync(moduleID);

            if (module == null) return;

            this.GradingDBContext.Modules.Remove(module);

            await this.GradingDBContext.SaveChangesAsync();

            // Sending acknowledgement of succes to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.AcknowledgeSucces(serial)));
        }

        /// <summary>
        /// Sets up all the commands this handler can handle
        /// </summary>
        protected override void Init()
        {
            // Get actions
            this.Actions.Add("GetUsers", async (j, s) => await GetUsers(s));
            this.Actions.Add("GetModules", async (j,s) => await GetModules(s));

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
