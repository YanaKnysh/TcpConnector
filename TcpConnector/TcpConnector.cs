using Listener.Services;
using System.Net;
using System.Net.Sockets;

namespace Listener
{
    public class TcpConnector
    {
        public bool isListening {  get; set; }
        private TcpListener server;
        private string ipAddress;
        private int port;

        public TcpConnector(string ipAddress, int port)
        {
            isListening = true;
            this.ipAddress = ipAddress;
            this.port = port;
            IPAddress localAddr = IPAddress.Parse(ipAddress);
            server = new TcpListener(localAddr, port);
        }

        public async Task Listen() 
        {
            try
            {
                server.Start();

                while (isListening)
                {
                    Console.Write("Waiting for a connection... ");

                    CommandParserService commandParserService = new CommandParserService(server);
                    await commandParserService.ParseCommand(ipAddress, port);
                    
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

        public void Stop()
        {
            server.Stop();
            isListening = false;
        }
    }
}
