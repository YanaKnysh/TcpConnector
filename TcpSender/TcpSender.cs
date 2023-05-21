using Core;
using System.Net.Sockets;

namespace Sender
{
    public class TcpSender
    {
        public static void Connect(string server, int port, string message)
        {
            try
            {
                using TcpClient client = new TcpClient(server, port);

                byte[] data = Encoder.EncodeMessage(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);

                Console.WriteLine(Message.FormMessage(message, true));

                data = new byte[256];
                string responseData = string.Empty;

                int bytes = stream.Read(data, 0, data.Length);
                responseData = Encoder.DecodeMessage(data, bytes);
                Console.WriteLine(Message.FormMessage(responseData, false));
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
