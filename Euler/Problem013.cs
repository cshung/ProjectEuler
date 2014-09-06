namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem013()
        {
            var text = ReadResourceAsString("Euler.Problem013.txt").Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var data = text.Select(t => BigInteger.Parse(t));
            Console.WriteLine(data.Aggregate((a, b) => BigInteger.Add(a, b)).ToString().Substring(0, 10));
        }
    }
}
