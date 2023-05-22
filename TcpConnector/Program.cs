using Listener;

internal class Program
{
        public static async Task Main()
        {
            Console.WriteLine("Press spacebar to stop");

            TcpConnector tcpConnector = new TcpConnector("127.0.0.1", 1000);
            await tcpConnector.Listen();
    }
}


//ConsoleKeyInfo c;
//do
//{
//    c = Console.ReadKey();
//} while (c.Key != ConsoleKey.Spacebar);

//tcpConnector.isListening = false;
//Environment.Exit(0);

//var userInput = Console.ReadKey();

//while (userInput.Key != ConsoleKey.Spacebar) 
//{
//    userInput = Console.ReadKey();
//}
