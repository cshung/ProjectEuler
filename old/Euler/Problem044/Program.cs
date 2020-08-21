namespace Problem044
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Numerics;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;

    class Program
    {
        public static void Main(string[] args)
        {
            AdvancedSolution();
        }

        public static GaussianInteger Power(GaussianInteger value, int power)
        {
            return EulerUtil.Power<GaussianInteger>(value, power, (x, y) => GaussianInteger.Multiply(x, y), GaussianInteger.Create(1L, 0L));
        }

        public static void BruteForceSolutionWithHint()
        {
            List<BigInteger> pent = Enumerable.Range(1, 4800).Select(t => new BigInteger(t)).Select(t => t * (3 * t - 1) / 2).ToList();
            int i = 0;
            int j = 0;
            Parallel.ForEach(pent, delegate(BigInteger pent1)
            {
                Console.Write('.');
                Parallel.ForEach(pent, delegate(BigInteger pent2)
                {
                    if (pent2 > pent1)
                    {
                        BigInteger diff = pent2 - pent1;
                        if (pent.Contains(diff))
                        {
                            BigInteger sum = pent1 + pent2;
                            if (pent.Contains(sum))
                            {
                                Console.WriteLine(i);
                                Console.WriteLine(j);
                                Console.WriteLine(diff);
                                Console.WriteLine(pent1);
                                Console.WriteLine(pent2);
                                Console.WriteLine(sum);

                                Console.WriteLine("Hit!");
                                throw new Exception();
                            }
                        }
                    }
                    Interlocked.Increment(ref j);
                });
                Interlocked.Increment(ref i);
            });
        }

        public static void AdvancedSolution()
        {
            // First, pentagon numbers can be represented as a square like this as follow:
            // n(3n - 1)/2 = (3n^2 - n)/2 = (36n^2 - 12n)/24 = ((36n^2 - 12n + 1) - 1)/24 = ((6n - 1)^2 - 1)/24
            //
            // So if we have two pentagon numbers sums to another, we have two squares sum to a square + 1 as follow:
            //
            // ((6a - 1)^2 - 1)/24 + ((6b - 1)^2 - 1)/24 = ((6c - 1)^2 - 1)/24
            //           (6a - 1)^2 - 1 + (6b - 1)^2 - 1 = (6c - 1)^2 - 1
            //                   (6a - 1)^2 + (6b - 1)^2 = (6c - 1)^2 + 1
            //
            // So the problem of searching is reduced to searching this special pattern (very similar to Pyth's triplet except the + 1)
            // 
            // So I searched a little bit on how to enumerate the ways an integer decompose into sum of two squares
            // http://mathoverflow.net/questions/29644/enumerating-ways-to-decompose-an-integer-into-the-sum-of-two-squares
            // 
            // This lead to factorizing an integer into Gaussian integer primes
            // http://stackoverflow.com/questions/2269810/whats-a-nice-method-to-factor-gaussian-integers
            // 
            // And then this leads to finding modular square roots
            // http://en.wikipedia.org/wiki/Tonelli%E2%80%93Shanks_algorithm
            //
            // Looking at these complexities - this problem is quite hard.
            //

            Random random = new Random(0);
            long i = 0;
            while (true)
            {
                i++;
                long toDecompose = (6 * i - 1);
                toDecompose = toDecompose * toDecompose;
                toDecompose++;
                //Console.WriteLine("Decomposing: " + toDecompose);
                var decompositions = Decompose(toDecompose);
                foreach (var decomposition in decompositions)
                {                    
                    long a = Math.Abs(decomposition.Item1);
                    long b = Math.Abs(decomposition.Item2);
                    Debug.Assert(a * a + b * b == toDecompose);
                    //Console.WriteLine("Decomposed into " + (a * a) + " + " + (b * b));
                    if ((a + 1) % 6 == 0)
                    {
                        if ((b + 1) % 6 == 0)
                        {
                            a = (a + 1) / 6;
                            b = (b + 1) / 6;
                            long A = a * (3 * a - 1) / 2;
                            long B = b * (3 * b - 1) / 2;
                            long C = i * (3 * i - 1) / 2;
                            //Console.WriteLine(a + " " + b);
                            //Console.WriteLine(A + " + " + B + " = "  + C);

                            // I want to know whether B + C is a pentagon number, to check, we have
                            // (B + C) = ((6n - 1)^2 + 1) / 24
                            // 24(B + C) - 1 = Perfect square mod 6 = 5 => work

                            if (EulerUtil.IsPentagonNumber(B + C))
                            {
                                Console.WriteLine(string.Format("{0} + {1} = {2} and {3} + {4} = {5}", A, B, C, B, C, (B + C)));
                                return;
                            }
                            if (EulerUtil.IsPentagonNumber(A + C))
                            {
                                Console.WriteLine(string.Format("{0} + {1} = {2} and {3} + {4} = {5}", A, B, C, A, C, (A + C)));
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static IEnumerable<Tuple<long, long>> Decompose(long j)
        {
            var source = GaussianInteger.Create(j, 0);
            var factorization = GaussianInteger.Factorize(source).ToList();

            //var target = factorization.Aggregate((x, y) => GaussianInteger.Multiply(x, y));
            //Debug.Assert(source.Real == target.Real);
            //Debug.Assert(source.Imag == target.Imag);

            // real parts
            long real = factorization.Where(t => t.Imag == 0).Aggregate(1L, (x, y) => x * long.Parse(y.Real.ToString()));
            long realRoot = (long)Math.Sqrt(real);
            if (realRoot * realRoot == real)
            {
                // Grouping Conjugate pairs (at the same time reduce power by half)
                List<Tuple<GaussianInteger, int>> groupedConjugatePairs = factorization.Where(t => t.Imag != 0).GroupBy(t => t.Imag > 0 ? t : t.Conjugate()).Select(t => Tuple.Create(t.Key, t.Count() / 2)).ToList();

                if (groupedConjugatePairs.Count > 0)
                {
                    //IEnumerable<int> shouldBeEven = factorization.Where(t => t.Imag != 0).GroupBy(t => t.Imag > 0 ? t : t.Conjugate()).Select(t => t.Count());
                    //bool checkEven = shouldBeEven.Select(t => t % 2 == 0).Aggregate((x, y) => x && y);
                    //Console.WriteLine(shouldBeEven.ToConcatString(" "));
                    //Debug.Assert(checkEven, "First check");

                    List<int> powerList = groupedConjugatePairs.Select(t => t.Item2).ToList();
                    powerList[0]--;
                    //Console.WriteLine("Power List: " + powerList.ToConcatString(" "));
                    //Console.WriteLine(conjugateCounts.Select(t => t.ToConcatString(" ")).ToConcatString("\n"));
                    foreach (var conjugateCount in EulerUtil.MultiRadixNumbers(powerList))
                    {
                        var groupedConjugatePairsWithCounts = EulerUtil.StackLists(groupedConjugatePairs, conjugateCount);
                        //GaussianInteger reconstructHere = GaussianInteger.Multiply(GaussianInteger.Create(real, 0L), groupedConjugatePairsWithCounts.Select(t => Power(t.Item1.Item1, t.Item1.Item2)).Select(t => GaussianInteger.Multiply(t, t.Conjugate())).Aggregate(GaussianInteger.Create(1L, 0L), (x, y) => GaussianInteger.Multiply(x, y)));
                        //Debug.Assert(reconstructHere.Real == j, "Second check");

                        IEnumerable<GaussianInteger> conjugatedValues = groupedConjugatePairsWithCounts.Select(t => GaussianInteger.Multiply(Power(t.Item1.Item1, t.Item1.Item2 - t.Item2), Power(t.Item1.Item1.Conjugate(), t.Item2)));

                        var representation = conjugatedValues.Aggregate(GaussianInteger.Create(1L, 0L), (x, y) => GaussianInteger.Multiply(x, y));
                        //yield return Tuple.Create(real, representation);
                        var a = realRoot * representation.Real;
                        var b = realRoot * representation.Imag;


                        yield return Tuple.Create(long.Parse(a.ToString()), long.Parse(b.ToString()));
                        //var reconstruct = GaussianInteger.Multiply(GaussianInteger.Create(real, 0L), GaussianInteger.Multiply(representation, representation.Conjugate()));
                        //Debug.Assert(reconstruct.Equals(GaussianInteger.Create(j, 0)));
                    }
                }
            }
        }
    }
}