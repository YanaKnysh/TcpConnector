namespace Core
{
    public static class MessageFormatter
    {
        public static string FormMessage(string message, string ipAdress, int port, bool isClient)
        {
            var currentTime = DateTime.Now.ToString("hh.mm.ss.ffffff");
            string direction = isClient ? " => " : " <= ";

            return currentTime + ipAdress + ":" + port.ToString() + direction + message;
        }
    }
}
