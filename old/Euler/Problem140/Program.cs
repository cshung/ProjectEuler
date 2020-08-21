namespace Problem140
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Numerics;

    class Program
    {
        static void Main(string[] args)
        {
            var seeds = new List<Tuple<BigInteger, BigInteger>>();
            seeds.Add(Tuple.Create(BigInteger.Parse("0"), BigInteger.Parse("-1")));
            seeds.Add(Tuple.Create(BigInteger.Parse("0"), BigInteger.Parse("1")));
            seeds.Add(Tuple.Create(BigInteger.Parse("-3"), BigInteger.Parse("-2")));
            seeds.Add(Tuple.Create(BigInteger.Parse("-3"), BigInteger.Parse("2")));

            seeds.Add(Tuple.Create(BigInteger.Parse("-4"), BigInteger.Parse("-5")));
            seeds.Add(Tuple.Create(BigInteger.Parse("-4"), BigInteger.Parse("5")));

            seeds.Add(Tuple.Create(BigInteger.Parse("2"), BigInteger.Parse("-7")));
            seeds.Add(Tuple.Create(BigInteger.Parse("2"), BigInteger.Parse("7")));
            var solutions = new HashSet<BigInteger>();
            foreach (var seed in seeds)
            {
                foreach (var solution in Solutions(seed))
                {
                    if (solution.Item1 > 0) {
                        solutions.Add(solution.Item1);
                    }
                }
            }
            Console.WriteLine(solutions.OrderBy(s => s).Take(30).Aggregate((p, q) => p + q));
        }

        private static IEnumerable<Tuple<BigInteger, BigInteger>> Solutions(Tuple<BigInteger, BigInteger> seed)
        {
            BigInteger x = seed.Item1;
            BigInteger y = seed.Item2;

            BigInteger p = BigInteger.Parse("-9");
            BigInteger q = BigInteger.Parse("-4");
            BigInteger k = BigInteger.Parse("-14");
            BigInteger r = BigInteger.Parse("-20");
            BigInteger s = BigInteger.Parse("-9");
            BigInteger l = BigInteger.Parse("-28");

            for (int i = 0; i < 50; i++)
            {
                yield return Tuple.Create(x, y);
                BigInteger nx = p * x + q * y + k;
                BigInteger ny = r * x + s * y + l;
                x = nx;
                y = ny;
            }
        }
    }
}
