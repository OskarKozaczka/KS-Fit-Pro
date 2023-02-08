using KS_Fit_Pro.Models;

namespace KS_Fit_Pro.Source
{
    internal static class BeltRequestHelper
    {
        static void AddChecksum(ref byte[] message)
        {
            message[4] = (byte)((message[1] + message[2] + message[3]) % 256);
        }

        public static byte[] GetEncodedMessage(EnBeltAction beltAction, int actionValue)
        {
            byte[] message = new byte[6] { 247, 162, (byte)beltAction, (byte)actionValue, 0, 253 };
            AddChecksum(ref message);
            return message;
        }
    }
}