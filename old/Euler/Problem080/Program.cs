using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Common;

namespace Problem080
{
    class Program
    {
        static void Main(string[] args)
        {
            var squares = new HashSet<int>(Enumerable.Range(1, 10).Select(t => t * t));
            var nonSquares = new HashSet<int>(Enumerable.Range(1, 100).Except(squares));
            int sum = 0;
            foreach (var nonSquare in nonSquares)
            {
                int digit = 100;
                string zeroes = new string('0', digit * 2);
                string number = nonSquare + zeroes;

                BigInteger square = BigInteger.Parse(number);
                var squareRoot = EulerUtil.SquareRoot(square);

                string result = squareRoot.Item2.ToString();
                sum += result.Take(100).Select(c => c - '0').Aggregate((x, y) => x + y);
            }

            Console.WriteLine(sum);
        }
    }
}
