namespace Problem106
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int n = 12;
            List<List<int>> subsets = EulerUtil.Subsets(n).Where(t => t.Count > 0 && t.Count < n).ToList();
            int countCandidate = 0;
            int compareCount = 0;
            for (int i = 0; i < subsets.Count; i++)
            {
                for (int j = i + 1; j < subsets.Count; j++)
                {
                    var left = subsets[i];
                    var right = subsets[j];
                    if (left.Intersect(right).Count() == 0)
                    {
                        countCandidate++;
                        if (left.Count == right.Count)
                        {
                            if (left.Count != 1)
                            {
                                var difference = Enumerable.Zip(left, right, (x, y) => x - y);
                                if (difference.Any(e => e > 0) && difference.Any(e => e < 0))
                                {
                                    // Let's build a code-generator here for problem 103
                                    //var leftCodeString = left.Select(e => e - 1).Select(e => string.Format("ca[{0}]", e)).ToConcatString("+");
                                    //var rightCodeString = right.Select(e => e - 1).Select(e => string.Format("ca[{0}]", e)).ToConcatString("+");
                                    //Console.WriteLine("if (({0}) == ({1})) {{ continue; }}", leftCodeString, rightCodeString);
                                    compareCount++;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(compareCount);
            Console.WriteLine(countCandidate);
        }
    }
}
