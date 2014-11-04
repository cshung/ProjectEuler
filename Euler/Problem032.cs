namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem032()
        {
            HashSet<int> results = new HashSet<int>();
            foreach (var permutation in Permutations(Enumerable.Range(1, 9).ToArray()))
            {
                // Break down the permutation into list of different partitions and check product formula
                for (int firstPart = 1; firstPart < 9; firstPart++)
                {
                    for (int secondPart = 1; secondPart < 9; secondPart++)
                    {
                        int thirdPart = 9 - firstPart - secondPart;
                        if (thirdPart > 0)
                        {
                            string permutationString = permutation.Aggregate("", (s, x) => s + x);
                            int firstPartValue = int.Parse(permutationString.Substring(0, firstPart));
                            int secondPartValue = int.Parse(permutationString.Substring(firstPart, secondPart));
                            int thirdPartValue = int.Parse(permutationString.Substring(9 - thirdPart));
                            if (firstPartValue == secondPartValue * thirdPartValue)
                            {
                                results.Add(firstPartValue);
                                //Console.WriteLine("{0} = {1} x {2}", firstPartValue, secondPartValue, thirdPartValue);
                            }
                        }
                    }
                }
            }
            Console.WriteLine(results.Aggregate((x, y) => x + y));
        }
    }
}
