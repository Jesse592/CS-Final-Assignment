using Grading_Administration_Server.Communication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdmin_client
{
    class ConnectionManager
    {
        private TCPHandler Handler;
        private Dictionary<int, Action<JObject>> SerialCodes;
        private Action<JObject> HandleUnwantedData;

        public ConnectionManager()
        {
            Handler = new TCPHandler();
        }

        public void OnMessageReceived()
        {

        }

        public void HandleReceived()
        {

        }

        public void SendCommand(JObject jObject)
        {

        }

        public void SendCommand(JObject jObject, Action<JObject> callback)
        {

        }
    }
}
