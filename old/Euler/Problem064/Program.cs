using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem064
{
    class Program
    {
        // See TEX for derivation
        // Also note this link: http://www.alpertron.com.ar/QUAD.HTM
        static void Main(string[] args)
        {
            HashSet<int> squares = new HashSet<int>(Enumerable.Range(1, 100).Select(t => t * t));
            int oddPeriodCount = 0;
            for (int i = 1; i <= 10000; i++)
            {
                if (!squares.Contains(i))
                {
                    QuadraticIrrational start = new QuadraticIrrational { A = 0, B = 1, C = i, D = 1 };
                    var continuedFraction = EulerUtil.ContinuedFraction(start);
                    Console.WriteLine("sqrt({2}) = [{0}; ({1})*]", continuedFraction.Item1, continuedFraction.Item2.ToConcatString(","), i);
                    int periodLength = continuedFraction.Item2.Count();
                    if (periodLength % 2 == 1)
                    {
                        oddPeriodCount++;
                    }
                }
            }

            Console.WriteLine(oddPeriodCount);
        }
    }
}
