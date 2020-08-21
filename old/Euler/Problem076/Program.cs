using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem076
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(PartitionNumber(100));
        }

        static Dictionary<Tuple<int, int>, int> restrictedPartitionNumberCache = new Dictionary<Tuple<int, int>, int>();

        static Dictionary<long, long> partitionNumberCache = new Dictionary<long, long>();

        static long PartitionNumber(long n)
        {
            if (n == 0)
            {
                return 1;
            }

            long sum = 0;
            if (!partitionNumberCache.TryGetValue(n, out sum))
            {
                // Using the pentagon number theorem
                int i = 1;
                bool flip = true;
                int state = 0;
                while (true)
                {
                    long generalizedPentagon = (i * (3 * i - 1) / 2);
                    long index = n - generalizedPentagon;
                    if (flip) { i = -i; flip = false; } else { i = -i; i++; flip = true; }

                    if (index >= 0)
                    {
                        long recursive = PartitionNumber(index);
                        if (state >= 2)
                        {
                            recursive = -recursive;
                        }

                        sum += recursive;
                    }
                    else
                    {
                        break;
                    }
                    state = (state + 1) % 4;
                }
                partitionNumberCache.Add(n, sum);
            }

            return sum;
        }

        static int RestrictedPartitionNumber(int n, int k)
        {
            int result = 0;
            Tuple<int, int> key = Tuple.Create(n, k);
            if (!restrictedPartitionNumberCache.TryGetValue(key, out result))
            {
                if (k >= n)
                {
                    result++;
                }

                int initialChoice = Math.Min(n - 1, k);
                for (int i = initialChoice; i >= 1; i--)
                {
                    // We choose the first number to be i
                    // Then the remaining number is n - i
                    // Now consider the number of ways n - i can be represented as a sum (possibly as a singleton) of descending numbers
                    result += RestrictedPartitionNumber(n - i, Math.Min(k, i));
                }
                restrictedPartitionNumberCache.Add(key, result);
            }

            return result;
        }
    }
}
