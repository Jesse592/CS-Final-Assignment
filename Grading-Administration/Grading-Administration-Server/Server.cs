using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GradingAdministration_server
{
    class Server
    {
        private bool Running;
        private string Ip { get; set; }
        private int Port { get; set; }
        private TcpListener tcpListner;
        private List<ClientConnection> clients;

        public Server(string ip, int port)
        {
            this.Running = true;
            this.Ip = ip;
            this.Port = port;
            this.tcpListner = new TcpListener(System.Net.IPAddress.Parse(this.Ip), this.Port);
            this.clients = new List<ClientConnection>();
        }

        public void RunServer()
        {
            this.tcpListner.Start();
            this.tcpListner.BeginAcceptTcpClient(new AsyncCallback(HandleClient), null);
        }

        private void HandleClient(IAsyncResult result)
        {
            try
            {
                TcpClient tcpClient = this.tcpListner.EndAcceptTcpClient(result);
                ClientConnection client = new ClientConnection(tcpClient);
                this.clients.Add(client);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void StopServer()
        {
            foreach (ClientConnection client in this.clients)
            {
                client.Stop();
            }
        }
    }
}
