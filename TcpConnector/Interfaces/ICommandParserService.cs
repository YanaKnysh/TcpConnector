namespace Listener.Interfaces
{
    public interface ICommandParserService
    {
        Task ParseCommand(string ipAddress, int port);
    }
}