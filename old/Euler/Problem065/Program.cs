using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Numerics;

namespace Problem065
{
    class Program
    {
        // http://en.wikipedia.org/wiki/Continued_fraction
        static void Main(string[] args)
        {
            BigRational first = new BigRational(new BigInteger(2), BigInteger.One);
            BigRational second = new BigRational(new BigInteger(3), BigInteger.One);

            int k = 1;
            int loop = 1;
            int value;

            int convergent = 2;
            while (true)
            {
                if (loop == 1)
                {
                    value = 2 * k;
                    k++;
                }
                else
                {
                    value = 1;
                }
                loop = (loop + 1) % 3;

                BigRational newValue;

                newValue = new BigRational(value * second.Numerator + first.Numerator, value * second.Denominator + first.Denominator);
                first = second;
                second = newValue;
                convergent++;
                Console.WriteLine(convergent + ": " + second.Numerator + " / " + second.Denominator);
                if (convergent == 100)
                {
                    break;
                }
            }
            Console.WriteLine(second.Numerator.ToString().Select(c => c - '0').Aggregate((x, y) => x + y));

        }
    }
}
