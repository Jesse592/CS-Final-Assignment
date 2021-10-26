using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Grading_Administration_Server.Settings;
using System;
using System.Collections.Generic;

namespace Grading_Administration_Server
{
    class Program
    {
        static void Main(string[] args)
        {

            /*GradingDBContext db = new GradingDBContext();
            
            User user = new User()
            {
                FirstName = "Jesse",
                LastName = "Krijgsman",
                DateOfBirth = DateTime.Parse("2002-04-14 13:26"),
                Email = "JesseKrijgsman@hotmail.nl",
                UserType = "0",
                Modules = new List<ModuleContribution>()
            };

            List<ModuleContribution> l = new List<ModuleContribution>();
            Module dcn = new Module("Data communicatie en netwerken", DateTime.Now, DateTime.Now, 3, true, l);

            ModuleContribution mc = new ModuleContribution(user, dcn, new List<Grade>());

            Grade rg = new Grade(mc, DateTime.Now, 8, "G", 5.5);
            mc.grades.Add(rg);
            dcn.Participants.Add(mc);
            user.Modules.Add(mc);

            LoginDetail lg = new LoginDetail()
            {
                User = user,
                UserName = "Jesse",
                Password = "root"
            };


            db.Users.Add(user);
            db.Modules.Add(dcn);
            db.LoginDetails.Add(lg);
            db.moduleContributions.Add(mc);
            db.grades.Add(rg);

            db.SaveChanges();
            Console.WriteLine("rrr");*/

           Server server = new Server(Setting.IPAdress, Setting.PortNumber);

           server.RunServer();


           Console.ReadKey();
        }
    }
}
