namespace Problem039
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> primitiveTriplesSum = EulerUtil.GetPrimitivePythTriples(1000, null).Select(t => t.Item1 + t.Item2 + t.Item3).Where(t => t <= 1000).ToList();
            // The number of solution for p is the number of ways it can be written as multiple of primitiveTriplesSum;
            var numberOfSolutions = GetNumberOfSolutions(primitiveTriplesSum).ToList();
            int maxNumberOfSolutions = numberOfSolutions.Select(t => t.Item2).Max();
            Console.WriteLine(numberOfSolutions.Single(t => t.Item2 == maxNumberOfSolutions).Item1);
        }

        private static IEnumerable<Tuple<int, int>> GetNumberOfSolutions(IEnumerable<int> primitiveTriplesSum)
        {
            IEnumerable<int> candidates = Enumerable.Range(1, 1000);
            foreach (var candidate in candidates)
            {
                int numberOfSolutions = primitiveTriplesSum.Where(p => candidate % p == 0).Count();
                yield return Tuple.Create(candidate, numberOfSolutions);
            }
        }
    }
}
