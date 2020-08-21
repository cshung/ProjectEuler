using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem021
{
    class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            d.Add(1, 0);
            for (int i = 2; i <= 10000; i++)
            {
                d.Add(i, SumOfProperDivisor(i));
            }
            int maxSumOfDivisorWithin10000 = d.Values.Max();
            for (int i = 10001; i <= maxSumOfDivisorWithin10000; i++)
            {
                d.Add(i, SumOfProperDivisor(i));
            }
            Console.WriteLine(AmicableNumbers(d).Aggregate((x, y) => x + y));
        }

        private static IEnumerable<int> AmicableNumbers(Dictionary<int, int> d)
        {
            for (int i = 2; i <= 10000; i++)
            {
                if (d[d[i]] == i & i != d[i])
                {
                    yield return i;
                }
            }
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
