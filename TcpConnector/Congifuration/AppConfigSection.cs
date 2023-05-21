namespace Listener.Congifuration
{
    public class AppConfigSection : IConfigSection
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
    }
}
