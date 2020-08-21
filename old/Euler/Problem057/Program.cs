namespace Problem057
{
    using System.Linq;
    using System.Numerics;
    using Common;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            BigRational b = new BigRational(new BigInteger(3), new BigInteger(2));
            for (int i = 2; i <= 1000; i++)
            {
                BigRational bNew = BigRational.One + (BigRational.One / (BigRational.One + b));
                b = bNew;
                if (bNew.Numerator.ToString().Length > bNew.Denominator.ToString().Length)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
