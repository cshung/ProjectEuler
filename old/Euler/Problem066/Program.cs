using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;
using System.Numerics;

namespace Problem066
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, BigInteger> solutions = new Dictionary<int, BigInteger>();
            foreach (int i in NonSquares(1000))
            {
                var continuedFraction = EulerUtil.ContinuedFraction(new QuadraticIrrational { A = 0, B = 1, C = i, D = 1 });
                int periodLength = continuedFraction.Item2.Count;
                List<int> reversedConvergentCandidates;
                List<int> reversedPeriod = continuedFraction.Item2;
                reversedPeriod.Reverse();
                if (periodLength % 2 == 0)
                {
                    reversedConvergentCandidates = reversedPeriod.Skip(1).Concat(new List<int> { continuedFraction.Item1 }).ToList();
                }
                else
                {
                    reversedConvergentCandidates = reversedPeriod.Skip(1).Concat(reversedPeriod).Concat(new List<int> { continuedFraction.Item1 }).ToList();
                }

                BigInteger n = 0;
                BigInteger d = 1;

                foreach (int currentValue in reversedConvergentCandidates)
                {
                    if (n != 0)
                    {
                        // a_x + 1/current
                        BigInteger newD = n;
                        BigInteger newN = currentValue * newD + d;
                        d = newD;
                        n = newN;
                    }
                    else
                    {
                        // This is a special case, since 1/0 can't be done
                        n = currentValue;
                        d = 1;
                    }
                }
                solutions.Add(i, n);
                //Console.WriteLine("x^2 - {0}y^2 = 1 has solution: x = {1}, y = {2}", i, n, d);
                //Debug.Assert(n * n - i * d * d == 1);
                //Console.ReadKey(); Console.WriteLine();
            }
            BigInteger max = solutions.Select(t => t.Value).Max();
            Console.WriteLine(solutions.Where(t => t.Value == max).Select(t => t.Key).Single());
        }


        private static IEnumerable<int> NonSquares(int size)
        {
            bool isSquare = true;
            int nextSquareSize = 0;
            int nextSquareCount = 0;
            for (int i = 1; i < size; i++)
            {
                if (!isSquare)
                {
                    yield return i;
                    nextSquareCount--;
                    if (nextSquareCount == 0)
                    {
                        isSquare = true;
                    }
                }
                else
                {
                    nextSquareSize += 2;
                    nextSquareCount = nextSquareSize;
                    isSquare = false;
                }
            }
        }
    }
}
