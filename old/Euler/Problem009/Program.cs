namespace Problem009
{
    using System;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            foreach (var triple in EulerUtil.GetPrimitivePythTriples(1000, null))
            {
                int tripleSum = triple.Item1 + triple.Item2 + triple.Item3;
                if (1000 % tripleSum == 0)
                {
                    int scale = 1000 / tripleSum;
                    Console.WriteLine(scale * scale * scale * triple.Item1 * triple.Item2 * triple.Item3);
                    return;
                }
            }
        }
    }
}
