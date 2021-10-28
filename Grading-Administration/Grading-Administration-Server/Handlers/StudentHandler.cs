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
    /// <summary>
    /// Class that extends Handler. It handles all the command for the STUDENT usertype
    /// </summary>
    class StudentHandler : Handler
    {
        private readonly GradingDBContext GradingDBContext;
        private User user;

        /// <summary>
        /// Constructor for the StudentHandler
        /// </summary>
        /// <param name="gradingDBContext">The database context the class can use</param>
        /// <param name="user">The user that was connected to this handler</param>
        /// <param name="sendAction">The action where the results are send to</param>
        public StudentHandler(GradingDBContext gradingDBContext, User user, Action<JObject> sendAction) : base(sendAction)
        {
            GradingDBContext = gradingDBContext;
            this.user = user;
        }

        /// <summary>
        /// Method that gives all the modules with all the grades of a given user
        /// </summary>
        /// <param name="data">The json data that has the user</param>
        /// <param name="serial">The id-code given by the client</param>
        public async Task GetAllGrades(JObject data, int serial)
        {
            // Getting the userID
            int userID = JSONHelperServer.GetIDFromJSON(data, "user.UserId");

            // Ignoring if the userID is -1 and given user is not the correct user
            if (userID == -1 || userID != this.user?.UserId) return;

            // Searching the database to get all the grades of the user
            List<ModuleContribution> grades = await (from dt in this.GradingDBContext.moduleContributions
                                                     where dt.User.UserId == userID
                                                     select dt).ToListAsync();

            // We need to send all grades per module + the module id, so a dictionary
            var gradesList = new List<object>();

            // filling the dictionary with the shared (save) version of the objects
            foreach(ModuleContribution mc in grades)
            {
                var mcGrades = JSONHelperServer.GradesToShared(mc.grades?.ToList());
                List<User> teachers = await GetTeachersWithModule(mc);

                // transforming the list to shared users
                var sharedTeachers = new List<Grading_Administraton_Shared.Entities.User>();
                teachers.ForEach(t => sharedTeachers.Add(t.ToSharedUser()));

                // The objects in the list follow this structure:
                // first module, than a list of the grades
                gradesList.Add(new
                {
                    module = mc.Module.ToSharedModule(),
                    teachers = sharedTeachers,
                    mcGrades = mcGrades
                }
                );
            }

            // Converting to json and sending to client
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GenericList("GetAllGrades", gradesList, serial)));
        }
        
        /// <summary>
        /// This method will get all the modules a user is enrroled in.
        /// Checks is the userIDs match before fetching the data
        /// </summary>
        /// <param name="data">The json that contains the student</param>
        /// <param name="serial">The ID-code given by the client</param>
        public Task GetModules(JObject data, int serial)
        {
            // Getting the userID
            int userID = JSONHelperServer.GetIDFromJSON(data, "user.UserId");

            // Ignoring if the userID is -1 and given user is not the correct user
            if (userID == -1 || userID != user?.UserId)
                return Task.CompletedTask;

            // Create a list with shared Modules to be sent to the user
            var modules = new List<Grading_Administraton_Shared.Entities.Module>();

            foreach(ModuleContribution m in user?.Modules)
            {
                modules.Add(m.Module.ToSharedModule());
            };

            SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllModules(modules, serial)));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Returns all the teachers connected to a given module
        /// </summary>
        /// <param name="module">The module to get the teachers of</param>
        private async Task<List<User>> GetTeachersWithModule(ModuleContribution module)
        {
            // getting all the users that are in the module AND are marked as teacher
            return await (from tc in this.GradingDBContext.Users
                          join m in this.GradingDBContext.moduleContributions on tc.UserId equals m.UserId
                          where tc.UserType == ((int)UserType.TEACHER).ToString() && m.ModuleId == module.ModuleId  
                          select tc).ToListAsync();            
        }

        /// <summary>
        /// Sets up all the commands this handler can handle
        /// </summary>
        protected override void Init()
        {
            this.Actions.Add("GetAllGrades", GetAllGrades);
            this.Actions.Add("GetModules", GetModules);
        }
    }
}
