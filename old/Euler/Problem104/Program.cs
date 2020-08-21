using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using Common;

namespace Problem104
{
    class Program
    {
        static void Main(string[] args)
        {
            int index = 0;
            Stopwatch sw = Stopwatch.StartNew();
            foreach (var smallFib in SmallFib())
            {
                index++;
                string smallFibString = smallFib.ToString();
                if (Pandigital(smallFibString))
                {
                    BigInteger value = FastFib(index);
                    string bigFibString = value.ToString();
                    string firstNine = bigFibString.Substring(0, 9);
                    if (Pandigital(firstNine))
                    {
                        Console.WriteLine(index);
                        Console.WriteLine(bigFibString.Length);
                        Console.WriteLine(sw.Elapsed);
                        Console.ReadKey();
                        break;
                    }
                }
            }
        }

        class BigMatrix
        {
            public BigInteger a;
            public BigInteger b;
            public BigInteger c;
            public BigInteger d;

            public static BigMatrix Multiply(BigMatrix left, BigMatrix right)
            {
                return new BigMatrix
                {
                    a = left.a * right.a + left.b * right.c,
                    b = left.a * right.b + left.b * right.d,
                    c = left.c * right.a + left.d * right.c,
                    d = left.c * right.b + left.d * right.d,
                };
            }
        }

        private static Dictionary<int, BigMatrix> cache = new Dictionary<int, BigMatrix>();

        private static BigInteger FastFib(int index)
        {
            BigMatrix source = new BigMatrix
            {
                a = 1,
                b = 1,
                c = 1,
                d = 0
            };
            BigMatrix one = new BigMatrix
            {
                a = 1,
                b = 0,
                c = 0,
                d = 1
            };
            BigMatrix product = CachedPower<BigMatrix>(source, index, BigMatrix.Multiply, one, cache);
            return product.b;
        }

        public static T CachedPower<T>(T value, int power, Func<T, T, T> multiply, T one, Dictionary<int, T> cache)
        {
            T result;
            if (!cache.TryGetValue(power, out result))
            {
                List<int> availableCacheValues = cache.Keys.Where(t => t < power).ToList();
                if (availableCacheValues.Count > 0)
                {
                    int maxLess = availableCacheValues.Max();
                    int remainingPower = power - maxLess;
                    T remainingValue = CachedPower(value, remainingPower, multiply, one, cache);
                    result = multiply(cache[maxLess], remainingValue);
                }
                else
                {
                    if (power == 0)
                    {
                        result = one;
                    }
                    else if (power == 1)
                    {
                        result = value;
                    }
                    else
                    {
                        T halfPower = CachedPower(value, power / 2, multiply, one, cache);
                        result = multiply(halfPower, halfPower);
                        if (power % 2 == 1)
                        {
                            result = multiply(result, value);
                        }
                    }
                }
                cache.Add(power, result);
            }
            return result;
        }

        public static IEnumerable<int> SmallFib()
        {
            yield return 1;
            yield return 1;
            int a = 1;
            int b = 1;
            while (true)
            {
                int sum = (a + b) % 1000000000;
                yield return sum;
                a = b;
                b = sum;
            }
        }

        private static bool Pandigital(string nine)
        {
            return nine.Contains('1') && nine.Contains('2') && nine.Contains('3') && nine.Contains('4') && nine.Contains('5') && nine.Contains('6') && nine.Contains('7') && nine.Contains('8') && nine.Contains('9');
        }
    }
}
