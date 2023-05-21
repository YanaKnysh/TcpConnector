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
            Console.WriteLine("Received: {0}", data);

            // Process the data sent by the client.
            //data = data.ToUpper();

            //byte[] msg = Encoder.EncodeMessage(data);



            // Send back a response.
            //_stream.Write(msg, 0, msg.Length);
            //Console.WriteLine("Sent: {0}", data);

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
                    Console.WriteLine("Sent: {0}", Response.PrintDone);
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.WrongLabel);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine("Sent: {0}", Response.WrongLabel);
                }
            }
            else if (data == Command.GetStatus)
            {
                if (isPrinting)
                {
                    byte[] response = Encoder.EncodeMessage(Response.Printing);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine("Sent: {0}", Response.Printing);
                }
                else
                {
                    byte[] response = Encoder.EncodeMessage(Response.Idle);
                    _stream.Write(response, 0, response.Length);
                    Console.WriteLine("Sent: {0}", Response.Idle);
                }
            }
            else
            {
                byte[] response = Encoder.EncodeMessage(Response.UnknowCommand);
                _stream.Write(response, 0, response.Length);
                Console.WriteLine("Sent: {0}", Response.UnknowCommand);
            }
        }
    }
}
