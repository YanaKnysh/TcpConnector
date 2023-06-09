﻿using Core;
using Listener.Interfaces;
using System.Net.Sockets;

namespace Listener.Services
{
    public class CommandParserService : ICommandParserService
    {
        private TcpListener listener;

        public CommandParserService(TcpListener listener)
        {
            this.listener = listener;
        }

        public async Task ParseCommand(string ipAddress, int port)
        {
            byte[] bytes = new byte[256];

            using TcpClient client = await listener.AcceptTcpClientAsync();

            Console.WriteLine("Connected!");

            NetworkStream stream = client.GetStream();

            int i;

            // Loop to receive all the data sent by the client.
            while ((i = await stream.ReadAsync(bytes, 0, bytes.Length)) != 0)
            {
                int startPosition = -1;
                int endPosition = -1;
                for (int j = 0; j < i; j++)
                {
                    if (bytes[j] == Wrapper.Start)
                    {
                        startPosition = j;
                    }
                    else if (bytes[j] == Wrapper.End && startPosition != -1)
                    {
                        endPosition = j;
                    }

                    if (startPosition != -1 && endPosition != -1 && endPosition > startPosition)
                    {
                        var commandArray = new byte[bytes.Length - 2];
                        Array.Copy(bytes, startPosition, commandArray, 0, j);
                        CommandHandlerService commandService = new CommandHandlerService(stream);
                        await commandService.HandleRequest(commandArray, endPosition + 1, ipAddress, port);
                        startPosition = -1;
                        endPosition = -1;
                    }
                }
            }
        }
    }
}
