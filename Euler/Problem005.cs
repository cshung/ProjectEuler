namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem005()
        {
            IEnumerable<long> bases = Enumerable.Range(1, 20).Select(x => (long)x);
            Console.WriteLine(bases.Aggregate(CommonMultiple));
        }
    }
}
