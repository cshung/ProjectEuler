using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Common;

namespace Problem094
{
    class Program
    {
        static void Main(string[] args)
        {
            // WLOG, let a = b, c = a + d where d is +/- 1
            // s = (a + b + c)/2 = (3a + d)/2
            // s(s - a)(s - b)(s - c) is a perfect square
            // s(s - a)(s - a)(s - (a + d)) is a perfect square
            // s(s - (a + d)) is a perfect square
            // (3a + d)/2((3a + d)/2 - (a + d)) is a perfect square
            // 1/4(3a + d)((3a + d)-2a-2d) is a perfect square
            // 1/4(3a + d)(a - d) is a perfect square
            // (3a + d)(a - d) is an even perfect square
            // 3a^2 - 2ad - d^2 is an even perfect square
            // The rest is just solving the Diophantine equation!
            // Equation 1: 3a^2 - 4e^2 - 2a - 1 = 0 (when d = 1)
            // Equation 2: 3a^2 - 4e^2 + 2a - 1 = 0 (when d = -1)

            IEnumerable<BigInteger> solutionSet1WithDuplicates = new List<BigInteger>();
            solutionSet1WithDuplicates = solutionSet1WithDuplicates.Concat(GenerateSolution(1, 0, 7, 8, -2, 6, 7, -2));
            solutionSet1WithDuplicates = solutionSet1WithDuplicates.Concat(GenerateSolution(-1, 1, 7, 8, -2, 6, 7, -2));

            HashSet<BigInteger> solutionSet1 = new HashSet<BigInteger>(solutionSet1WithDuplicates);
            // 1 is known bad
            solutionSet1.Remove(1);

            IEnumerable<BigInteger> solutionSet2WithDuplicates = new List<BigInteger>();
            solutionSet2WithDuplicates = solutionSet2WithDuplicates.Concat(GenerateSolution(-1, 0, 7, 8, 2, 6, 7, 2));
            solutionSet2WithDuplicates = solutionSet2WithDuplicates.Concat(GenerateSolution(1, -1, 7, 8, 2, 6, 7, 2));

            HashSet<BigInteger> solutionSet2 = new HashSet<BigInteger>(solutionSet2WithDuplicates);
            // 1 is known bad
            solutionSet2.Remove(1);

            Console.WriteLine(solutionSet1.ToConcatString(","));
            Console.WriteLine(solutionSet2.ToConcatString(","));

            BigInteger sum = 0;

            foreach (BigInteger solution in solutionSet1)
            {
                BigInteger s = solution * 3 + 1;
                sum = sum + s;
                //s = BigInteger.Divide(s, 2);
                //BigInteger root;
                //if (!EulerUtil.IsPerfectSquare(s * (s - solution - 1), out root))
                //{
                //    Console.WriteLine("Ooops!");
                //}
            }

            foreach (BigInteger solution in solutionSet2)
            {
                BigInteger s = solution * 3 - 1;
                sum = sum + s;
                //s = BigInteger.Divide(s, 2);
                //BigInteger root;
                //if (!EulerUtil.IsPerfectSquare(s * (s - solution + 1), out root))
                //{
                //    Console.WriteLine("Ooops!");
                //}
            }

            // 7220496864 is known to be wrong
            Console.WriteLine(sum);
        }

        private static IEnumerable<BigInteger> GenerateSolution(BigInteger x1, BigInteger y1, int P, int Q, int K, int R, int S, int L)
        {
            while (true)
            {
                BigInteger nx = P * x1 + Q * y1 + K;
                BigInteger ny = R * x1 + S * y1 + L;

                x1 = nx;
                y1 = ny;


                BigInteger toCheck = x1;
                if (toCheck < 0)
                {
                    toCheck = toCheck * -1;
                }
                if (toCheck * 3 > BigInteger.Parse("1000000000"))
                {
                    break;
                }
                if (x1 > 0)
                {
                    yield return x1;
                }
            }
        }
    }
}
