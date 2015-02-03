namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        // Greedy algorithm
        // Eliminate big factors one by one by attempting to grow the smallest factor
        public static void Problem500()
        {
            var primes = Primes(8500000).ToArray(); // estimated by prime number theorem - this should be good enough to contain 500500 primes)
            var primes_log = primes.Select(t => Math.Log(t)).ToArray();
            int count = 500500;
            List<int> indices = Enumerable.Range(1, count).Select(t => 1).ToList();

            int left = 0;
            bool hasImprovement;
            do
            {
                hasImprovement = false;
                int right = indices.Count - 1;
                double leftIncreaseLog = primes_log[left] * (indices[left] + 1);
                double rightDecreaseLog = primes_log[right];
                if (leftIncreaseLog < rightDecreaseLog)
                {
                    indices[left] += (indices[left] + 1);
                    indices.RemoveAt(right);
                    hasImprovement = true;
                }
                else
                {
                    if (indices[left] != 1)
                    {
                        left++;
                        hasImprovement = true;
                    }
                }
            } while (hasImprovement);
            IEnumerable<long> powered = Enumerable.Zip(primes, indices, (p, i) => Power((long)p, i, 1, (x, y) => (x * y) % 500500507));
            Console.WriteLine(powered.Aggregate(1L, (x, y) => (x * y) % 500500507));
        }
    }
}