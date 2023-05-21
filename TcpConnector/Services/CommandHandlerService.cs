using Core;
using Listener.Interfaces;
using System.Net.Sockets;

namespace Listener.Services
{
    public class CommandHandlerService : ICommandHandlerService
    {
        private NetworkStream _stream;
        private bool isPrinting;

        public CommandHandlerService(NetworkStream networkStream)
        {
            _stream = networkStream;
            isPrinting = false;
        }

        public async Task HandleRequest(byte[] command, int byteCount, string ipAddress, int port)
        { 
            string data;

            data = Encoder.DecodeMessage(command, byteCount);

            // Process the data sent by the client.
            if (data.StartsWith(Command.Print))
            {
                var labelName = data.Substring(5);
                if (labelName != null && Label.labels.Contains(labelName))
                {
                    isPrinting = true;
                    await Task.Delay(1000);
                    isPrinting = false;
                    byte[] response = Encoder.EncodeMessage(Response.PrintDone);
                    await _stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine(MessageFormatter.FormMessage(Response.PrintDone, ipAddress, port, false));
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.WrongLabel);
                    await _stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine(MessageFormatter.FormMessage(Response.WrongLabel, ipAddress, port, false));
                }
            }
            else if (data == Command.GetStatus)
            {
                if (isPrinting)
                {
                    byte[] response = Encoder.EncodeMessage(Response.Printing);
                    await _stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine(MessageFormatter.FormMessage(Response.Printing, ipAddress, port, false));
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.Idle);
                    await _stream.WriteAsync(response, 0, response.Length);
                    Console.WriteLine(MessageFormatter.FormMessage(Response.Idle, ipAddress, port, false));
                }
            }
            else
            {
                byte[] response = Encoder.EncodeMessage(Response.UnknowCommand);
                await _stream.WriteAsync(response, 0, response.Length);
                Console.WriteLine(MessageFormatter.FormMessage(Response.UnknowCommand, ipAddress, port, false));
            }
        }
    }
}
