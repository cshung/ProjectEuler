namespace Problem050
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;

    class Program
    {
        // The program is considerably slow - what can we do about it?
        static void Main(string[] args)
        {
            Console.WriteLine(Enumerable.Range(1, 100).Select(t => '.').ToConcatString());
            int max = -1;
            int maxPrime = -1;
            var primes = EulerUtil.Primes(1000000).ToArray();
            var sums = new List<long>();
            sums.Add(0);
            // If primes[i] + primes[i + 1] > 1000000, then it is impossible for any consecutive sum to include these two
            int lastPrime = 0;
            foreach (int prime in primes)
            {
                sums.Add(sums[sums.Count - 1] + prime);
                if (prime + lastPrime > 1000000)
                {
                    break;
                }
                lastPrime = prime;
            }
            
            Dictionary<long, int> sumSet = new Dictionary<long,int>();
            for (int d = 0; d < sums.Count; d++)
            {
                sumSet.Add(sums[d], d);
            }

            long size = (long)primes.Length * sums.Count;

            long count = 0;
            Parallel.ForEach(primes, delegate(int prime)
            {
                for (int c = 0; c < sums.Count; c++)
                {
                    Interlocked.Increment(ref count);
                    if (count % (size / 100) == 0)
                    {
                        Console.Write('.');
                    }
                    int d;
                    if (sumSet.TryGetValue(sums[c] + prime, out d))
                    {
                        //Console.WriteLine("{0} = {1} - {2}", prime, sums[d], sums[c]);
                        if (d - c > max)
                        {
                            max = d - c;
                            maxPrime = prime;
                        }
                    }
                }
            });
            Console.WriteLine(maxPrime);
        }
    }
}
