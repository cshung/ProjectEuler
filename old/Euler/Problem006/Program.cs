using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem006
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 100;
            int sumOfSquares = n * (n + 1) * (2 * n + 1) / 6;
            int squareOfSum = n * n * (n + 1) * (n + 1) / 4;
            Console.WriteLine("{0} - {1} = {2}", squareOfSum, sumOfSquares, squareOfSum - sumOfSquares);
        }
    }
}
