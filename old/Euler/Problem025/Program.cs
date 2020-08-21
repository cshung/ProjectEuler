using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Problem025
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = new BigInteger(1), b = new BigInteger(1), c;
            int d = 3;
            while (true)
            {
                c = BigInteger.Add(a, b);
                string s = c.ToString();
                if (s.Length == 1000)
                {
                    Console.WriteLine(d);
                    break;
                }
                a = b;
                b = c;
                d++;
            }
        }
    }
}
