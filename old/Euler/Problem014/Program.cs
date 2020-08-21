using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem014
{
    class Program
    {
        static Dictionary<long, long> lengths = new Dictionary<long, long>();

        static long Length(long x)
        {
            if (x == 1)
            {
                return 1;
            }
            else
            {
                long result;
                if (lengths.TryGetValue(x, out result))
                {
                    return result;
                }
                else
                {
                    long next = x % 2 == 0 ? x / 2 : (3 * x + 1);
                    result = 1 + Length(next);
                    lengths.Add(x, result);
                    return result;
                }
            }
        }


        static void Main(string[] args)
        {
            for (long i = 1; i <= 1000000; i++)
            {
                Length(i);
            }
            var lengthSorted = lengths.ToList();
            lengthSorted.Sort((x, y) => (x.Value < y.Value) ? 1 : ((x.Value == y.Value) ? 0 : -1));
            Console.WriteLine(lengthSorted[0]);
        }
    }


}
