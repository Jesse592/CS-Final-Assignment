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
        public async void GetAllGrades(JObject data, int serial)
        {
            // Getting the userID
            int userID = JsonToUser(data);

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
                var mcGrades = GradesToShared(mc.grades?.ToList());
                List<User> teachers = await GetTeachersWithModule(mc);
                // The objects in the list follow this structure:
                // first module, than a list of the grades
                gradesList.Add(new
                {
                    module = mc.Module.ToSharedModule(),
                    mcGrades
                }
                ); ;
            }

            // Converting to json and sending to client
            Console.WriteLine(JObject.FromObject(JSONWrapperServer.GetAllGrades(gradesList, serial)));
            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllGrades(gradesList, serial)));
        }
        
        /// <summary>
        /// This method will get all the modules a user is enrroled in.
        /// Checks is the userIDs match before fetching the data
        /// </summary>
        /// <param name="student">The json that contains the student</param>
        /// <param name="serial">The ID-code given by the client</param>
        public void GetModules(JObject data, int serial)
        {
            // Getting the userID
            int userID = JsonToUser(data);

            // Ignoring if the userID is -1 and given user is not the correct user
            if (userID == -1 || userID != this.user?.UserId) return;

            // Create a list with shared Modules to be sent to the user
            var modules = new List<Grading_Administraton_Shared.Entities.Module>();

            foreach(ModuleContribution m in this.user?.Modules)
            {
                modules.Add(m.Module.ToSharedModule());
            };

            this.SendAction?.Invoke(JObject.FromObject(JSONWrapperServer.GetAllModules(modules, serial)));
        }

        /// <summary>
        /// Returns all the teachers connected to a given module
        /// </summary>
        /// <param name="module">The module to get the teachers of</param>
        private async Task<List<User>> GetTeachersWithModule(ModuleContribution module)
        {
            List<User> teachers = await (from tc in this.GradingDBContext.Users
                                         join m in this.GradingDBContext.moduleContributions on tc.UserId equals m.UserId
                                         where tc.UserType == "1" && m.ModuleId == module.ModuleId  
                                         select tc).ToListAsync();

            Console.WriteLine(teachers);
            return teachers;
        }

        /// <summary>
        /// Converst a json object and retreives the userID
        /// </summary>
        /// <param name="json">The json object</param>
        /// <returns>The user id in the json</returns>
        private static int JsonToUser(JObject json)
        {

            //Try to get the userID value from the JObject
            //Returns null when token not found
            JToken userIDToken = json.SelectToken("user.UserId");

            // Ignoring message when not correct format, -1 selected as false format
            if (userIDToken == null) return -1;

            return (int)userIDToken;
        } 

        /// <summary>
        /// Transforms a list grades to a list of sharedGrades.
        /// Sharedgrades are needed to prevent sending the client sensitive data
        /// </summary>
        /// <param name="grades">the grades to ben converted</param>
        /// <returns>The list of shared grades</returns>
        private static List<Grading_Administraton_Shared.Entities.Grade> GradesToShared(List<Grade> grades)
        {
            // converting the grades to shared and filling the list
            var newGrades = new List<Grading_Administraton_Shared.Entities.Grade>();
            grades.ForEach(g => newGrades.Add(g.ToSharedGrade()));

            return newGrades;
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
