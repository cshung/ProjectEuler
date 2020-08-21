using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem073
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            for (int d = 1; d <= 12000; d++)
            {
                if (d % 120 == 0)
                {
                    Console.Write('.');
                }
                for (int n = 1; n < d; n++)
                {
                    if (EulerUtil.CommonFactor(n, d) == 1)
                    {
                        if (3 * n > d)
                        {
                            if (2 * n < d)
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(count);
        }
    }
}
