using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem034
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindMatch().Aggregate((x, y) => x + y));
        }

        static IEnumerable<int> FindMatch(){
            int[] factorial = new int[10];

            factorial[0] = 1;
            for (int i = 1; i < 10; i++)
            {
                factorial[i] = factorial[i - 1] * i;
            }

            // For any K digit number, the digitFactorialSumMax = 9!k, any k digit number has to be as least as large as 10^{k - 1}
            int k = 1;
            long digitFactorialSumMax = factorial[9];
            long valueMin = 1;
            while (digitFactorialSumMax > valueMin)
            {
                digitFactorialSumMax += factorial[9];
                valueMin = valueMin * 10;
                k++;
            }

            // We know the number is impossible to be of k digit, so we have a simple max
            int max = 1;
            for (int i = 1; i <= k; i++)
            {
                max *= 10;
            }
            for (int i = 10; i < max; i++)
            {
                int digitFactorialSum = i.ToString().Select(d => factorial[d - '0']).Aggregate((x, y) => x + y);
                if (i == digitFactorialSum)
                {
                    yield return i;
                }
            }
        }
    }
}
