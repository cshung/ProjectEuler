namespace Euler
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal static partial class Program
    {

        class Solution029
        {
            public void Run()
            {
                int A = 100;
                int B = 100;
                long sum = 0;
                bool[] processed = new bool[A + 1];
                for (int a = 2; a <= A; a++)
                {
                    if (!processed[a])
                    {
                        int numberOfSets = (int)Math.Log(B, a);
                        int c = a;
                        for (int i = 1; i <= numberOfSets; i++)
                        {
                            processed[c] = true;
                            c *= a;
                        }
                        IEnumerable<long> allPowers = Enumerable.Range(1, numberOfSets).Select(t => (long)t);
                        foreach (var code in Gray(numberOfSets))
                        {
                            var joined = Enumerable.Zip(allPowers, code.Item1, (x, y) => Tuple.Create(x, y));
                            var powers = joined.Where(t => t.Item2).Select(t => t.Item1);
                            if (powers.Count() == 0)
                            {
                                // Gray code generate this sequence - but this should simply be discarded since it make
                                // no sense for having an intersection of no sets.
                                // One could argue this is simply empty set and contribute 0 to sum, that's fine too.
                                continue;
                            }
                            // The size of the intersection of a particular subset can be computed using this formula
                            // min(powers) * B / lcm
                            var lcm = powers.Aggregate((x, y) => CommonMultiple(x, y));
                            var min = powers.Min();
                            var intersectionSize = min * B / lcm;
                            if (powers.Contains(lcm))
                            {
                                intersectionSize--;
                            }
                            sum = sum + (code.Item2 ? -1 : 1) * intersectionSize;
                        }
                    }
                }
                Console.WriteLine(sum);
            }

            // From "The Art of Computer Programming, book 4"
            private IEnumerable<Tuple<List<bool>, bool>> Gray(int n)
            {
                int parity = 0;
                bool[] bits = new bool[n];
                int j = 0;
                while (true)
                {
                    // Iterating gray code and present the inclusion-exclusion sum
                    yield return Tuple.Create(bits.ToList(), parity == 0);
                    parity = 1 - parity;
                    if (parity == 1)
                    {
                        j = 0;
                    }
                    else
                    {
                        int k = 0;
                        while (true)
                        {
                            if (bits[k])
                            {
                                break;
                            }
                            else
                            {
                                k++;
                            }
                        }
                        // Now we know bits[k] is true, and the minimal such k, then 
                        j = k + 1;
                    }
                    if (j == n)
                    {
                        yield break;
                    }
                    else
                    {
                        bits[j] = !bits[j];
                    }
                }
            }
        }
        public static void Problem029()
        {
            new Solution029().Run();
        }
    }
}
