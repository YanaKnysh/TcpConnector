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
        var list = new List<Tuple<string, int, string>>()
        {
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.GetStatus.ToString()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.First()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.Print.ToString() + Label.labels.Last()),
            new Tuple<string, int, string>("127.0.0.1", 1000, Command.GetStatus.ToString()),
            new Tuple<string, int, string>("127.0.0.1", 1000, "hello3"),

        };

        ParallelOptions parallelOptions = new()
        {
            MaxDegreeOfParallelism = 3
        };
        await Parallel.ForEachAsync(list, async (item, CancellationToken) =>
        {
            await TcpSender.Connect(item.Item1, item.Item2, item.Item3);
        });
    }

}