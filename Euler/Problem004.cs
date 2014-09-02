namespace Euler
{
    using System;

    internal static partial class Program
    {
        public static void Problem004()
        {
            int max = 0;
            for (int i = 999; i >= 100; i--)
            {
                for (int j = 999; j >= 100; j--)
                {
                    int candidate = i * j;
                    if (candidate == Reverse(candidate)) 
                    {
                        if (candidate > max)
                        {
                            max = candidate;
                        }
                    }
                }
            }

            Console.WriteLine(max);
        }
    }
}
