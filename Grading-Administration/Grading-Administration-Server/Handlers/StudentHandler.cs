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
        private User user;

        public StudentHandler(GradingDBContext gradingDBContext, User user) : base()
        {
            GradingDBContext = gradingDBContext;
            this.user = user;
        }

        public void GetGrades(JObject student, int serial)
        {

        }

        public async void GetAllGrades(JObject data, int serial)
        {
            // Getting the userID
            int userID = jsonToUser(data);

            // Ignoring if the userID is -1 and given user is not the correct user
            if (userID == -1 || userID != this.user?.UserId) return;

            // Searching the database to get all the grades of the user
            List<ModuleContribution> grades = await (from dt in this.GradingDBContext.moduleContributions
                                                     where dt.User.UserId == userID
                                                     select dt).ToListAsync();

            // We need to send all grades per module, so a dictionary
            var gradesList = new Dictionary<Grading_Administraton_Shared.Entities.Module, List<Grading_Administraton_Shared.Entities.Grade>>();

            // filling the dictionary with the shared (save) version of the objects
            foreach(ModuleContribution mc in grades)
            {
                var mcGrades = gradesToShared(mc.grades.ToList());

                gradesList.Add(new Grading_Administraton_Shared.Entities.Module(mc.Module), mcGrades);
            }


        }
        
        public void GetModules(JObject student, int serial)
        {

        }

        private int jsonToUser(JObject json)
        {

            //Try to get the userID value from the JObject
            //Returns null when token not found
            JToken userIDToken = json.SelectToken("user.UserId");

            // Ignoring message when not correct format, -1 selected as false format
            if (userIDToken == null) return -1;

            return (int)userIDToken;
        } 

        private List<Grading_Administraton_Shared.Entities.Grade> gradesToShared(List<Grade> grades)
        {
            // converting the grades to shared and filling the list
            var newGrades = new List<Grading_Administraton_Shared.Entities.Grade>();
            grades.ForEach(g => newGrades.Add(new Grading_Administraton_Shared.Entities.Grade(g)));

            return newGrades;
        }

        protected override void Init()
        {
            this.Actions.Add("GetGrades", GetGrades);
            this.Actions.Add("GetAllGrades", GetAllGrades);
            this.Actions.Add("GetModules", GetModules);
        }
    }
}
