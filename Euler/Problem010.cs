namespace Euler
{
    using System;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem010()
        {
            Console.WriteLine(Primes(2000000).Select(t => (long)t).Sum());
        }
    }
}
