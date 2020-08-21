namespace Problem100
{
    using System;
    using System.Numerics;

    class Program
    {
        // The problem ask for 
        // 2b(b-1) = (r+b)(r+b-1)
        // With some simplification, we get to
        // 2b^2 - 2b = r^2 + 2rb + b^2 - r - b
        // b^2 - 2rb - r^2 - b + r = 0
        // Plugging this into the Quadratic Diophantine equation solver yield this result
        static void Main(string[] args)
        {
            BigInteger x = 1;
            BigInteger y = 0;
            int P = 5;
            int Q = 2;
            int K = -2;
            int R = 2;
            int S = 1;
            int L = -1;
            while (true)
            {
                BigInteger nx = P * x + Q * y + K;
                BigInteger ny = R * x + S * y + L;
                x = nx;
                y = ny;
                BigInteger sum = x + y;
                if ((x + y).ToString().Length > 12)
                {
                    break;
                }
            }

            Console.WriteLine(x);
        }
    }
}
