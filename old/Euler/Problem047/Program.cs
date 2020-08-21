using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem047
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 1000000;
            var numberOfDistinctFactors = Enumerable.Range(1, max).Select(t => EulerUtil.BruteForceFactor(t).GroupBy(u => u).Count()).ToList();
            int hit = 0;
            int target = 4;
            for (int i = 0; i < max; i++)
            {
                if (numberOfDistinctFactors[i] == target)
                {
                    hit++;
                    if (hit == target)
                    {
                        Console.WriteLine(i - (target - 1) + 1);
                        return;
                    }
                }
                else
                {
                    hit = 0;
                }
            }
        }
    }
}
