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
        public static int JsonToUser(JObject json)
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
        public static List<Grading_Administraton_Shared.Entities.Grade> GradesToShared(List<Grade> grades)
        {
            // converting the grades to shared and filling the list
            var newGrades = new List<Grading_Administraton_Shared.Entities.Grade>();
            grades.ForEach(g => newGrades.Add(g.ToSharedGrade()));

            return newGrades;
        }
    }
}
