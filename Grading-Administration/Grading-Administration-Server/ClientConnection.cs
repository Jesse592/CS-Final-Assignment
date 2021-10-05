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

        public ClientConnection(TcpClient client)
        {
            this.GradingDBContext = new GradingDBContext();
            this.client = client;
        }

        public static void OnMessageReceived()
        {

        }

        public static void HandleData()
        {

        }

        public static void HandleLogin(JObject LoginDetails)
        {

        }

        public static void SendMessage(JObject data)
        {

        }

        public void Stop()
        {
            this.client.Close();
        }
    }
}
