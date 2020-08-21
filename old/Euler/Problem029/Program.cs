namespace Problem029
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;

    class Program
    {
        static void Main(string[] args)
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
                    IEnumerable<long> allMultiples = Enumerable.Range(1, numberOfSets).Select(t => (long)t);
                    foreach (var code in Gray(numberOfSets))
                    {
                        var joined = EulerUtil.StackLists(allMultiples, code.Item1);
                        var multiples = joined.Where(t => t.Item2).Select(t => t.Item1);
                        if (multiples.Count() == 0)
                        {
                            // Gray code generate this sequence - but this should simply be discarded since it make
                            // no sense for having an intersection of no sets.
                            // One could argue this is simply empty set and contribute 0 to sum, that's fine too.
                            continue;
                        }
                        // The size of the intersection of a particular subset can be computed using this formula
                        // min(multiple) * B / lcm
                        var lcm = multiples.Aggregate((x, y) => EulerUtil.CommonMultiple(x, y));
                        var min = multiples.Min();
                        var intersectionSize = min * B / lcm;
                        if (multiples.Contains(lcm))
                        {
                            intersectionSize--;
                        }
                        //Console.WriteLine("This set of multiples  = {" + filtered.Aggregate("", (s, x) => s + " " + x).Trim() + "}");
                        //Console.WriteLine("Intersection size      = " + intersectionSize);
                        sum = sum + (code.Item2 ? -1 : 1) * intersectionSize;
                    }
                }
            }
            Console.WriteLine(sum);
        }

        // From "The Art of Computer Programming, book 4"
        private static IEnumerable<Tuple<List<bool>, bool>> Gray(int n)
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
}
