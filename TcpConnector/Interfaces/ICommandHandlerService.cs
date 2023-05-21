namespace Listener.Interfaces
{
    public interface ICommandHandlerService
    {
        Task HandleRequest(byte[] command, int byteCount, string ipAddress, int port);
    }
}