namespace Euler
{
    using System;

    internal static partial class Program
    {
        public static void Problem002()
        {
            int fib, fibLast, fibLastLast;
            fibLast = 1;
            fibLastLast = 1;
            int sum = 0;
            do
            {
                fib = fibLast + fibLastLast;
                if (fib % 2 == 0)
                {
                    sum += fib;
                }

                fibLastLast = fibLast;
                fibLast = fib;
            } while (fib < 4000000);
            Console.WriteLine(sum);
        }
    }
}
