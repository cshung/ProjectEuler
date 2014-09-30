namespace Euler
{
    using System;

    internal static partial class Program
    {
        public static void Problem028()
        {
            long sum = 1;
            for (int i = 1; i < (1001 + 1) / 2; i++)
            {
                long end = (2 * i + 1) * (2 * i + 1);
                long diff = 2 * i;
                long start = end - 3 * diff;
                sum += (start + end) * 4 / 2;
            }
            Console.WriteLine(sum);
        }
    }
}
