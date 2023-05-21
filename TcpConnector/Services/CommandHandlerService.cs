using Core;
using Listener.Interfaces;
using System.Net.Sockets;

namespace Listener.Services
{
    public class CommandHandlerService : ICommandService
    {
        private NetworkStream _stream;
        private bool isPrinting;

        public CommandHandlerService(NetworkStream networkStream)
        {
            _stream = networkStream;
            isPrinting = false;
        }

        public void HandleRequest(byte[] command, int byteCount)
        { 
            string data = null;

            // Translate data bytes to a ASCII string.
            data = Encoder.DecodeMessage(command, byteCount);
            Console.WriteLine(Message.FormMessage(data, true));

            // Process the data sent by the client.
            if (data.StartsWith(Command.Print))
            {
                var labelName = data.Substring(5);
                if (labelName != null && Label.labels.Contains(labelName))
                {
                    isPrinting = true;
                    Thread.Sleep(1000);
                    isPrinting = false;
                    byte[] response = Encoder.EncodeMessage(Response.PrintDone);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine(Message.FormMessage(Response.PrintDone, false));
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.WrongLabel);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine(Message.FormMessage(Response.WrongLabel, false));
                }
            }
            else if (data == Command.GetStatus)
            {
                if (isPrinting)
                {
                    byte[] response = Encoder.EncodeMessage(Response.Printing);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine(Message.FormMessage(Response.Printing, false));
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.Idle);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine(Message.FormMessage(Response.Idle, false));
                }
            }
            else
            {
                byte[] response = Encoder.EncodeMessage(Response.UnknowCommand);
                _stream.Write(response, 0, response.Length);
                Console.WriteLine(Message.FormMessage(Response.UnknowCommand, false));
            }
        }
    }
}
