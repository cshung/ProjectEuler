namespace Problem023
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            List<int> abundantNumbers = new List<int>();
            for (int i = 2; i <= 28124; i++)
            {
                if (IsAbundantNumbers(i))
                {
                    abundantNumbers.Add(i);
                }
            }
            // Find out the set of numbers that cannot be written as a sum of two abundant numbers.
            HashSet<int> abundantSums = new HashSet<int>();
            foreach (int abundantNumber1 in abundantNumbers)
            {
                foreach (int abundantNumber2 in abundantNumbers)
                {
                    abundantSums.Add(abundantNumber1 + abundantNumber2);
                }
            }
            long answer = (1 + 23) * 23 / 2;
            for (int i = 24; i < 28124; i++)
            {
                if (!abundantSums.Contains(i))
                {
                    answer += i;
                }
            }
            Console.WriteLine(answer);
        }

        private static bool IsAbundantNumbers(int i)
        {
            return SumOfProperDivisor(i) > i;
        }

        private static int SumOfProperDivisor(int i)
        {
            return SumOfDivisor(i) - i;
        }

        private static int SumOfDivisor(int i)
        {
            List<KeyValuePair<int, int>> groupedFactors = Factor(i).GroupBy(t => t).Select(g => new KeyValuePair<int, int>(g.Key, g.Count())).ToList();
            return groupedFactors.Select(kvp => (Power(kvp.Key, kvp.Value + 1) - 1) / (kvp.Key - 1)).Aggregate((x, y) => x * y);
        }

        private static int Power(int x, int y)
        {
            if (y == 0)
            {
                return 1;
            }
            else if (y == 1)
            {
                return x;
            }
            else
            {
                int r = Power(x, y / 2);
                return y % 2 == 0 ? r * r : r * r * x;
            }

        }

        private static IEnumerable<int> Factor(int n)
        {
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    foreach (int otherFactors in Factor(n / i))
                    {
                        yield return otherFactors;
                    }
                    yield break;
                }
            }
            yield return n;
        }
    }
}
