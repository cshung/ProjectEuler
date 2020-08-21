using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;

namespace Problem068
{
    class Program
    {
        static void Main(string[] args)
        {
            long max = -1;
            int i = 0;
            foreach (var perm in EulerUtil.Permutations(Enumerable.Range(1, 10).ToList()))
            {
                if (++i % 10000 == 0)
                {
                    Console.Write('.');
                }

                int[] p = perm.ToArray();
                int sum = p[0] + p[1] + p[2];
                if (p[1] != 10 && p[2] != 10 && p[4] != 10 && p[6] != 10 && p[8] != 10)
                {
                    if (p[3] + p[2] + p[4] == sum)
                    {
                        if (p[5] + p[4] + p[6] == sum)
                        {
                            if (p[7] + p[6] + p[8] == sum)
                            {
                                if (p[9] + p[8] + p[1] == sum)
                                {
                                    int min = new int[] { p[0], p[3], p[5], p[7], p[9] }.Min();
                                    string good = CreateString(p, min);
                                    long goodNum = long.Parse(good);
                                    if (goodNum > max)
                                    {
                                        max = goodNum;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            Console.WriteLine(max);
        }

        private static string CreateString(int[] p, int min)
        {
            string good;
            if (min == p[0])
            {
                good = "" + p[0] + p[1] + p[2] + p[3] + p[2] + p[4] + p[5] + p[4] + p[6] + p[7] + p[6] + p[8] + p[9] + p[8] + p[1];
            }
            else if (min == p[3])
            {
                good = "" + p[3] + p[2] + p[4] + p[5] + p[4] + p[6] + p[7] + p[6] + p[8] + p[9] + p[8] + p[1] + p[0] + p[1] + p[2];
            }
            else if (min == p[5])
            {
                good = "" + p[5] + p[4] + p[6] + p[7] + p[6] + p[8] + p[9] + p[8] + p[1] + p[0] + p[1] + p[2] + p[3] + p[2] + p[4];
            }
            else if (min == p[7])
            {
                good = "" + p[7] + p[6] + p[8] + p[9] + p[8] + p[1] + p[0] + p[1] + p[2] + p[3] + p[2] + p[4] + p[5] + p[4] + p[6];
            }
            else if (min == p[9])
            {
                good = "" + p[9] + p[8] + p[1] + p[0] + p[1] + p[2] + p[3] + p[2] + p[4] + p[5] + p[4] + p[6] + p[7] + p[6] + p[8];
            }
            else
            {
                good = null;
                Debug.Fail("Shouldn't reach");
            }
            return good;
        }
    }
}