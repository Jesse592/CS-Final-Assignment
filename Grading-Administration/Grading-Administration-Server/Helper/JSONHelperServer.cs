using Grading_Administration_Server.EntityFramework.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Helper
{
    /// <summary>
    /// Static class that holds method that are used multiple times to parse JSON data to needed data
    /// </summary>
    public static class JSONHelperServer
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
        public static List<Grading_Administraton_Shared.Entities.Module> ModulesToShared(List<Module> modules)
        {
            // converting the grades to shared and filling the list
            var newModules = new List<Grading_Administraton_Shared.Entities.Module>();
            modules.ForEach(m => newModules.Add(m.ToSharedModule()));

            return newModules;
        }

        // <summary>
        /// Newtonsoft does not contain tryParse methodes, it chrases when a invalid string is given
        /// </summary>
        /// <param name="message">The string to be parsed to JSON</param>
        /// <param name="jObject">The object that was parsed</param>
        /// <returns>True if succesfull, otherwise false</returns>
        public static bool TryParse(this JObject jObject, string message, out JObject parsedObject)
        {
            parsedObject = null;

            // Checking some values that can be check witgout try-catch
            if (string.IsNullOrWhiteSpace(message)) return false;
            if (!message.StartsWith("{") || !message.EndsWith("}")) return false;

            try
            {
                parsedObject = JsonConvert.DeserializeObject(message) as JObject;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in parsing json, not valid");
                return false;
            }
        }
    }
}
