namespace Euler
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    internal static partial class Program
    {
        public static string ReadResourceAsString(string resourceName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName).CopyTo(ms);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        // Pollard's rho algorithm for factoring (note that it does not factor 2^n)
        public static List<long> PollardFactor(long n)
        {
            List<long> factors = new List<long>();
            while (true)
            {
                // Factor it
                long x = 2;
                long y = 2;
                long d = 1;
                while (d == 1)
                {
                    x = PollardSequence(x);
                    y = PollardSequence(PollardSequence(y));
                    long diff = x - y;
                    if (diff < 0)
                    {
                        diff = -diff;
                    }
                    d = CommonFactor(diff, n);
                    if (d == n)
                    {
                        factors.Add(n);
                        break;
                    }
                }
                if (d == n) { break; }
                factors.Add(d);
                n = n / d;
            }

            return factors;
        }

        private static long PollardSequence(long x)
        {
            return (x % int.MaxValue) * (x % int.MaxValue) + 1;
        }

        private static long CommonFactor(long m, long n)
        {
            while (true)
            {
                long r = m % n;
                if (r == 0)
                {
                    return n;
                }

                m = n;
                n = r;
            }
        }

        private static long CommonMultiple(long m, long n)
        {
            return m * n / CommonFactor(m, n);
        }

        private static int Reverse(int i)
        {
            int j = 0;
            while (i > 0)
            {
                j = j * 10 + i % 10;
                i = i / 10;
            }
            return j;
        }

        private static IEnumerable<Tuple<int, int, int>> GetPrimitivePythTriples(int sumMax)
        {
            var root = new Tuple<int, int, int>(3, 4, 5);
            Queue<Tuple<int, int, int>> bfsQueue = new Queue<Tuple<int, int, int>>();
            bfsQueue.Enqueue(root);
            while (bfsQueue.Count > 0)
            {
                var visiting = bfsQueue.Dequeue();
                int a = visiting.Item1;
                int b = visiting.Item2;
                int c = visiting.Item3;

                if (a + b + c <= sumMax)
                {
                    yield return visiting;
                    bfsQueue.Enqueue(Tuple.Create(
                        1 * a - 2 * b + 2 * c,
                        2 * a - 1 * b + 2 * c,
                        2 * a - 2 * b + 3 * c));
                    bfsQueue.Enqueue(Tuple.Create(
                        1 * a + 2 * b + 2 * c,
                        2 * a + 1 * b + 2 * c,
                        2 * a + 2 * b + 3 * c));
                    bfsQueue.Enqueue(Tuple.Create(
                        -1 * a + 2 * b + 2 * c,
                        -2 * a + 1 * b + 2 * c,
                        -2 * a + 2 * b + 3 * c));
                }
            }
        }

        private static IEnumerable<int> Primes(int max)
        {
            if (max >= 2)
            {
                yield return 2;
            }

            // sieve[i] represents 2i + 3; [So sieve[0] = 3, sieve[1] = 5] ...
            BitArray sieve = new BitArray(max / 2);
            int i = 0;
            while (true)
            {
                int num = (2 * i) + 3;
                int current_i = i;
                var indexes = Enumerable.Range(1, (sieve.Length - 1 - current_i) / num).Select(t => (t * num) + current_i).ToList();
                Parallel.ForEach<int>(indexes, delegate(int index) { sieve[index] = true; });
                sieve[i] = false;
                i++;
                while (i < sieve.Length && sieve[i])
                {
                    i++;
                }

                if (i >= sieve.Length)
                {
                    break;
                }
            }

            for (int j = 0; j < sieve.Length; j++)
            {
                if (!sieve[j])
                {
                    int num = (2 * j) + 3;
                    if (num <= max)
                    {
                        yield return num;
                    }
                }
            }
        }

        // Brute-force factoring 
        private static IEnumerable<Tuple<int, int>> BruteForceFactor(long n)
        {
            if (n == 1)
            {
                yield return Tuple.Create(1, 1);
            }
            else
            {
                int i = 2;
                while (n > 1)
                {
                    if (n % i == 0)
                    {
                        int index = 0;
                        while (n % i == 0)
                        {
                            index++;
                            n /= i;
                        }
                        yield return Tuple.Create(i, index);
                    }
                    i++;
                }
            }
        }
        
        private static int SumOfProperDivisor(int i)
        {
            return SumOfDivisor(i) - i;
        }

        private static int SumOfDivisor(int i)
        {
            var groupedFactors = BruteForceFactor(i).ToList();
            return groupedFactors.Select(kvp => (Power(kvp.Item1, kvp.Item2 + 1) - 1) / (kvp.Item1 - 1)).Aggregate((x, y) => x * y);
        }

        private static int Power(int x, int y)
        {
            return Power<int>(x, y, 1, (p, q) => p * q);
        }

        private static T Power<T>(T x, int y, T one, Func<T, T, T> multiply)
        {
            if (y == 0)
            {
                return one;
            }
            else if (y == 1)
            {
                return x;
            }
            else
            {
                T r = Power(x, y / 2, one, multiply);
                return y % 2 == 0 ? multiply(r, r) : multiply(multiply(r, r), x);
            }
        }
    }
}
