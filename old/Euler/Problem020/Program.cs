using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Problem020
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger prod = new BigInteger(1);
            for (int i = 2; i <= 100; i++)
            {
                prod = BigInteger.Multiply(prod, new BigInteger(i));
            }
            Console.WriteLine(prod.ToString().Select(i => i - '0').Aggregate((x, y) => x + y));
        }
    }
}
