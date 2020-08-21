using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;

namespace Problem088
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var minSumProdNumbers = new HashSet<int>();
            for (int k = 2; k <= 12000; k++)
            {
                //Console.Write(k);
                int goal = k;
                bool found = false;
                while (!found)
                {
                    foreach (var factorization in Factorizations(goal))
                    {
                        var factorizationArray = factorization.ToArray();
                        int oneCount = k - factorizationArray.Length;
                        if (oneCount >= 0)
                        {
                            int sum = factorizationArray.Aggregate((x, y) => x + y) + oneCount;
                            if (sum == goal)
                            {
                                minSumProdNumbers.Add(goal);
                                found = true;
                                break;
                            }
                        }
                    }

                    goal++;
                }
            }
            Console.WriteLine(minSumProdNumbers.Aggregate((x, y) => x + y));
            Console.WriteLine(sw.Elapsed);
        }

        private static IEnumerable<IEnumerable<int>> Factorizations(int i)
        {
            var factors = EulerUtil.BruteForceFactor(i).ToArray();
            var groupedFactors = factors.GroupBy(t => t).Select(t => Tuple.Create(t.Key, t.Count())).ToArray();
            var primes = groupedFactors.Select(t => (int)t.Item1).ToList();
            var powers = groupedFactors.Select(t => t.Item2).ToList();
            foreach (var factorization in Factorizations(powers, null, 0))
            {
                yield return
                    factorization.
                    // Each factorization object is a list of powers, stack it with the primes
                    Select(t => EulerUtil.StackLists(primes, t)).

                    // Then computes the powers - and compute the product of these powers
                    Select(u => u.Select(v => EulerUtil.Power<int>(v.Item1, v.Item2, (x, y) => x * y, 1)).Aggregate((x, y) => x * y));
            }
        }

        private static IEnumerable<IEnumerable<IEnumerable<int>>> Factorizations(IEnumerable<int> powers, string currentMax, int n)
        {
            string debugHeader = new string(' ', n * 2);
            bool first = true;
            int prod = powers.Select(t => t + 1).Aggregate((x, y) => x * y);
            int count = 0;
            foreach (var head in EulerUtil.MultiRadixNumbers(powers.ToList()))
            {
                count++;
                if (first)
                {
                    // This is picking 1 as head, we need to exclude this case
                    first = false;
                }
                else
                {
                    if (count == prod)
                    {
                        // This is picking all powers as head - no need to do any recursive calls.
                        yield return new List<IEnumerable<int>> { head };
                    }
                    else
                    {
                        //Console.WriteLine(debugHeader + "Considering " + head.ToConcatString(" "));
                        string headString = new string(head.Select(t => 'A' + t).Select(t => (char)t).ToArray());

                        if (currentMax == null || string.Compare(headString, currentMax) >= 0)
                        {
                            var rest = EulerUtil.StackLists(powers, head).Select(t => t.Item1 - t.Item2);
                            //Console.WriteLine(debugHeader + "Computed rest to be " + rest.ToConcatString(" "));
                            string restString = new string(rest.Select(t => 'A' + t).Select(t => (char)t).ToArray());

                            if (string.Compare(restString, headString) >= 0)
                            {
                                //Console.WriteLine(debugHeader + "It works");
                                foreach (var restFactorizations in Factorizations(rest, headString, n + 1))
                                {
                                    yield return new List<IEnumerable<int>> { head }.Concat(restFactorizations);
                                }
                                //Console.WriteLine(head.ToConcatString(" ") + " . Factors(" + rest.ToConcatString(" ") + ")");
                            }
                            else
                            {
                                //Console.WriteLine(debugHeader + "It doesn't work");
                            }
                        }
                        else
                        {
                            //Console.WriteLine(debugHeader + "skipped");
                        }
                    }
                }
            }
        }

        static void SlowPartitionApproach()
        {
            // The partition approach is way too slow - try factoring instead
            var minSumProdNumbers = new HashSet<int>();
            for (int k = 2; k <= 12000; k++)
            {
                Console.Write(k);
                int goal = k;
                bool found = false;
                while (!found)
                {
                    Console.Write('.');
                    foreach (var partition in Partition(goal, 1, k))
                    {

                        //Console.Write('*');
                        var partitionArray = partition.ToArray();
                        //Console.WriteLine(goal + " -> " + partitionArray.ToConcatString(" "));
                        long prod = partitionArray.Select(t => (long)t).Aggregate((x, y) => x * y);
                        if (prod == goal)
                        {
                            found = true;
                            minSumProdNumbers.Add(goal);
                            break;
                        }
                    }
                    goal++;
                }
                Console.WriteLine();
            }
            Console.WriteLine(minSumProdNumbers.Aggregate((x, y) => x + y));
        }

        static IEnumerable<IEnumerable<int>> Partition(int goal, int currentMax, int n)
        {
            if (goal < currentMax * n)
            {
                yield break;
            }
            else
            {
                if (n > 1)
                {
                    // goal - head >= head * (n - 1)
                    // goal >= head * n
                    for (int head = currentMax; head <= goal / n; head++)
                    {
                        foreach (var subList in Partition(goal - head, head, n - 1))
                        {
                            yield return new List<int> { head }.Concat(subList);
                        }
                    }
                }
                else
                {
                    yield return new List<int> { goal };
                }
            }
        }
    }
}
