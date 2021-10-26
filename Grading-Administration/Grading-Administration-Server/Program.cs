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

            User talboom = new User()
            {
                FirstName = "Johan",
                LastName = "Talboom",
                DateOfBirth = DateTime.Parse("1984-07-14 13:26"),
                Email = "JohanTalboom@avans.nl",
                UserType = "1",
                Modules = new List<ModuleContribution>()
            };

            User hansie = new User()
            {
                FirstName = "Hans",
                LastName = "Van der Linden",
                DateOfBirth = DateTime.Parse("1972-07-14 13:26"),
                Email = "HJ.Linden@avans.nl",
                UserType = "1",
                Modules = new List<ModuleContribution>()
            };

            User twan = new User()
            {
                FirstName = "Twan",
                LastName = "Van Noorloos",
                DateOfBirth = DateTime.Parse("2002-05-08 08:26"),
                Email = "t.vanNoorloos@student.avans.nl",
                UserType = "0",
                Modules = new List<ModuleContribution>()
            };

            List<ModuleContribution> ogplijt = new List<ModuleContribution>();
            Module ogp = new Module("OGP0", DateTime.Parse("2020-08-01 00:00"), DateTime.Parse("2020-08-31 00:00"), 2, true, ogplijt);

            List<ModuleContribution> OOMlijt = new List<ModuleContribution>();
            Module OOM = new Module("OOM1", DateTime.Parse("2020-09-01 00:00"), DateTime.Parse("2020-10-12 00:00"), 3, true, OOMlijt);

            List<ModuleContribution> D2Glijt = new List<ModuleContribution>();
            Module D2G = new Module("2DG", DateTime.Parse("2021-2-12 00:00"), DateTime.Parse("2020-4-2 00:00"), 6, true, D2Glijt);


            /////// TWAN
            ModuleContribution twanogpmc = new ModuleContribution(twan, ogp, new List<Grade>());

            //Grade twanOGP = new Grade(twanogpmc, DateTime.Parse("2020-08-24 00:00"), 4.2, "O", 5.5);
            //Grade twanOGP2 = new Grade(twanogpmc, DateTime.Parse("2020-12-24 00:00"), 9.6, "G", 5.5);

            //twanogpmc.grades.Add(twanOGP);
            //twanogpmc.grades.Add(twanOGP2);
            ogp.Participants.Add(twanogpmc);
            twan.Modules.Add(twanogpmc);

            ModuleContribution twan2dgmc = new ModuleContribution(twan, D2G, new List<Grade>());

            //Grade twan3dg = new Grade(twanogpmc, DateTime.Parse("2021-04-4 00:00"), 8.1, "G", 5.5);
            //twan2dgmc.grades.Add(twan3dg);
            D2G.Participants.Add(twan2dgmc);
            twan.Modules.Add(twan2dgmc);

            ///////////
            ///
            ////////////// JOHAN

            ModuleContribution johanOGPmc = new ModuleContribution(talboom, ogp, new List<Grade>());

            ogp.Participants.Add(johanOGPmc);
            talboom.Modules.Add(johanOGPmc);

            ModuleContribution johan2DGmc = new ModuleContribution(talboom, D2G, new List<Grade>());

            D2G.Participants.Add(johan2DGmc);
            talboom.Modules.Add(johan2DGmc);

            ///////////////// hans
            ///
            ModuleContribution hansOOMmc = new ModuleContribution(hansie, OOM, new List<Grade>());

            OOM.Participants.Add(hansOOMmc);
            hansie.Modules.Add(hansOOMmc);

            ModuleContribution hans2dgmc = new ModuleContribution(hansie, D2G, new List<Grade>());

            D2G.Participants.Add(hans2dgmc);
            hansie.Modules.Add(hans2dgmc);

            LoginDetail lgjohan = new LoginDetail()
            {
                User = talboom,
                UserName = "Johan",
                Password = "root"
            };

            LoginDetail lghansie = new LoginDetail()
            {
                User = talboom,
                UserName = "Hans",
                Password = "root"
            };

            LoginDetail twanlg = new LoginDetail()
            {
                User = twan,
                UserName = "Twan",
                Password = "wachtwoord"
            };

            db.Users.Add(talboom);
            db.Users.Add(twan);
            db.Users.Add(hansie);

            db.LoginDetails.Add(lgjohan);
            db.LoginDetails.Add(lghansie);
            db.LoginDetails.Add(twanlg);

            db.Modules.Add(ogp);
            db.Modules.Add(D2G);
            db.Modules.Add(OOM);

            db.moduleContributions.Add(twanogpmc);
            db.moduleContributions.Add(twan2dgmc);
            db.moduleContributions.Add(johanOGPmc);
            db.moduleContributions.Add(johan2DGmc);
            db.moduleContributions.Add(hansOOMmc);
            db.moduleContributions.Add(hans2dgmc);

            //db.grades.Add(twanOGP);
            //db.grades.Add(twanOGP2);
            //db.grades.Add(twan3dg);
            

            ModuleContribution con = db.moduleContributions.Find(7);

            Grade twanOGP = db.grades.Find(5);
            Grade twanOGP2 = db.grades.Find(6);

            con.grades.Add(twanOGP);
            con.grades.Add(twanOGP2);

            db.SaveChanges();
            Console.WriteLine("rrr");*/

           Server server = new Server(Setting.IPAdress, Setting.PortNumber);

           server.RunServer();


           Console.ReadKey();
        }
    }
}
