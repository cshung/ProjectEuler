namespace Problem125
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            int limit = (int)Math.Round(Math.Pow(10, 8));
            //limit = 10000;
            int max = (int)Math.Ceiling(Math.Sqrt(limit));
            long[] runningSums = new long[max];
            long runningSum = 0;
            for (int i = 0; i < max; i++)
            {
                int n = i + 1;
                int nSquared = n * n;
                runningSum += nSquared;
                runningSums[i] = runningSum;
            }

            int count = 0;
            //Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6}", "Good?", "Count", "From", "To", "Length", "Sum", "Recount");

            HashSet<long> good = new HashSet<long>();
            for (int i = -1; i < max; i++)
            {
                for (int j = i + 1; j < max; j++)
                {
                    long consecutiveSum = runningSums[j] - (i == -1 ? 0 : runningSums[i]);
                    if (consecutiveSum < limit)
                    {
                        string consecutiveSumString = consecutiveSum.ToString();

                        if (consecutiveSumString.Reverse().Aggregate(string.Empty, (x, y) => x + y) == consecutiveSumString)
                        {
                            count++;
                            if (i + 2 != j + 1)
                            {
                                good.Add(consecutiveSum);
                                //int length = (j + 1) - (i + 2) + 1;
                                //long recount = Enumerable.Range(i + 2, length).Select(e => (long)e).Select(e => e * e).Sum();
                                //Console.WriteLine("{0} \t {1} \t {2} \t {3} \t {4} \t {5} \t {6}", "Good", count, (i + 2), (j + 1), length, consecutiveSum, recount);
                                //if (consecutiveSum != recount)
                                //{
                                //    Console.WriteLine("BOOM!");
                                //}
                            }
                        }
                    }
                }
            }
            Console.WriteLine(good.Sum());
        }
    }
}
