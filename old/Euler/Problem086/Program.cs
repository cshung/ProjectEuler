namespace Problem086
{
    using System;
    using System.Collections.Generic;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int max = 2000;
            int min = 1;
            while (max != min)
            {
                int mid = (min + max)/2;
                int cubeCount = CubeCount(mid);
                Console.WriteLine("CubeCount({0}) = {1}", mid, cubeCount);
                if (cubeCount > 1000000)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }
        }

        private static int CubeCount(int M)
        {
            var primitivePythTriples = EulerUtil.GetPrimitivePythTriples(null, M);
            var results = new HashSet<Tuple<int, int, int>>();
            foreach (var primitivePythTriple in primitivePythTriples)
            {
                int a = primitivePythTriple.Item1;
                int b = primitivePythTriple.Item2;
                int primitiveShortLeg = Math.Min(a, b);
                int primitiveLongLeg = Math.Max(a, b);
                // Now, I am interested in all the multiples of the primitivePythTriple as long as the shortLeg stay within M.
                int m = M / primitiveShortLeg;
                for (int i = 1; i <= m; i++)
                {
                    int shortLeg = primitiveShortLeg * i;
                    int longLeg = primitiveLongLeg * i;
                    //Console.WriteLine(Tuple.Create(shortLeg, longLeg));
                    // Now consider how many ways we can write as a cube.
                    // Put it in the diagram as the problem did, we need to decompose one edge into a sum, such that
                    //
                    // b + c = oneLeg, 1 <= b, c <= min(M, anotherLeg)
                    //
                    // If the long leg is less than M, then the decomposition is easy because the upper bound will never be reached
                    // In that case count += shortLeg - 1 (i.e. (1 + (shortLeg - 1)) ... (shortLeg - 1) + 1)
                    // Otherwise decomposing the short leg yield no result because long leg is too long.
                    //
                    // Now take the short leg and decompose the long one, now it is possible that we reach the upper bound.
                    // Note that another leg is the short one, so another leg is always less than M.
                    // Further, if (longLeg - shortLeg < shortLeg), then (shortLeg + (longLeg - shortLeg)) is feasible.
                    // this is the one with maximum first part, it goes all the way to ((longLeg - shortLeg) + shortLeg), therefore
                    // There are longLeg - shortLeg - shortLeg + 1 decompositions
                    // Otherwise, there is no decomposition possible

                    if (longLeg <= M)
                    {
                        // Debug
                        for (int j = 1; j <= shortLeg - 1; j++)
                        {
                            //Console.WriteLine("{0}^2 + ({1} + {2})^2 is a square", longLeg, j, shortLeg - j);
                            int[] result = new int[] { longLeg, j, shortLeg - j };
                            Array.Sort(result);
                            results.Add(Tuple.Create(result[0], result[1], result[2]));
                        }
                    }
                    if (longLeg - shortLeg < shortLeg)
                    {
                        for (int j = shortLeg; j >= longLeg - shortLeg; j--)
                        {
                            //Console.WriteLine("{0}^2 + ({1} + {2})^2 is a square", shortLeg, j, longLeg - j);
                            int[] result = new int[] { shortLeg, j, longLeg - j };
                            Array.Sort(result);
                            results.Add(Tuple.Create(result[0], result[1], result[2]));
                        }
                    }
                }
            }

            return results.Count;
        }
    }
}
