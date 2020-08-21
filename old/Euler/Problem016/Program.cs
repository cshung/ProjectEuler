using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Problem016
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger p = BigInteger.Pow(new BigInteger(2), 1000);
            Console.WriteLine(p.ToString().Select(x => x - '0').Aggregate((x, y) => (x + y)));
        }
    }
}
