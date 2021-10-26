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
            
            User user = db.Users.Find(7);

            List<ModuleContribution> l = new List<ModuleContribution>();
            Module cs = new Module("C#", DateTime.Now, DateTime.Now, 2, true, l);

            ModuleContribution mc = new ModuleContribution(user, cs, new List<Grade>());

            Grade rg = new Grade(mc, DateTime.Now, 4.0, "O", 5.5);
            mc.grades.Add(rg);
            cs.Participants.Add(mc);
            user.Modules.Add(mc);

            user.Modules.Add(mc);

            db.SaveChanges();
            Console.WriteLine("rrr");*/

           Server server = new Server(Setting.IPAdress, Setting.PortNumber);

           server.RunServer();


           Console.ReadKey();
        }
    }
}
