namespace Problem010
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EulerUtil.Primes(2000000).Select(t => (long)t).Aggregate((x, y) => x + y));
        }
    }
}
