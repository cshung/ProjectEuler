using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Numerics;

namespace Problem045
{
    class Program
    {
        static void Main()
        {
            long n = 1;
            while (true)
            {
                if (n % 10000 == 0)
                {
                    Console.Write('.');
                }
                long hexagonalNumber = n * (2 * n - 1);
                if (EulerUtil.IsPentagonNumber(hexagonalNumber))
                {
                    if (IsTriangularNumber(hexagonalNumber))
                    {
                        Console.WriteLine(hexagonalNumber);
                    }
                }
                n++;
            }
        }

        private static bool IsTriangularNumber(long candidate)
        {
            BigInteger bigCandidate = new BigInteger(candidate);
            BigInteger root;
            if (EulerUtil.IsPerfectSquare(1 + 4 * 2 * bigCandidate, out root))
            {
                return true;
            }

            return false;
        }
    }
}
