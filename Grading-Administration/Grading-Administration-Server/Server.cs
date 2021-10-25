using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using GradingAdministration_server;

namespace Grading_Administration_Server
{
    class Server
    {
        private bool running;

        private string ip { get; set; }
        private int port { get; set; }

        private TcpListener tcpListner;
        private List<ClientConnection> clients;

        public Server(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
            tcpListner = new TcpListener(System.Net.IPAddress.Parse(ip), port);
            clients = new List<ClientConnection>();
        }

        public void RunServer()
        {
            running = true;

            Console.WriteLine("Server: started listening for clients");

            tcpListner.Start();
            tcpListner.BeginAcceptTcpClient(new AsyncCallback(HandleClient), null);
        }

        private void HandleClient(IAsyncResult result)
        {
            TcpClient tcpClient = tcpListner.EndAcceptTcpClient(result);

            Console.WriteLine("NIEUWE CLIENT");

            ClientConnection client = new ClientConnection(tcpClient);
            clients.Add(client);

            //Start listening for clients again
            tcpListner.BeginAcceptTcpClient(new AsyncCallback(HandleClient), null);
        }

        public void StopServer()
        {
            this.running = false;

            foreach (ClientConnection client in clients)
                client.Stop();

        }
    }
}
