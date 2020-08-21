namespace Problem091
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int d = 51;
            for (int i = 1; i < d * d; i++)
            {
                for (int j = i + 1; j < d * d; j++)
                {
                    int ax = i % d;
                    int ay = i / d;
                    int bx = j % d;
                    int by = j / d;

                    // OA . OB = (ax, ay) . (bx, by) = ax * bx + ay * by
                    if (ax * bx + ay * by == 0)
                    {
                        count++;
                        //Console.WriteLine("{4}: (0, 0) - ({0},{1}) - ({2},{3})", ax, ay, bx, by, count);
                    }

                    // AO . AB = (-ax, -ay) . (bx - ax, by - ay) = (ax - bx)ax + (ay - by)ay
                    if ((ax - bx) * ax + (ay - by) * ay == 0)
                    {
                        count++;
                        //Console.WriteLine("{4}: (0, 0) - ({0},{1}) - ({2},{3})", ax, ay, bx, by, count);
                    }

                    // BO . BA = (-bx, -by) . (ax - bx, ay - by) = (bx - ax)bx + (by - ay)by
                    if ((bx - ax) * bx + (by - ay) * by == 0)
                    {
                        count++;
                        //Console.WriteLine("{4}: (0, 0) - ({0},{1}) - ({2},{3})", ax, ay, bx, by, count);
                    }
                }
            }
            Console.WriteLine(count);
        }
    }
}
