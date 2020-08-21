namespace Problem003
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(EulerUtil.PollardFactor(600851475143).Max());
        }
    }
}
