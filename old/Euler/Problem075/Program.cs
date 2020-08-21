using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem075
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 1500000;
            int[] fuck = new int[max + 1];
            var tripleSums = new HashSet<int>(EulerUtil.GetPrimitivePythTriples(max, null).Select(t => t.Item1 + t.Item2 + t.Item3));
            foreach (var tripleSum in tripleSums)
            {
                int current = tripleSum;
                while (current <= max)
                {
                    fuck[current]++;
                    current += tripleSum;
                }
            }
            int uniqueCount = 0;
            for (int i = 1; i < fuck.Length; i++)
            {
                if (fuck[i] == 1)
                {
                    uniqueCount++;
                }
            }

            Console.WriteLine(uniqueCount);
        }
    }
}
