using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem036
{
    class Program
    {
        static void Main(string[] args)
        {
            long sum = 0;
            for (int i = 1; i <= 1000000; i++)
            {
                List<int> decimalReversed = ToBinaryReversed(i, 10).ToList();
                List<int> decimalReversedTwice = new List<int>(decimalReversed);
                decimalReversed.Reverse();
                if (decimalReversed.SequenceEqual(decimalReversedTwice))
                {
                    List<int> binaryReversed = ToBinaryReversed(i, 2).ToList();
                    List<int> binaryReversedTwice = new List<int>(binaryReversed);
                    binaryReversed.Reverse();
                    if (binaryReversed.SequenceEqual(binaryReversedTwice))
                    {
                        sum += i;
                    }
                }
            }
            Console.WriteLine(sum);
        }

        static IEnumerable<int> ToBinaryReversed(int x, int b)
        {
            while (true)
            {
                if (x == 0)
                {
                    yield break;
                }
                yield return x % b;
                x = x / b;
            }
        }
    }
}
