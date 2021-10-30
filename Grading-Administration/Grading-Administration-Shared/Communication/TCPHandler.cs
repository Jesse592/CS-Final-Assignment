using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grading_Administration_Shared.Communication
{
    /// <summary>
    /// Handles the TCP communication between two hosts
    /// </summary>
    class TCPHandler
    {
        public event EventHandler<string> OnDataReceived;
        public event EventHandler<string> OnConectionError;

        private NetworkStream stream;

        private bool running;

        /// <summary>
        /// Constructor for TCPHandler
        /// </summary>
        /// <param name="stream">The stream to use</param>
        public TCPHandler(NetworkStream stream)
        {
            this.stream = stream;

            this.running = false;
        }

        /// <summary>
        /// Method to send a string to the host
        /// </summary>
        /// <param name="message">The message to sent</param>
        public void SendMessage(string message)
        {
            // Gets the byte array according to protocol
            byte[] messageArray = ConvertMessage(message);
            stream.Write(messageArray, 0, messageArray.Length);
            stream.Flush();
        }

        /// <summary>
        /// Reads and converts a message from the stream
        /// </summary>
        /// <returns>The string received from the client</returns>
        public string ReadMessage()
        {
            // 4 bytes long == 32 bits, always positive unsigned
            byte[] lengthArray = new byte[4];

            // First checking if we can read from the stream
            if (!stream.CanRead) return null;
            
            stream?.Read(lengthArray, 0, 4);
            int length = BitConverter.ToInt32(lengthArray, 0);

            Console.WriteLine(length);

            byte[] buffer = new byte[length];
            int totalRead = 0;

            //read bytes until stream indicates there are no more
            while (totalRead < length)
            {
                int read = stream.Read(buffer, totalRead, buffer.Length - totalRead);
                totalRead += read;
            }

            return Encoding.ASCII.GetString(buffer, 0, totalRead);
        }

        /// <summary>
        /// Reads messages from the stream in a loop to keep reading
        /// </summary>
        private void HandleIncoming()
        {
            // Starting the reading loop in new thread
            new Thread(
                () =>
                {

                    while (running)
                    {try
                        {
                            string message = ReadMessage();
                            OnDataReceived?.Invoke(this, message);
                        }
                        catch (IOException e) 
                        {
                            // Client has dissconnected, calling the error command
                            this.running = false;
                            this.OnConectionError?.Invoke(this, e.Message);
                        }
                    }

                    // Shutting down
                    stream?.Close();

                }).Start();
        }

        /// <summary>
        /// Starts or stops the server
        /// </summary>
        /// <param name="run">to run or not to run</param>
        public void SetRunning(bool run)
        {
            this.running = run;

            if (run)
                HandleIncoming();
            
        }

        /// <summary>
        /// Convertst a given string to a byte array with 4 leading bytes that give length
        /// </summary>
        /// <param name="message">The string to send</param>
        /// <returns>The byte array ready to send</returns>
        private static byte[] ConvertMessage(string message)
        {
            // Getting the payload and length
            byte[] payload = Encoding.ASCII.GetBytes(message);
            byte[] length = new byte[4];
            length = BitConverter.GetBytes(payload.Length);

            // Merging the payload and length
            byte[] final = new byte[length.Length + payload.Length];
            Buffer.BlockCopy(length, 0, final, 0, length.Length);
            Buffer.BlockCopy(payload, 0, final, length.Length, payload.Length);

            return final;
        }


    }
}
