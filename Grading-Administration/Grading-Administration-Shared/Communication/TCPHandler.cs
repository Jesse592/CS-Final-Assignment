using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grading_Administration_Server.Communication
{
    class TCPHandler
    {
        public event EventHandler<string> OnDataReceived;
        private NetworkStream stream;

        private bool running;

        public TCPHandler(NetworkStream stream)
        {
            this.stream = stream;

            this.running = false;
        }

        public static void SendMessage(string message, NetworkStream stream)
        {
            int length = Encoding.ASCII.GetBytes(message).Length;
            byte[] MessageArray = ConvertMessage(message);
            stream.Write(MessageArray, 0, length + 4);
            stream.Flush();
        }

        public string ReadMessage()
        {
            // 4 bytes long == 32 bits, always positive unsigned
            byte[] lengthArray = new byte[4];

            stream.Read(lengthArray, 0, 4);
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

        private void HandleIncoming()
        {
            // Starting the reading loop in new thread
            new Thread(
                () =>
                {

                    while (running)
                    {
                        // Call the event with the message received
                        if (stream != null)
                        {
                            string message = ReadMessage();
                            OnDataReceived.Invoke(this, message);
                        }

                    }

                    // Shutting down
                    stream?.Close();

                }).Start();
        }

        public void SetRunning(bool run)
        {
            this.running = run;

            if (run)
                HandleIncoming();
            
        }

        private static byte[] ConvertMessage(string message)
        {
            byte[] payload = Encoding.ASCII.GetBytes(message);
            byte[] length = new byte[4];
            length = BitConverter.GetBytes(payload.Length);

            byte[] final = new byte[length.Length + payload.Length];
            Buffer.BlockCopy(length, 0, final, 0, payload.Length);
            Buffer.BlockCopy(payload, 0, final, length.Length, payload.Length);

            return final;
        }


    }
}
