using Core;
using Sender;

class MyTcpSender
{
    public static async Task Main()
    {
        {
            await SendRequests();

            Console.ReadLine();
        }
    }

    public static async Task SendRequests()
    {
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First());
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last());
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First());
        await TcpSender.Connect("127.0.0.1", 1000, Command.GetStatus.ToString());
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last());
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First());
        await TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last());
        await TcpSender.Connect("127.0.0.1", 1000, Command.GetStatus.ToString());
        await TcpSender.Connect("127.0.0.1", 1000, "hello3");
    }
}