namespace Problem055
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Numerics;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            for (int j = 1; j <= 10000; j++)
            {
                int counter = 0;
                bool isPan;
                BigInteger i = new BigInteger(j);
                do
                {
                    var bigString = i.ToString();
                    var revString = bigString.Reverse().ToArray();
                    isPan = EulerUtil.StackLists(bigString, revString).Select(t => t.Item1 == t.Item2).Aggregate((x, y) => x && y);
                    if (counter != 0 && isPan)
                    {
                        break;
                    }
                    else
                    {
                        i = BigInteger.Parse(new string(revString)) + i;
                    }
                    //Console.WriteLine(i);
                } while (++counter < 50);
                if (counter == 50 && !isPan)
                {
                    count++;
                }
                //Console.WriteLine(j + "\t" + (counter == 50));
                if (j % 1000 == 0)
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine(count);
        }
    }
}
