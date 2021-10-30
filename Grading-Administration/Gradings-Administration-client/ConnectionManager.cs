
using Grading_Administration_Shared.Communication;
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
        private int Serialcode = 0;

        private ConnectionManager()
        {
            try
            {
                TcpClient connection = new TcpClient("127.0.0.1", 6969);

                this.TCPHandler = new TCPHandler(connection.GetStream());
                this.TCPHandler.OnDataReceived += OnMessageReceived;
            } catch(Exception e)
            {
                Console.WriteLine("Error in connection setup");
            }
            
            this.TCPHandler?.SetRunning(true);

            this.SerialCodes = new Dictionary<int, Action<JObject>>();
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

            bool canParse = data.TryGetValue("serial", out serial);

            if (!canParse || serial == null)
                return;

            int code = serial.ToObject<int>();

            if (this.SerialCodes.ContainsKey(code))
                this.SerialCodes[code]?.Invoke(data);
        }

        public void SendCommand(JObject jObject)
        {
            this.TCPHandler?.SendMessage(jObject.ToString());
        }

        public void SendCommand(JObject jObject, Action<JObject> callback)
        {
            this.SerialCodes.Add(this.Serialcode, callback);

            jObject.Add("serial", JToken.FromObject(this.Serialcode));

            this.Serialcode++;

            SendCommand(jObject);
        }
    }
}
