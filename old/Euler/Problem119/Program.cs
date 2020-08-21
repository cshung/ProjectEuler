namespace Problem119
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            // To make things easier - start with three digits
            // We know in two digits, there is only one solution (by a really simple brute force) 81 = (8+1)^2

            int n = 3;
            int count = 1;
            while (true)
            {
                // The algorithm trying to filter out values that are not powers
                //
                // For a value to be an n-digit number, it means
                // 10^{n-1} <= v < 10^n
                // 2 <= digitSum < 9n [We know digitSum could be 1 - but that can never be an answer, just ignore all those cases]
                //
                // These bounds allow us to compute the bounds for the powers as follow:
                // digitSum^k = v
                // k log_10(digitSum) = log_10(v)
                // k = log_10(v)/log_10(digitSum)
                // Therefore kMin = min(log_10(v))/max(log_10(digitSum))
                //                = (n-1)/log_10(9n)
                // 
                // Similarly kMax = max(log_10(v))/min(log_10(digitSum))
                //                = n/log_10(2)

                int kMin = (int)Math.Ceiling((n - 1) / Math.Log10(9 * n));
                int kMax = (int)Math.Floor(n / Math.Log10(2));

                SortedDictionary<long, HashSet<Tuple<int, int>>> powerMap = new SortedDictionary<long, HashSet<Tuple<int, int>>>();
                for (int k = kMin; k <= kMax; k++)
                {
                    // We know the power is k, and we want to find out the range of digitSum we have to test
                    // We already know 2 < digitSum <= 9n, we wanted to make this range more narrow by
                    // knowing k.
                    //
                    // 10^{n-1} <= v^k         < 10^n
                    //    n - 1 <= k log_10(v) < n
                    //
                    // For lower bound, we have
                    //
                    //      (n - 1)/k <= log_10(v)
                    // 10^{(n - 1)/k} <=  v
                    //
                    // For upper bound, we have
                    //
                    // log_10(v) < n/k
                    //         v < 10^{n/k}
                    //

                    // We are careful with floating point here.
                    // Suppose the value is actually an integer, the ceiling (or floor) could miss the value
                    // But that doesn't matter since that value is useless
                    // (either it hits the upper bound => not needed) or 
                    // it hits the lower bound => it is 10^{n-1} which can't be an answer.
                    int startingValue = (int)Math.Ceiling(Math.Pow(10, (n - 1) * 1.0 / k));
                    int endingValue = (int)Math.Floor(Math.Pow(10, n * 1.0 / k));

                    int maxEndingValue = 9 * n;
                    startingValue = startingValue > 2 ? startingValue : 2;
                    endingValue = endingValue < maxEndingValue ? endingValue : maxEndingValue;
                    for (int v = startingValue; v <= endingValue; v++)
                    {
                        Tuple<int, int> power = Tuple.Create(v, k);
                        long vk = (long)Math.Pow(v, k);
                        HashSet<Tuple<int, int>> powers;
                        if (!powerMap.TryGetValue(vk, out powers))
                        {
                            powers = new HashSet<Tuple<int, int>>();
                            powerMap.Add(vk, powers);
                        }
                        powers.Add(power);
                    }
                }

                foreach (var check in powerMap)
                {
                    int digitSum = check.Key.ToString().Select(c => c - '0').Sum();
                    foreach (var power in check.Value)
                    {
                        if (digitSum == power.Item1)
                        {
                            count++;
                            Console.WriteLine(count + "\t" + check.Key + " = " + power.Item1 + "^" + power.Item2);
                            if (count == 30)
                            {   
                                return;
                            }

                            break;
                        }
                    }
                }

                n++;
            }
        }
    }
}