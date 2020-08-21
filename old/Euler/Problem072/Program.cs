using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem072
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = Enumerable.Range(1, 1000000).Select(t => EulerUtil.EulerPhi(t)).Aggregate((x, y) => x + y);
            Console.WriteLine(sum);
        }
    }
}
