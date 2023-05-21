using Core;
using System.Net.Sockets;

namespace Sender
{
    public class TcpSender
    {
        public static async Task Connect(string server, int port, string message)
        {
            try
            {
                using TcpClient client = new TcpClient(server, port);

                //Encoder encoder = new Encoder();
                byte[] data = Encoder.EncodeMessage(message);
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(data, 0, data.Length);

                //MessageFormatter messageFormatter = new MessageFormatter();
                Console.WriteLine(MessageFormatter.FormMessage(message, server, port, true));

                data = new byte[256];
                string responseData = string.Empty;

                int bytes = await stream.ReadAsync(data, 0, data.Length);
                responseData = Encoder.DecodeMessage(data, bytes);
                Console.WriteLine(MessageFormatter.FormMessage(responseData, server, port, false));
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
