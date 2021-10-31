
using Grading_Administration_Shared.Communication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace GradingAdmin_client
{
    /// <summary>
    /// Class handles all the needed communcation between a server and all other classes.
    /// The server is transpartend to all other classes that use the connection manager
    /// </summary>
    public class ConnectionManager
    {
        #region Singleton pattern
        //# Singleton pattern, class is created only if it doesn't exists.
        //# Else the same object is given for transparant communication
        private static ConnectionManager Connection = null;
        public static ConnectionManager GetConnectionManager()
        {
            if (Connection == null)
            {
                Connection = new ConnectionManager();
            }

            return Connection;
        }
        #endregion

        private TCPHandler TCPHandler;
        private Dictionary<int, Action<JObject>> SerialCodes;
        private int Serialcode = 0;

        /// <summary>
        /// This class handles all the data between the server and the client
        /// Class uses singleton pattern, only one can exist
        /// </summary>
        private ConnectionManager()
        {
            try
            {
                // Trying to start the connection to the server
                TcpClient connection = new TcpClient("127.0.0.1", 6969);

                // Creating a TCP handler to start the TCP connection to the server
                this.TCPHandler = new TCPHandler(connection.GetStream());

                // Setting the onDataReceived event to the corresponding method
                this.TCPHandler.OnDataReceived += OnMessageReceived;
            } catch(Exception e)
            {
                Console.WriteLine("Error in connection setup:{0}", e.Message);
            }
            
            // starting the TCP handler
            this.TCPHandler?.SetRunning(true);

            this.SerialCodes = new Dictionary<int, Action<JObject>>();
        }

        /// <summary>
        /// Event method that is called when the TCP handler receives a message from the server
        /// </summary>
        /// <param name="sender">The object that called the event</param>
        /// <param name="data">The data that was received from the server</param>
        public void OnMessageReceived(object sender, string data)
        {
            // Checking for null of empty data string
            if (data == null | data == "")
                return;

            // Returns null is string is invalid json
            JObject jobject = JObject.Parse(data);

            // Checking if parse was succesfull
            if (jobject == null) return;

            // Calling the method to read the JSON
            HandleReceived(jobject);
        }

        /// <summary>
        /// Method handles a JSON object that was received from the server
        /// </summary>
        /// <param name="data">The JSON data reveived from the server</param>
        public void HandleReceived(JObject data)
        {
            JToken serial;

            // Trying to get the serial value
            bool canParse = data.TryGetValue("serial", out serial);

            // Returning of the TryGet failed
            if (!canParse || serial == null)
                return;

            int code = serial.ToObject<int>();

            // If the command ID is in the SerialCodes list, if so invoke
            if (this.SerialCodes.ContainsKey(code))
                this.SerialCodes[code]?.Invoke(data);
        }

        /// <summary>
        /// Given a JSON object this file in converted to a string and send to the server
        /// </summary>
        /// <param name="jObject">The object to send to the server</param>
        public void SendCommand(JObject jObject)
        {
            this.TCPHandler?.SendMessage(jObject.ToString());
        }

        /// <summary>
        /// Same SendCommand method, however it includes a delegate that is invoked when data comes back
        /// </summary>
        /// <param name="jObject">The data to send to the server</param>
        /// <param name="callback">The method to be called when the server awnsers</param>
        public void SendCommand(JObject jObject, Action<JObject> callback)
        {
            // Adding the static current code and the delegate to the list
            this.SerialCodes.Add(this.Serialcode, callback);

            // Inserting the serial token in the JSON data
            jObject.Add("serial", JToken.FromObject(this.Serialcode));

            // Upping the value of the serial code with one
            this.Serialcode++;

            // Sending the command to the server
            SendCommand(jObject);
        }
    }
}
