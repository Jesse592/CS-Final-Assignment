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

        public ConnectionManager(TCPHandler handler)
        {
            Handler = handler;
        }
    }
}
