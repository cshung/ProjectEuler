namespace Euler
{
    using System;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem003()
        {
            Console.WriteLine(PollardFactor(600851475143).Max());
        }
    }
}
