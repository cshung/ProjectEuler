namespace Problem005
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<long> bases = Enumerable.Range(1, 20).Select(x => (long)x);
            Console.WriteLine(bases.Aggregate(EulerUtil.CommonMultiple));
        }
    }
}
