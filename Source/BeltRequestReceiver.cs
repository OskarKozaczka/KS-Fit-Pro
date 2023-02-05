namespace KS_Fit_Pro.Source
{
    public class BeltRequestReceiver
    {
        internal BeltState GetBeltStateFromMessage(byte[] message)
        {
            return new BeltState()
            {
                BeltMode = (BeltMode)Convert.ToInt32(message[2]),
                BeltSpeed = Convert.ToInt32(message[3]),
                ManualMode = Convert.ToInt32(message[4]) == 1 ? true : false,
                ActivityTime = GetTime(message),
                ActivityDistance = GetDistance(message),
                Steps = GetSteps(message)
            };
        }

        TimeSpan GetTime(byte[] message)
        {
            var firstByte = Convert.ToInt32(message[5]) ^ 64;
            var sedondByte = Convert.ToInt32(message[6]) ^ 8;
            var thirdByte = Convert.ToInt32(message[7]) ^ 1;
            var seconds = firstByte + sedondByte + thirdByte;
            return new TimeSpan(0, 0, seconds);
        }

        int GetDistance(byte[] message)
        {
            var firstByte = Convert.ToInt32(message[8]) ^ 64;
            var sedondByte = Convert.ToInt32(message[9]) ^ 8;
            var thirdByte = Convert.ToInt32(message[10]) ^ 1;
            var meters = firstByte + sedondByte + thirdByte;
            return meters * 10;
        }

        int GetSteps(byte[] message)
        {
            var firstByte = Convert.ToInt32(message[11]) ^ 64;
            var sedondByte = Convert.ToInt32(message[12]) ^ 8;
            var thirdByte = Convert.ToInt32(message[13]) ^ 1;
            var steps = firstByte + sedondByte + thirdByte;
            return steps;
        }
    }
}
