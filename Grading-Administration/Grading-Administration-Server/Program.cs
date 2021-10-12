using Grading_Administration_Server.Settings;
using System;

namespace Grading_Administration_Server
{
    class Program
    {
        static void Main(string[] args)
        {

           Server server = new Server(Setting.IPAdress, Setting.PortNumber);

           server.RunServer();


           Console.ReadKey();
        }
    }
}
