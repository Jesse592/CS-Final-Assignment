using Grading_Administration_Server.EntityFramework.models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Helper
{
    static class JSONHelperServer
    {

        /// <summary>
        /// Converst a json object and retreives the userID
        /// </summary>
        /// <param name="json">The json object</param>
        /// <returns>The user id in the json</returns>
        public static int GetIDFromJSON(JObject json, string path)
        {

            //Try to get the ID value from the JObject
            //Returns null when token not found
            JToken userIDToken = json.SelectToken(path);

            // Ignoring message when not correct format, -1 selected as false format
            if (userIDToken == null) return -1;

            return (int)userIDToken;
        }

        /// <summary>
        /// Transforms a list of users to a list of sharedusers.
        /// Sharedusers are needed to prevent sending the client sensitive data
        /// </summary>
        /// <param name="users">the users to convert</param>
        /// <returns>The list of shared grades</returns>
        public static List<Grading_Administraton_Shared.Entities.User> UserToShared(List<User> users)
        {
            // converting the users to shared and filling the list
            var newUsers = new List<Grading_Administraton_Shared.Entities.User>();
            users.ForEach(u=> newUsers.Add(u.ToSharedUser()));

            return newUsers;
        }


        /// <summary>
        /// Transforms a list grades to a list of sharedGrades.
        /// Sharedgrades are needed to prevent sending the client sensitive data
        /// </summary>
        /// <param name="grades">the grades to ben converted</param>
        /// <returns>The list of shared grades</returns>
        public static List<Grading_Administraton_Shared.Entities.Grade> GradesToShared(List<Grade> grades)
        {
            // converting the grades to shared and filling the list
            var newGrades = new List<Grading_Administraton_Shared.Entities.Grade>();
            grades.ForEach(g => newGrades.Add(g.ToSharedGrade()));

            return newGrades;
        }

        /// <summary>
        /// Transforms a list modules to a list of sharedModues.
        /// Sharedmodules are needed to prevent sending the client sensitive data
        /// </summary>
        /// <param name="modules">the modules to ben converted</param>
        /// <returns>The list of shared grades</returns>
        public static List<Grading_Administraton_Shared.Entities.Module> GradesToShared(List<Module> modules)
        {
            // converting the grades to shared and filling the list
            var newModules = new List<Grading_Administraton_Shared.Entities.Module>();
            modules.ForEach(m => newModules.Add(m.ToSharedModule()));

            return newModules;
        }
    }
}
