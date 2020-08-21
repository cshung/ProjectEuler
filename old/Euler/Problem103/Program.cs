using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem103
{
    class Program
    {
        // Take a while for brainless brute force, but it worked, be a little patient it is something like a minute.
        static void Main(string[] args)
        {
            int start = 11;
            // This is a guess, we need to stop somewhere.
            int end = 50;
            int n = 7;
            foreach (var combination in EulerUtil.Combinations(Enumerable.Range(start, end - start + 1), n))
            {
                int[] ca = combination.ToArray();
                int a = ca[0];
                int b = ca[1];
                int c = ca[2];
                int d = ca[3];
                int e = ca[4];
                int f = ca[5];
                int g = ca[6];

                if (a + b <= g)
                {
                    continue;
                }

                if (a + b + c <= f + g)
                {
                    continue;
                }

                if (a + b + c + d <= e + f + g)
                {
                    continue;
                }

                // Code generated using Problem 106
                if ((ca[1] + ca[2]) == (ca[0] + ca[3])) { continue; }
                if ((ca[1] + ca[2]) == (ca[0] + ca[4])) { continue; }
                if ((ca[1] + ca[2]) == (ca[0] + ca[5])) { continue; }
                if ((ca[1] + ca[2]) == (ca[0] + ca[6])) { continue; }
                if ((ca[1] + ca[3]) == (ca[0] + ca[4])) { continue; }
                if ((ca[1] + ca[3]) == (ca[0] + ca[5])) { continue; }
                if ((ca[1] + ca[3]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[3]) == (ca[0] + ca[4])) { continue; }
                if ((ca[2] + ca[3]) == (ca[1] + ca[4])) { continue; }
                if ((ca[2] + ca[3]) == (ca[0] + ca[5])) { continue; }
                if ((ca[2] + ca[3]) == (ca[1] + ca[5])) { continue; }
                if ((ca[2] + ca[3]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[3]) == (ca[1] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[3]) == (ca[0] + ca[4] + ca[5])) { continue; }
                if ((ca[1] + ca[2] + ca[3]) == (ca[0] + ca[4] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[3]) == (ca[0] + ca[5] + ca[6])) { continue; }
                if ((ca[1] + ca[4]) == (ca[0] + ca[5])) { continue; }
                if ((ca[1] + ca[4]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[4]) == (ca[0] + ca[5])) { continue; }
                if ((ca[2] + ca[4]) == (ca[1] + ca[5])) { continue; }
                if ((ca[2] + ca[4]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[4]) == (ca[1] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[4]) == (ca[0] + ca[3] + ca[5])) { continue; }
                if ((ca[1] + ca[2] + ca[4]) == (ca[0] + ca[3] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[4]) == (ca[0] + ca[5] + ca[6])) { continue; }
                if ((ca[3] + ca[4]) == (ca[0] + ca[5])) { continue; }
                if ((ca[3] + ca[4]) == (ca[1] + ca[5])) { continue; }
                if ((ca[3] + ca[4]) == (ca[2] + ca[5])) { continue; }
                if ((ca[3] + ca[4]) == (ca[0] + ca[6])) { continue; }
                if ((ca[3] + ca[4]) == (ca[1] + ca[6])) { continue; }
                if ((ca[3] + ca[4]) == (ca[2] + ca[6])) { continue; }
                if ((ca[0] + ca[3] + ca[4]) == (ca[1] + ca[2] + ca[5])) { continue; }
                if ((ca[0] + ca[3] + ca[4]) == (ca[1] + ca[2] + ca[6])) { continue; }
                if ((ca[1] + ca[3] + ca[4]) == (ca[0] + ca[2] + ca[5])) { continue; }
                if ((ca[1] + ca[3] + ca[4]) == (ca[0] + ca[2] + ca[6])) { continue; }
                if ((ca[1] + ca[3] + ca[4]) == (ca[0] + ca[5] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[4]) == (ca[0] + ca[1] + ca[5])) { continue; }
                if ((ca[2] + ca[3] + ca[4]) == (ca[0] + ca[1] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[4]) == (ca[0] + ca[5] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[4]) == (ca[1] + ca[5] + ca[6])) { continue; }
                if ((ca[1] + ca[5]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[5]) == (ca[0] + ca[6])) { continue; }
                if ((ca[2] + ca[5]) == (ca[1] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[5]) == (ca[0] + ca[3] + ca[6])) { continue; }
                if ((ca[1] + ca[2] + ca[5]) == (ca[0] + ca[4] + ca[6])) { continue; }
                if ((ca[3] + ca[5]) == (ca[0] + ca[6])) { continue; }
                if ((ca[3] + ca[5]) == (ca[1] + ca[6])) { continue; }
                if ((ca[3] + ca[5]) == (ca[2] + ca[6])) { continue; }
                if ((ca[0] + ca[3] + ca[5]) == (ca[1] + ca[2] + ca[6])) { continue; }
                if ((ca[1] + ca[3] + ca[5]) == (ca[0] + ca[2] + ca[6])) { continue; }
                if ((ca[1] + ca[3] + ca[5]) == (ca[0] + ca[4] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[5]) == (ca[0] + ca[1] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[5]) == (ca[0] + ca[4] + ca[6])) { continue; }
                if ((ca[2] + ca[3] + ca[5]) == (ca[1] + ca[4] + ca[6])) { continue; }
                if ((ca[4] + ca[5]) == (ca[0] + ca[6])) { continue; }
                if ((ca[4] + ca[5]) == (ca[1] + ca[6])) { continue; }
                if ((ca[4] + ca[5]) == (ca[2] + ca[6])) { continue; }
                if ((ca[4] + ca[5]) == (ca[3] + ca[6])) { continue; }
                if ((ca[0] + ca[4] + ca[5]) == (ca[1] + ca[2] + ca[6])) { continue; }
                if ((ca[0] + ca[4] + ca[5]) == (ca[1] + ca[3] + ca[6])) { continue; }
                if ((ca[0] + ca[4] + ca[5]) == (ca[2] + ca[3] + ca[6])) { continue; }
                if ((ca[1] + ca[4] + ca[5]) == (ca[0] + ca[2] + ca[6])) { continue; }
                if ((ca[1] + ca[4] + ca[5]) == (ca[0] + ca[3] + ca[6])) { continue; }
                if ((ca[1] + ca[4] + ca[5]) == (ca[2] + ca[3] + ca[6])) { continue; }
                if ((ca[2] + ca[4] + ca[5]) == (ca[0] + ca[1] + ca[6])) { continue; }
                if ((ca[2] + ca[4] + ca[5]) == (ca[0] + ca[3] + ca[6])) { continue; }
                if ((ca[2] + ca[4] + ca[5]) == (ca[1] + ca[3] + ca[6])) { continue; }
                if ((ca[3] + ca[4] + ca[5]) == (ca[0] + ca[1] + ca[6])) { continue; }
                if ((ca[3] + ca[4] + ca[5]) == (ca[0] + ca[2] + ca[6])) { continue; }
                if ((ca[3] + ca[4] + ca[5]) == (ca[1] + ca[2] + ca[6])) { continue; }

                Console.WriteLine(combination.ToConcatString(" "));

                break;
            }
        }
    }
}
