using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GradingAdministration_server
{
    class TCPHandler
    {
        public static void SendMessage(string message, NetworkStream stream)
        {
            int length = Encoding.ASCII.GetBytes(message).Length;
            byte[] MessageArray = ConvertMessage(message);
            stream.Write(MessageArray, 0, length + 4);
            stream.Flush();
        }

        public static byte[] ConvertMessage(string message)
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
