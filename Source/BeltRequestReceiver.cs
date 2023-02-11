using KS_Fit_Pro.Models;
using System.Text;

namespace KS_Fit_Pro.Source
{
    
    public class BeltRequestReceiver
    {
        byte[] message;
        internal BeltState GetBeltStateFromMessage(byte[] message)
        {
            this.message = message;
            return new BeltState()
            {
                BeltMode = (BeltMode)Convert.ToInt32(message[2]),
                BeltSpeed = Convert.ToInt32(message[3]),
                IsManualMode = Convert.ToInt32(message[4]) == 1 ? true : false,
                ActivityTime = new TimeSpan(0,0, DecodeThreeByteValues(5)),
                ActivityDistance = DecodeThreeByteValues(8) * 10,
                Steps = DecodeThreeByteValues(11)
            };
        }
       
        int DecodeThreeByteValues(int startingByte) 
        {
            var firstByte = Convert.ToInt32(message[startingByte]) ^ 64;
            var sedondByte = Convert.ToInt32(message[startingByte + 1]) ^ 8;
            var thirdByte = Convert.ToInt32(message[startingByte +2]) ^ 1;
            return firstByte + sedondByte + thirdByte;
        }
    }
}
