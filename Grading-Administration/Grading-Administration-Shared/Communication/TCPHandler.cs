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
    class TCPHandler
    {
        public event EventHandler<string> OnDataReceived;
        public event EventHandler<string> OnConectionError;

        private NetworkStream stream;

        private bool running;

        public TCPHandler(NetworkStream stream)
        {
            this.stream = stream;

            this.running = false;
        }

        public void SendMessage(string message)
        {
            Console.WriteLine(message);

            byte[] messageArray = ConvertMessage(message);
            stream.Write(messageArray, 0, messageArray.Length);
            stream.Flush();
        }

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
            Buffer.BlockCopy(length, 0, final, 0, length.Length);
            Buffer.BlockCopy(payload, 0, final, length.Length, payload.Length);

            return final;
        }


    }
}
