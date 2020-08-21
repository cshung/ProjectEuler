namespace Problem070
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            double min = double.PositiveInfinity;
            int minIndex = -1;
            for (int i = 2; i < 10000000; i++)
            {
                if (i % 100000 == 0)
                {
                    Console.Write('.');
                }

                long euler = EulerUtil.EulerPhi(i);
                string eulerSorted = euler.ToString().OrderBy(c => c).ToConcatString();
                string iSorted = i.ToString().OrderBy(c => c).ToConcatString();
                if (string.Equals(eulerSorted, iSorted))
                {
                    double fraction = (double)i / euler;
                    if (fraction < min)
                    {
                        min = fraction;
                        minIndex = i;
                    }
                }
            }
            Console.WriteLine(minIndex);
        }
    }
}
