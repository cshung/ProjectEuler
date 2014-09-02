namespace Euler
{
    using System;

    internal static partial class Program
    {
        public static void Problem006()
        {
            int n = 100;
            int sumOfSquares = n * (n + 1) * (2 * n + 1) / 6;
            int squareOfSum = n * n * (n + 1) * (n + 1) / 4;
            Console.WriteLine(squareOfSum - sumOfSquares);
        }
    }
}
