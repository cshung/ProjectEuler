using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem071
{
    class Program
    {
        // Basically, the problem can be formulated as an optimization as follow:
        // Minimize 3/7 - n/d
        // Subject to (n, d) are integers, (n, d) = 1, 1 <= n < d, 1 <= d <= 1000000, 3/7 - n/d > 0
        // 
        // Massaging the objective function a bit, we have objective = (3d - 7n)/7d, so in general we want big d, and big n so 
        // that the numerator is small and the denominator is large.
        // It is quite easy to optimize for n when d is fixed, so the basic strategy is to try all d, and find feasible n that 
        // minimize the objective.
        static void Main(string[] args)
        {
            double optimal = double.PositiveInfinity;
            long optimalN = 0;
            long optimalD = 0;
            for (long d = 1; d <= 1000000; d++)
            {
                // n is as large as possible, but not too large
                // the largest possible n is when 3d - 7n > 0 => 7n < 3d => n < 3d/7
                // note that n < d automatically by this, so don't worry about the n < d constraint

                long n = 3 * d / 7;  // n = floor(3d/7)
                if ((3 * d) % 7 == 0)
                {
                    // Otherwise the constraint n < 3d/7 is not hit because in this case n = 3d/7 exactly.
                    n--;
                }

                bool feasible = (n > 0) && (3 * d > 7 * n);
                while (feasible && EulerUtil.CommonFactor(n, d) != 1)
                {
                    n--;
                    feasible = (n > 0) && (3 * d > 7 * n);
                }

                if (!feasible)
                {
                    continue;
                }

                double candidateOptimal = (3 * d - 7.0 * n) / 7 / d;
                if (candidateOptimal < optimal)
                {
                    optimal = candidateOptimal;
                    optimalN = n;
                    optimalD = d;
                }
            }
            Console.WriteLine(optimal + " " + optimalN + "/" + optimalD);
        }
    }
}
