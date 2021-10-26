using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Grading_Administration_Server.Handlers;
using Grading_Administration_Shared.Communication;
using Grading_Administration_Server.Helper;
using Grading_Administration_Shared.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server
{
    class ClientConnection
    {
        private readonly GradingDBContext GradingDBContext;

        private Handler handler;

        private readonly TcpClient client;
        private readonly TCPHandler TCPHandler;

        private bool running = false;

        public ClientConnection(TcpClient client)
        {
            Console.WriteLine("Connected to client");

            // Creating a new DB Context per client, to ensure the context is not open 24/7
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
        /// Method handles the incoming data from the OnMessageReceived method.
        /// Where the OnMessageReceived method handles converting the message to a JObject, this method wil check if the object has
        /// the correct format.
        /// It handles the login command, all other commands all send to the this.handler
        /// </summary>
        private void HandleData(JObject data)
        {
            // command and data value always gives in the action
            // serial is optional
            JToken commandJson;
            JToken dataJson;
            JToken serialJson;

            bool correctCommand = data.TryGetValue("command", StringComparison.InvariantCulture, out commandJson);
            bool correctData = data.TryGetValue("data", StringComparison.InvariantCulture, out dataJson);
            bool correctSerial = data.TryGetValue("serial", StringComparison.InvariantCulture, out serialJson);

            // returning when JSON has wron format
            if (!correctCommand || !correctData) return;

            int serial;
            // Checking if serial is given, -1 if not
            serial = correctSerial ? (int)serialJson : -1;

            // This class only handles login
            if (commandJson.ToString() == "login")
                HandleLogin(data);
            
            // Sending this command to the handler, if client is not logged in handler will be null
            this.handler?.Invoke(commandJson.ToString(), dataJson as JObject, serial);
        }

        /// <summary>
        /// Handles the login command send by the user
        /// </summary>
        /// <param name="loginDetails">The command sent by the client to login</param>
        private async void HandleLogin(JObject loginDetails)
        {
            Console.WriteLine(loginDetails.ToString());

            string userName = loginDetails.SelectToken("data.username").ToString();
            string passWord = loginDetails.SelectToken("data.password").ToString(); // is sended in plain text, not yet hashed

            int serial = loginDetails.SelectToken("serial").ToObject<int>();

            // query for user that mathes the username and password
            User user = await (from dt in this.GradingDBContext.LoginDetails
                         where dt.UserName == userName && dt.Password == passWord
                         select dt.User).FirstOrDefaultAsync();

            if (user != null)
            {
                // login succes
                SetupLoginHandler(user);
                SendMessage(JObject.FromObject(JSONWrapperServer.LoginCorrect(user.ToSharedUser(), serial)));

                /*// TEST REMOVE PLEASE REMOVE, forcing creating teacher handler + command
                this.handler = new TeacherHandler(this.GradingDBContext, user, SendMessage);
                this.handler?.Invoke("AddGrade", JObject.FromObject(new { StudentID =  9, ModuleID = 12, Grade = new Grading_Administraton_Shared.Entities.Grade(DateTime.Now, 8.1, "G", 5.5)} ), 8);
            */}
            else
                // login failed
                SendMessage(JObject.FromObject(JSONWrapperServer.LoginFailed(serial)));
        }

        /// <summary>
        /// Creates the correct handler based on the user credentials the client logged in with.
        /// </summary>
        /// <param name="user">The user the handler is based on</param>
        private void SetupLoginHandler(User user)
        {
            UserType userType = (UserType)(int.Parse(user.UserType));

            switch (userType)
            {
                case UserType.STUDENT: 
                    this.handler = new StudentHandler(this.GradingDBContext, user, SendMessage);
                    break;
                case UserType.TEACHER: 
                    this.handler = new TeacherHandler(this.GradingDBContext, user, SendMessage); 
                    break;
                case UserType.ADMIN: 
                    this.handler = new AdminHandler(SendMessage); 
                    break;
            }
                 
        }

        /// <summary>
        /// Sends the given JObject to the TCPHandler
        /// </summary>
        /// <param name="data">The data to be send</param>
        public void SendMessage(JObject data)
        {
            Console.WriteLine(data);
            string dataString = data.ToString();

            this.TCPHandler?.SendMessage(dataString);
        }

        /// <summary>
        /// Stops the connection with the client, stops: TCPHandler, socket and this object
        /// </summary>
        public void Stop()
        {
            this.running = false;
            this.TCPHandler.SetRunning(false);
            this.client.Close();
        }

    }
}
