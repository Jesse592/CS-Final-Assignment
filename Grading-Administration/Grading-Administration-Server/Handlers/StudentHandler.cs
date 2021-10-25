using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Handlers
{
    class StudentHandler : Handler
    {
        private readonly GradingDBContext GradingDBContext;

        public StudentHandler(GradingDBContext gradingDBContext) : base()
        {
            GradingDBContext = gradingDBContext;
        }

        public void GetGrades(JObject student, int serial)
        {

        }

        public void GetAllGrades(JObject data, int serial)
        {
            Console.WriteLine(data);

            // Getting the userID
            int userID = JsonToUser(data);

            // Ignoring if the userID is -1
            if (userID == -1) return;

            // 
        }
        
        public void GetModules(JObject student, int serial)
        {

        }

        private int JsonToUser(JObject json)
        {

            //Try to get the userID value from the JObject
            //Returns null when token not found
            JToken userIDToken = json.SelectToken("user.UserId");

            // Ignoring message when not correct format, -1 selected as false format
            if (userIDToken == null) return -1;

            return (int)userIDToken;
        } 

        protected override void Init()
        {
            this.Actions.Add("GetGrades", GetGrades);
            this.Actions.Add("GetAllGrades", GetAllGrades);
            this.Actions.Add("GetModules", GetModules);
        }
    }
}
