using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Numerics;

namespace Problem233
{
    class Program
    {
        // These can be useful
        // http://mathoverflow.net/questions/63032/is-there-another-simple-formula-for-the-sum-of-squares-function
        // 
        static void Main(string[] args)
        {
            // Now we know how to compute F - the next step is to find out the values that can lead to F = 420
            // First. 420 = 4 x 3 x 5 x 7
            // Therefore, the number must have either 
            // Exactly 1 prime factors of type (4k + 1), with indexes be (52)
            // Exactly 2 prime factors of type (4k + 1), with indexes be (7, 3), (10, 2) or (17, 1)
            // Exactly 3 prime factors of type (4k + 1), with indexes be (1, 2, 3)
            // If a number satisfy a prime factor requirementS above, f(N) = 420, regardless of the presence of any other prime factors of form other than 4p + 1

            // The rest is as follow
            // First, find all the 'seeds'. A seed is a number that satisfy the prime factor requirement. Naturally, there are infinitely many seeds, we only need seed less than 1e11.
            // Based on a seed - we see if it is multiplied with any number that does not have a prime factor of form 4k + 1, it would give the same F value.
            // So in order the find them out, just multiply the seed with those numbers
            // e.g. 2s + 3s + 4s + 6s, ... the sum should terminate when it exceed the bound.

            long limit = 100000000000L;
            //long limit = 38000000L;
            // Searching for seeds

            // Case 1
            // 5^52 > limits, we can ignore this one

            // Case 2
            // a^7b^3 < limit, therefore a > 7th root of limit/5^3
            //               , therefore b > 3th root of limit/5^7
            int max_a = (int)Math.Pow(limit / Math.Pow(5, 3), 1 / 7.0);
            int max_b = (int)Math.Pow(limit / Math.Pow(5, 7), 1 / 3.0);

            // Case 3
            // c^10d^2 < limit, therefore c > 10th root of limit/5^2
            //                , therefore d >  3th root of limit/5^7
            int max_c = (int)Math.Pow(limit / Math.Pow(5, 2), 1 / 10.0);
            int max_d = (int)Math.Pow(limit / Math.Pow(5, 10), 1 / 2.0);

            // Case 4
            // e^17f < limit, therefore e > 17th root of limit/5
            //              , therefore f >              limit/5^17
            // Since 5^17 > limit, there is no need to look at this one

            // Case 5
            // gh^2i^3 < limit, therefore g > limit/5^3/13^2
            //                  therefore h > 2th root of limit/13/5^3
            //                  therefore i > 3th root of limit/13^2/5^3
            int max_g = (int)(limit / Math.Pow(5, 3) / Math.Pow(13, 2));
            int max_h = (int)Math.Pow((limit / Math.Pow(5, 3) / Math.Pow(13, 1)), 1 / 2.0);
            int max_i = (int)Math.Pow((limit / Math.Pow(5, 3) / Math.Pow(13, 2)), 1 / 2.0);

            int max = new int[] { max_a, max_b, max_c, max_d, max_g, max_h, max_i }.Max();

            var goodPrimes = EulerUtil.Primes(max).Where(t => t % 4 == 1).ToArray();

            List<long> seeds = new List<long>();
            seeds.AddRange(Case2(max_a, max_b, limit, goodPrimes).ToList());
            seeds.AddRange(Case3(max_c, max_d, limit, goodPrimes).ToList());
            seeds.AddRange(Case5(max_g, max_h, max_i, limit, goodPrimes).ToList());

            // Sieving for items without good factors under max
            bool[] hasGoodFactor = new bool[max];

            foreach (var goodPrime in goodPrimes)
            {
                int i = goodPrime;
                while (i < hasGoodFactor.Length)
                {
                    hasGoodFactor[i - 1] = true;
                    i += goodPrime;
                }
            }

            var dummyFactors = Enumerable.Range(1, max).Where(t => !hasGoodFactor[t - 1]).ToArray();

            HashSet<long> found = new HashSet<long>();
            long sum = 0;
            foreach (long seed in seeds)
            {
                for (int i = 0; i < dummyFactors.Length; i++)
                {
                    long valid = seed * dummyFactors[i];
                    if (valid < limit)
                    {
                        sum += valid;
                        found.Add(valid);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine(sum);
            
        }

        private static IEnumerable<long> Case2(int max_a, int max_b, long limit, int[] goodPrimes)
        {
            foreach (var a in goodPrimes)
            {
                if (a > max_a)
                {
                    break;
                }

                foreach (var b in goodPrimes)
                {
                    if (b > max_b)
                    {
                        break;
                    }
                    if (a != b)
                    {
                        double candidate = Math.Pow(a, 7) * (long)Math.Pow(b, 3);
                        if (candidate < limit)
                        {
                            yield return (long)candidate;
                        }
                    }
                }
            }
        }

        private static IEnumerable<long> Case3(int max_c, int max_d, long limit, int[] goodPrimes)
        {
            foreach (var c in goodPrimes)
            {
                if (c > max_c)
                {
                    break;
                }

                foreach (var d in goodPrimes)
                {
                    if (d > max_d)
                    {
                        break;
                    }
                    if (c != d)
                    {
                        double candidate = Math.Pow(c, 10) * (long)Math.Pow(d, 2);
                        if (candidate < limit)
                        {
                            yield return (long)candidate;
                        }
                    }
                }
            }
        }

        // For reasonable running time need to trim this loop more
        private static IEnumerable<long> Case5(int max_g, int max_h, int max_i, long limit, int[] goodPrimes)
        {
            foreach (var i in goodPrimes)
            {
                if (i > max_i)
                {
                    break;
                }

                foreach (var h in goodPrimes)
                {
                    if (h > max_h)
                    {
                        break;
                    }
                    if (i != h)
                    {
                        foreach (var g in goodPrimes)
                        {
                            if (g > max_g)
                            {
                                break;
                            }
                            if (g != h && g != i)
                            {
                                double candidate = (g * Math.Pow(h, 2) * Math.Pow(i, 3));
                                if (candidate < limit)
                                {
                                    yield return (long)candidate;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static long F(long n)
        {
            // The equation for the circle is 
            // x^2 + y^2 - Nx - Ny = 0
            // We need to find the number of different possible N such that the equation has exactly 420 solutions.

            // For even N, let N = 2k
            //
            // x^2 + y^2 - 2kx - 2ky = 0
            // x^2 - 2kx + k^2 + y^2 - 2ky + k^2 = 2k^2
            // (x - k)^2 + (y - k)^2 = 2k^2
            // 
            // Therefore the problem reduce to finding the number of solutions in this equation, that turns out to be
            //
            // Assume k = 2^a_0 x (p_1^a_1 x p_2^a2 x ... ) x (q_1^b_1 x q_2^b_2) where p are primes of form 4x + 3 and q are primes of form 4x + 1
            // Then the number of solutions is product of 4 * product(1 + 2 * b_i)
            //
            // For N is odd, we have
            // 4x^2 + 4y^2 -4Nx - 4Ny = 0
            // (2x - N)^2 + (2y - N)^2 = 2N^2.
            // 
            // If we have a solution for p^2 + q^2 = 2N^2, it is either that both p and q are odd or both p and q are even.
            // Suppose both p and q are even, let p = 2k and q = 2l, we have (2k)^2 + (2l)^2 = 4(k^2 + l^2) = 0 (mod 4).
            // But then let N = 2m + 1, 2N^2 = 2(2m + 1)^2 = 8(m^2 + m) + 2 = 2 (mod 4)
            // Therefore the only possibility is that both p and q are odd.
            // Therefore, if (p, q) is a solution for p^2 + q^2 = 2N^2, (p, q) must be both odd and (x, y) = ((p + N)/2, (q + N)/2) must be an integer solution for (2x - N)^2 + (2y - N)^2 = 2N^2.
            // 
            if (n % 2 == 0)
            {
                n /= 2;
            }
            var factors = EulerUtil.BruteForceFactor(n);
            var factorGroups = factors.GroupBy(i => i).Select(t => Tuple.Create(t.Key, t.Count()));
            long b = 4;
            foreach (var factorGroup in factorGroups)
            {
                if (factorGroup.Item1 % 4 == 1)
                {
                    b *= (1 + factorGroup.Item2 * 2);
                }
            }
            return b;
        }
    }
}
