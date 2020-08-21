using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem024
{
    class Program
    {
        static void Main(string[] args)
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
            //Console.WriteLine("Finding the " + current + " permutation");
            List<int> availableDigits = Enumerable.Range(0, 10).ToList();
            while (availableDigits.Count > 0)
            {
                int partitionSize = factorials[availableDigits.Count - 1];
                int quotient = current / partitionSize;
                int remainder = current % partitionSize;
                //Console.WriteLine("The available digits are: " + availableDigits.Aggregate(string.Empty, (s, x) => s + " " + x));
                //Console.WriteLine("{0} = {1} x {2} + {3}", current, quotient, partitionSize, remainder);
                int partitionNumber = quotient + 1;
                current = remainder;
                //Console.Write("Therefore - picking: ");
                Console.Write(availableDigits[partitionNumber - 1]);
                //Console.WriteLine();
                availableDigits.RemoveAt(partitionNumber - 1);
            }
            Console.WriteLine();

        }
    }
}
