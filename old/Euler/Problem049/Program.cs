namespace Problem049
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(FindTuples().Distinct().ToConcatString());
        }

        static IEnumerable<Tuple<int, int, int>> FindTuples()
        {
            var brokenNumbers = Enumerable.Range(0, 10000).Select(t => t.ToString().PadLeft(4, '0').ToArray().Select(c => c - '0'));
            var primes = new HashSet<int>(EulerUtil.Primes(10000));
            foreach (var brokenNumber in brokenNumbers)
            {
                List<int> primePermutedNumbers = EulerUtil.Permutations(brokenNumber).Select(t => t.Aggregate((x, y) => 10 * x + y)).Where(t => primes.Contains(t)).Distinct().ToList();
                primePermutedNumbers.Sort();

                foreach (var combination in EulerUtil.Combinations(primePermutedNumbers, 3).Select(t => t.ToList()))
                {
                    int a = combination[0];
                    int b = combination[1];
                    int c = combination[2];
                    if (c < b)
                    {
                        Debug.Fail("Should not happen");
                    }

                    if (b < a)
                    {
                        Debug.Fail("Should not happen");
                    }

                    // No leading zero and AP constraint
                    if (a > 999 & b > 999 & c > 999 && b - a == c - b)
                    {
                        yield return Tuple.Create(a, b, c);
                    }
                }
            }
        }
    }
}
