using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Problem078
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger i = BigInteger.One;
            while (true)
            {
                string result = PartitionNumber(i).ToString();
                if (result.Length > 6)
                {
                    if (string.Equals("000000", result.Substring(result.Length - 6)))
                    {
                        Console.WriteLine(i);
                        break;
                    }
                }

                i = i + BigInteger.One;
            }
        }

        static Dictionary<BigInteger, BigInteger> partitionNumberCache = new Dictionary<BigInteger, BigInteger>();

        static BigInteger PartitionNumber(BigInteger n)
        {
            if (n == 0)
            {
                return 1;
            }

            BigInteger sum = 0;
            if (!partitionNumberCache.TryGetValue(n, out sum))
            {
                // Using the pentagon number theorem
                int i = 1;
                bool flip = true;
                int state = 0;
                while (true)
                {
                    BigInteger generalizedPentagon = (i * (3 * i - 1) / 2);
                    BigInteger index = n - generalizedPentagon;
                    if (flip) { i = -i; flip = false; } else { i = -i; i++; flip = true; }

                    if (index >= 0)
                    {
                        BigInteger recursive = PartitionNumber(index);
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

    }
}
