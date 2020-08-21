namespace Problem033
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            var result = FindFractions();
            Console.WriteLine(Simplify(result.Aggregate((x, y) => Tuple.Create(x.Item1 * y.Item1, x.Item2 * y.Item2))).Item2);
        }

        static IEnumerable<Tuple<int, int>> FindFractions() {
            for (int numerator = 10; numerator <= 99; numerator++)
            {
                for (int denominator = numerator + 1; denominator <= 99; denominator++)
                {
                    int a = numerator / 10;
                    int b = numerator % 10;
                    int c = denominator / 10;
                    int d = denominator % 10;

                    Tuple<int, int> fraction = Tuple.Create(numerator, denominator);
                    Tuple<int, int> simplified = Simplify(fraction);

                    if (a == c)
                    {
                        if (d != 0)
                        {
                            Tuple<int, int> naive = Tuple.Create(b, d);
                            Tuple<int, int> naiveSimplified = Simplify(naive);
                            if (naiveSimplified.Item1 == simplified.Item1 && naiveSimplified.Item2 == simplified.Item2)
                            {
                                yield return naiveSimplified;
                            }
                        }
                    }
                    if (a == d)
                    {
                        if (c != 0)
                        {
                            Tuple<int, int> naive = Tuple.Create(b, c);
                            Tuple<int, int> naiveSimplified = Simplify(naive);
                            if (naiveSimplified.Item1 == simplified.Item1 && naiveSimplified.Item2 == simplified.Item2)
                            {
                                yield return naiveSimplified;
                            }
                        }
                    }
                    if (b == c)
                    {
                        if (d != 0)
                        {
                            Tuple<int, int> naive = Tuple.Create(a, d);
                            Tuple<int, int> naiveSimplified = Simplify(naive);
                            if (naiveSimplified.Item1 == simplified.Item1 && naiveSimplified.Item2 == simplified.Item2)
                            {
                                yield return naiveSimplified;
                            }
                        }
                    } 
                }
            }
        }

        private static Tuple<int, int> Simplify(Tuple<int, int> fraction)
        {
            int commonFactor = EulerUtil.CommonFactor(fraction.Item1, fraction.Item2);
            return Tuple.Create(fraction.Item1 / commonFactor, fraction.Item2 / commonFactor);
        }
    }
}
