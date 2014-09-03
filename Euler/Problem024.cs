namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem024()
        {
            int[] factorials = new int[11];
            factorials[0] = 1;
            for (int i = 1; i <= 10; i++)
            {
                factorials[i] = factorials[i - 1] * i;
            }

            // There are nine factorials number starts with 0
            // There are nine factorials number starts with 1, 
            // ...

            int current = 1000000 - 1;
            List<int> availableDigits = Enumerable.Range(0, 10).ToList();
            while (availableDigits.Count > 0)
            {
                int partitionSize = factorials[availableDigits.Count - 1];
                int quotient = current / partitionSize;
                int remainder = current % partitionSize;
                int partitionNumber = quotient + 1;
                current = remainder;
                Console.Write(availableDigits[partitionNumber - 1]);
                availableDigits.RemoveAt(partitionNumber - 1);
            }
            Console.WriteLine();
        }
    }
}