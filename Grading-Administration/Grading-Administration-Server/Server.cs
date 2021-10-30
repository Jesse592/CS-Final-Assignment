using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using GradingAdministration_server;

namespace Grading_Administration_Server
{
    /// <summary>
    /// Handles running the server
    /// </summary>
    class Server
    {
        private bool running;

        private TcpListener tcpListner;
        private List<ClientConnection> clients;

        /// <summary>
        /// Constructor for the server
        /// </summary>
        /// <param name="ip">The IP to run on</param>
        /// <param name="port">The port to run on</param>
        public Server(string ip, int port)
        {
            tcpListner = new TcpListener(System.Net.IPAddress.Parse(ip), port);
            clients = new List<ClientConnection>();
        }

        /// <summary>
        /// Starts running the server
        /// </summary>
        public void RunServer()
        {
            // Set the server running
            this.running = true;

            Console.WriteLine("Server: started listening for clients");

            // Start the server
            tcpListner.Start();
            tcpListner.BeginAcceptTcpClient(new AsyncCallback(HandleClient), null);
        }

        /// <summary>
        /// When a client request a connection this method starts it up
        /// </summary>
        /// <param name="result">The user request to connect</param>
        private void HandleClient(IAsyncResult result)
        {
            TcpClient tcpClient = tcpListner.EndAcceptTcpClient(result);

            ClientConnection client = new ClientConnection(tcpClient);
            clients.Add(client);

            //Start listening for clients again
            if(this.running)
                tcpListner.BeginAcceptTcpClient(new AsyncCallback(HandleClient), null);
        }

        /// <summary>
        /// Stops the server and all clients
        /// </summary>
        public void StopServer()
        {
            this.running = false;

            // Stopping all clients
            foreach (ClientConnection client in clients)
                client.Stop();
        }
    }
}
