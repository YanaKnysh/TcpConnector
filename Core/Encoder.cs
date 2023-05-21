namespace Core
{
    public class Encoder
    {
        public static byte[] EncodeMessage(string message)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(message);
            byte[] wrappedData = new byte[data.Length + 2];
            wrappedData[0] = Wrapper.Start;
            Array.Copy(data, 0, wrappedData, 1, data.Length);
            wrappedData[wrappedData.Length - 1] = Wrapper.End;

            return wrappedData;
        }

        public static string DecodeMessage(byte[] message, int byteCount)
        {
            byte[] responseData = new byte[byteCount - 2];
            Array.Copy(message, 1, responseData, 0, responseData.Length);
            var response = System.Text.Encoding.ASCII.GetString(responseData, 0, responseData.Length);
            
            return response;
        }
    }
}
