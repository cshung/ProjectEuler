namespace Problem053
{
    using System;
    using System.Numerics;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            BigInteger[,] pascalTriangle = EulerUtil.BuildPascalTriangle(100);
            for (int n = 2; n <= 100; n++)
            {
                for (int r = 0; r <= n; r++)
                {
                    string ncr = EulerUtil.BinomialCoefficient(n, r, pascalTriangle).ToString();                    
                    if (ncr.Length > 7)
                    {
                        count++;
                    }
                    else 
                    {
                        int ncrValue = int.Parse(ncr);
                        if (ncrValue > 1000000)
                        {
                            count++;
                        }
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}
