using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem027
{
    class Program
    {
        static void Main(string[] args)
        {
            Tuple<int, int> best = Tuple.Create(-1, -1);
            //Console.WriteLine(Enumerable.Range(1, 100).Select(i => ".").Aggregate((x, y) => x + y));
            for (int a = -1000; a <= 1000; a++)
            {
                //if (a % 20 == 0) { Console.Write("."); }
                for (int b = -1000; b <= 1000; b++)
                {

                    long current = 0;
                    int length = 0;
                    while (true)
                    {
                        long value = current * current + a * current + b;
                        if (!IsPrime(value))
                        {
                            break;
                        } 
                        
                        //Console.WriteLine(a + " " + b + " => " + value + "is prime"); 
                               
                        length++;
                        current++;
                    }
                    if (length > best.Item1)
                    {
                        best = Tuple.Create(length, a * b);
                    }
                };
            };
            //Console.WriteLine(best.Item1);
            Console.WriteLine(best.Item2);
        }

        static Dictionary<long, bool> PrimeCache = new Dictionary<long, bool>();

        static bool IsPrime(long x)
        {
            if (x < 0)
            {
                x = -x;
            }
            bool result;
            if (PrimeCache.TryGetValue(x, out result))
            {
                return result;
            }
            for (long y = 2; y < x; y++)
            {
                if (x % y == 0)
                {
                    PrimeCache.Add(x, false);
                    return false;
                }
            }
            PrimeCache.Add(x, true);
            return true;
        }
    }
}
