using Listener;
using Listener.Interfaces;
using Listener.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//// Main settings
//using IHost host = CreateHostBuilder(args).Build();
//await host.RunAsync();
//static IHostBuilder CreateHostBuilder(string[] args)
//{
//    return Host.CreateDefaultBuilder(args)
//        .ConfigureServices((_, services) =>
//        {
//            services.AddTransient<ICommandParserService, CommandParserService>()
//                .AddTransient<ICommandService, CommandService>()
//                .AddSingleton<IConfiguration>())};
//        }
//}

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
