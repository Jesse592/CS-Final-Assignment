﻿using Grading_Administration_Server.Communication;
using Grading_Administration_Server.EntityFramework;
using Grading_Administration_Server.EntityFramework.models;
using Grading_Administration_Server.Handlers;
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

            // Creating a new DB Context per client, te ensure the context is not open 24/7
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
            // command value always gives the action 
            JToken command;

            bool correctCommand = data.TryGetValue("command", StringComparison.InvariantCulture, out command);

            // returning when JSON has wron format
            if (!correctCommand) return;

            // This class only handles login
            if (command.ToString() == "login")
                HandleLogin(data);
            
            // Sending this command to the handler, if client is not logged in handler will be null
            this.handler?.Invoke(command.ToString(), data);
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
                SendMessage(JObject.FromObject(JSONWrapperServer.LoginCorrect(user.ToSharedUser(), serial)));
                SetupLoginHandler(user);
            }
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
            switch (user.UserType)
            {
                case "Student": this.handler = new StudentHandler(); break;
                case "Teacher": this.handler = new TeacherHandler(); break;
                case "Admin": this.handler = new AdminHandler(); break;
            }
                 
        }

        /// <summary>
        /// Sends the given JObject to the TCPHandler
        /// </summary>
        /// <param name="data">The data to be send</param>
        public void SendMessage(JObject data)
        {
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
