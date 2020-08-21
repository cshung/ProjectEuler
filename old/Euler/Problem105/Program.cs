using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem105
{
    class Program
    {
        static int sum = 0;

        static void Main(string[] args)
        {
            string input = EulerUtil.ReadResourceAsString("Problem105.sets.txt");
            string[] lines = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            foreach (string line in lines)
            {
                int[] ca = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => int.Parse(t)).ToArray();
                Array.Sort(ca);
                if (ca.Distinct().Count() != ca.Length)
                {
                    continue;
                }
                if (Check(ca))
                {
                    sum += ca.Sum();
                }
            }
            Console.WriteLine(sum);
        }

        static Dictionary<int, List<Tuple<List<int>, List<int>>>> checkRuleMap = new Dictionary<int, List<Tuple<List<int>, List<int>>>>();

        private static bool Check(int[] ca)
        {
            int n = ca.Length;
            List<Tuple<List<int>, List<int>>> checkRules;
            if (!checkRuleMap.TryGetValue(ca.Length, out checkRules))
            {
                checkRules = new List<Tuple<List<int>, List<int>>>();

                List<List<int>> subsets = EulerUtil.Subsets(n).Where(t => t.Count > 0 && t.Count < n).ToList();
                for (int i = 0; i < subsets.Count; i++)
                {
                    for (int j = i + 1; j < subsets.Count; j++)
                    {
                        var left = subsets[i];
                        var right = subsets[j];
                        if (left.Intersect(right).Count() == 0)
                        {
                            if (left.Count == right.Count)
                            {
                                if (left.Count != 1)
                                {
                                    var difference = Enumerable.Zip(left, right, (x, y) => x - y);
                                    if (difference.Any(e => e > 0) && difference.Any(e => e < 0))
                                    {
                                        checkRules.Add(Tuple.Create(left.Select(e => e - 1).ToList(), right.Select(e => e - 1).ToList()));
                                    }
                                }
                            }
                        }
                    }
                }

                checkRuleMap.Add(n, checkRules);
            }

            int l = 2, r = n;
            while (l < r)
            {
                var smallSetIndex = Enumerable.Range(1, l).Select(t => t - 1);
                var largeSetIndex = Enumerable.Range(1, n - r + 1).Select(t => n - t);
                if (smallSetIndex.Select(t => ca[t]).Sum() <= largeSetIndex.Select(t => ca[t]).Sum())
                {
                    return false;
                }
                l++;
                r--;
            }

            // Use the check rules
            foreach (var checkRule in checkRules)
            {
                if (checkRule.Item1.Select(t => ca[t]).Sum() == checkRule.Item2.Select(t => ca[t]).Sum())
                {
                    return false;
                }
            }

            return true;
        }
    }
}