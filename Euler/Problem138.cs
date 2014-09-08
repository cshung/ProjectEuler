namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        //
        // h = b + 1 or b - 1
        // h^2 + (b/2)^2 = l^2
        // 4h^2 + b^2 = 4l^2 
        // That imply b^2 is even, which mean b is even
        // 
        // 4(b+/-1)^2 + b^2 -4l^2 = 0
        // 5b^2 +/- 8b - 4l^2 + 4 = 0
        // Solving the quadratic diophantine equation gives
        // b_0 = 0
        // l_0 = -1
        // b_{n+1} = -9b_n - 8l_n +/- 8
        // l_{n+1} = -10b_n - 9l_n +/- 8
        //
        // Experiment observed the sign change just flip the sign of the solution.
        // So just need to run one recursion.
        // Sum all the 12 L gives the answer
        //
        public static void Problem138()
        {
            int count = 0;
            long b = 0;
            long l = -1;
            long sum = 0;
            while (true)
            {
                long nb = -9 * b - 8 * l - 8;
                long nl = -10 * b - 9 * l - 8;
                b = nb;
                l = nl;
                if (b != 0)
                {
                    count++;
                    if (l > 0)
                    {
                        sum += l;
                    }
                    else
                    {
                        sum -= l;
                    }
                    if (count == 12)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}