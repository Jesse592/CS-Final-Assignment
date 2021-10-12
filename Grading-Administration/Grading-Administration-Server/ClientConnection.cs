using Grading_Administration_Server.Communication;
using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Shared.Util;
using Newtonsoft.Json;
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
            this.TCPHandler = new TCPHandler(client.GetStream());

            // Setting the onMessage and onError event
            this.TCPHandler.OnDataReceived += OnMessageReceived;
            this.TCPHandler.OnConectionError += (s, d) => Console.WriteLine($"Error in the connection: {d}");

            this.TCPHandler.SetRunning(true);

            this.running = true;
        }

        /// <summary>
        /// Method is called when data is recieved from the client (OnDataReceived event)
        /// </summary>
        public void OnMessageReceived(object sender, string message)
        {
            // Checking in the message is valid
            if (message == null | message == "")
                return;

            // Retursn null is string is invalid json
            JObject jobject = new JObject();

            bool validJSON = jobject.TryParse(message, out jobject);

            // Message is ignored if not a valid json
            if (!validJSON || jobject == null) return;

            HandleData(jobject);
        }

        /// <summary>
        /// Method handles the incoming data from the OnMessageReceived method
        /// </summary>
        private void HandleData(JObject data)
        {
            // command value always gives the action 
            JToken command;

            bool correctCommand = data.TryGetValue("command", StringComparison.InvariantCulture, out command);

            if (!correctCommand) return;
 
            // This class only handles login
        }

        private void HandleLogin(JObject LoginDetails)
        {

        }

        public void SendMessage(JObject data)
        {
            string dataString = data.ToString();

            this.TCPHandler?.SendMessage(dataString);
        }


        public void Stop()
        {
            this.running = false;
            this.TCPHandler.SetRunning(false);
            this.client.Close();
        }

    }
}
