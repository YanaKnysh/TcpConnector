using Listener.Services;
using System.Net;
using System.Net.Sockets;

namespace Listener
{
    public class TcpConnector
    {
        public bool isListening {  get; set; }

        public TcpConnector()
        {
            isListening = true;
        }

        public void Listen(string ipAdress, int port) 
        {
            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse(ipAdress);

                server = new TcpListener(localAddr, port);
                server.Start();

                while (isListening)
                {
                    Console.Write("Waiting for a connection... ");

                    CommandParserService commandParserService = new CommandParserService(server);
                    commandParserService.ParseCommand();
                    
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server?.Stop();
            }
        }
    }
}
