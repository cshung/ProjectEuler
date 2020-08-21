using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem074
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] factorial = new int[10];

            factorial[0] = 1;
            for (int i = 1; i < 10; i++)
            {
                factorial[i] = factorial[i - 1] * i;
            }

            int sixtyChainCount = 0;
            Dictionary<int, int> dfsCache = new Dictionary<int, int>();
            for (int i = 1; i <= 1000000; i++)
            {
                var chain = new HashSet<int>();
                chain.Add(i);
                int j = i;
                while (true)
                {
                    int dfs = GetDigitFactorialSum(factorial, dfsCache, j);
                    if (chain.Contains(dfs))
                    {
                        break;
                    }
                    else
                    {
                        chain.Add(dfs);
                        j = dfs;
                    }
                }
                if (chain.Count == 60)
                {
                    sixtyChainCount++;
                }
            }

            Console.WriteLine(sixtyChainCount);
        }

        private static int GetDigitFactorialSum(int[] factorial, Dictionary<int, int> dfsCache, int i)
        {
            int dfs;
            if (!dfsCache.TryGetValue(i, out dfs))
            {
                dfs = i.ToString().Select(c => c - '0').Select(c => factorial[c]).Aggregate((x, y) => x + y);
                dfsCache.Add(i, dfs);
            }

            return dfs;
        }
    }
}
