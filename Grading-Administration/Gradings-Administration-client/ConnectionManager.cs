
using Grading_Administration_Shared.Communication;
using Grading_Administration_Shared.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace GradingAdmin_client
{
    class ConnectionManager
    {
        private static ConnectionManager Connection = null;
        public static ConnectionManager GetConnectionManager()
        {
            if (Connection == null)
            {
                Connection = new ConnectionManager();
            }

            return Connection;
        }

        private TCPHandler TCPHandler;
        private Dictionary<int, Action<JObject>> SerialCodes;
        private Action<JObject> HandleUnwantedData;
        private int Serialcode = 0;

        public ConnectionManager()
        {
            TcpClient connection = new TcpClient("127.0.0.1", 6969);
            this.TCPHandler = new TCPHandler(connection.GetStream());

            this.SerialCodes = new Dictionary<int, Action<JObject>>();
            this.TCPHandler.OnDataReceived += OnMessageReceived;
        }

        public void OnMessageReceived(object sender, string data)
        {
            if (data == null | data == "")
                return;

            // Retursn null is string is invalid json
            JObject jobject = JObject.Parse(data);

            HandleReceived(jobject);
        }

        public void HandleReceived(JObject data)
        {
            JToken serial;

            bool canParse = data.TryGetValue("Serial", out serial);

            if (!canParse || serial == null)
                return;

            int code = serial.ToObject<int>();

            if (this.SerialCodes.ContainsKey(code))
                this.SerialCodes[code]?.Invoke(data);
        }

        public void SendCommand(JObject jObject)
        {
            this.TCPHandler.SendMessage(jObject.ToString());
        }

        public void SendCommand(JObject jObject, Action<JObject> callback)
        {
            this.SerialCodes.Add(this.Serialcode, callback);
            this.Serialcode++;

            SendCommand(jObject);
        }
    }
}
