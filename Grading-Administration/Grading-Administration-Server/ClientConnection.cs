using Grading_Administration_Server.Communication;
using Grading_Administration_Server.EntityFramework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server
{
    class ClientConnection
    {
        private GradingDBContext GradingDBContext;
        private TcpClient client;

        private TCPHandler TCPHandler;
        private bool running = false;

        public ClientConnection(TcpClient client)
        {
            Console.WriteLine("Connected to client");

            // Creating a new DB Context per client, te ensure the context is not open 24/7
            this.GradingDBContext = new GradingDBContext();
            
            this.client = client;

            this.TCPHandler = new TCPHandler(this.client.GetStream());

            // Setting the onMessage event
            this.TCPHandler.OnDataReceived += OnMessageReceived;
            this.TCPHandler.SetRunning(true);

            this.running = true;
        }

        public void OnMessageReceived(object sender, string message)
        {
            Console.WriteLine($"Received message on client: {message}");
            this.TCPHandler.SendMessage($"Received message on client: {message}");
        }

        public void HandleData()
        {

        }

        public void HandleLogin(JObject LoginDetails)
        {

        }

        public void SendMessage(JObject data)
        {

        }


        public void Stop()
        {
            this.running = false;
            this.TCPHandler.SetRunning(false);
            this.client.Close();
        }

    }
}
