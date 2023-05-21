using Core;
using Sender;

class MyTcpSender
{
    public static void Main()
    {
        {
            TcpSender.Connect("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First());
            TcpSender.Connect("127.0.0.1", 1000, Command.GetStatus.ToString());
            TcpSender.Connect("127.0.0.1", 1000, "hello3");
        }
    }
}