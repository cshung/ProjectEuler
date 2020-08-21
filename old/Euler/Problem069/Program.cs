namespace Problem069
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            double max = -1;
            int maxIndex = -1;
            for (int i = 2; i <= 1000000; i++)
            {
                long euler = EulerUtil.EulerPhi(i);
                double fraction = (double)i / euler;
                if (fraction > max)
                {
                    max = fraction ;
                    maxIndex = i;
                }
            }
            Console.WriteLine(maxIndex);
        }        
    }
}
