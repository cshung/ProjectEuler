namespace Problem043
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
            Console.WriteLine(SpecialValues().Aggregate((x, y) => x + y));
        }

        static IEnumerable<long> SpecialValues() 
        {
            var permutations = EulerUtil.Permutations(Enumerable.Range(1, 10).ToArray());
            foreach (var permutation in permutations)
            {
                var p = permutation.Select(d => d - 1);
                var indexed = p.Select((a, b) => Tuple.Create(a, b + 1)).ToList();
                if (GetPart(indexed, 8, 10) % 17 == 0)
                {
                    if (GetPart(indexed, 7, 9) % 13 == 0)
                    {
                        if (GetPart(indexed, 6, 8) % 11 == 0)
                        {
                            if (GetPart(indexed, 5, 7) % 7 == 0)
                            {
                                if (GetPart(indexed, 4, 6) % 5 == 0)
                                {
                                    if (GetPart(indexed, 3, 5) % 3 == 0)
                                    {
                                        if (GetPart(indexed, 2, 4) % 2 == 0)
                                        {
                                            yield return long.Parse(p.Aggregate("", (s, x) => s + x));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int GetPart(List<Tuple<int, int>> indexed, int a, int b)
        {
            return int.Parse(indexed.Where(t => t.Item2 >= a && t.Item2 <= b).Select(t => t.Item1).Aggregate("", (s, x) => s + x));
        }
    }
}
