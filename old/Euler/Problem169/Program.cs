namespace Problem169
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Common;

    /*
     * Top down recursive + memorization based solution.
     * 
     * A simple lemma would be very useful:
     * 
     * Lemma: Any required representation of the number 2^k + b, where 0 <= b < 2^k (i.e. 2^k is the MSB) must use at least one of 2^k or 2^{k-1}
     * 
     * Proof: Consider the largest possible representation 2(1 + 2 + ... + 2^{k-2}) = 2(2^{k-1} - 1) = 2^k - 2, it is still less than 2^k + b
     *        Therefore it must have at least one of 2^k or 2^{k-1}
     * 
     * The lemma serves as the design for the algorithm. The representation either contains the MSB, or it doesn't. It leads to the recursion below.
     * 
     * Ways(2^k + b) = RestrictedWays(b |where it can't use 2^k more than once) + RestrictedWays(2^k + b - 2^{k-1} | where it can't use 2^k or 2^{k-1} more than once)
     *               = RestrictedWays(b |where it can't use 2^k more than once) + RestrictedWays(2^{k-1} + b | where it can't use 2^k or 2^{k-1} more than once)
     *               = RestrictedWays(b |where it can't use 2^k more than once) + RestrictedWays(r | where it can't use 2^k or 2^{k-1} more than once)
     * 
     * Obviously, in the above, r = 2^{k-1} + b
     * 
     * The first branch is particularly easy, we see b < 2^k, therefore the restriction is meaningless, we can set
     * RestrictedWays(b |where it can't use 2^k more than once) = Ways(b) without hurting anything at all.
     * 
     * The second branch is more complicated. We compute the bounds to the input first.
     * 
     * 0 <= b < 2^k
     * 2^{k-1} <= b + 2^{k-1} < 2^k+ 2^{k-1}
     * 2^{k-1} <= r < 3(2^{k-1})
     *
     * There are three cases and we will tackle them one-by-one:
     * 
     * Case 1: 2^{k-1} <= r < 2^k
     * **************************
     * Since r < 2^k, the no 2^k constraint is meaningless. 
     * We also know any solution using 2^{k-1} more than once will be at least 2^k, which is also meaningless.
     * Since both constraints are meaningless, we can set
     * 
     * RestrictedWays(r | where it can't use 2^k or 2^{k-1} more than once) = Ways(r)
     * 
     * Case 2: r = 2^k 
     * ***************
     * In this special case, we know all the representations are like this:
     * 
     * 2^k
     * 2^{k-1} + 2^{k-1}
     * 2^{k-1} + 2^{k-2} + 2^{k-2}
     * 2^{k-1} + 2^{k-2} + 2^{k-3} + 2^{k-3}
     * ...
     * 2^{k-1} + ... + 1 + 1
     * 
     * Overall, there will be (k + 1) lines above, therefore we can set
     * RestrictedWays(r | where it can't use 2^k or 2^{k-1} more than once) = k - 1
     * 
     * Case 3: 2^k < r < 3(2^{k-1})
     * ****************************
     * Since r > 2^k, and we cannot use 2^k, by our lemma, it must use 2^{k-1}, therefore
     * RestrictedWays(r | where it can't use 2^k or 2^{k-1} more than once) = RestrictedWays(r - 2^{k-1} | where it can't use 2^k or 2^{k-1})
     *                                                                      = RestrictedWays(b | where it can't use 2^k or 2^{k-1})
     *                                                                      = RestrictedWays(b | where it can't use 2^{k-1})
     * Note that the last equality above comes from the fact that we know b < 2^k, therefore, the first constraint is meaningless
     * 
     * We note that in case 3, we need a special value others than normal ways return - therefore we make Ways return a Tuple and in case 3, we will pick the non-MSB part of it.
     */
    internal static class Program
    {
        private static void Main(string[] args)
        {
            BigInteger input = EulerUtil.Power(BigInteger.Parse("10"), 25, (x, y) => x * y, 1);
            var output = Ways(input);
            Console.WriteLine(output.Item1 + output.Item2);
        }

        private static Dictionary<BigInteger, Tuple<long, long>> cache = new Dictionary<BigInteger, Tuple<long, long>>();

        private static Tuple<long, long> Ways(BigInteger x)
        {
            Tuple<long, long> result;
            if (!cache.TryGetValue(x, out result))
            {
                if (x == 0)
                {
                    return Tuple.Create(0L, 1L);
                }
                if (x == 1)
                {
                    return Tuple.Create(1L, 0L);
                }

                BigInteger msb = 1;
                int k = 0;
                while (msb < x)
                {
                    msb = msb * 2;
                    k++;
                }
                if (msb != x)
                {
                    msb = msb / 2;
                    k--;
                }
                Tuple<long, long> withMsbResult = Ways(x - msb);
                long withMsb = withMsbResult.Item1 + withMsbResult.Item2;
                BigInteger residue = x - msb / 2;
                long withoutMsb;
                if (residue == msb)
                {
                    withoutMsb = k - 1;
                }
                else
                {
                    if (residue < msb)
                    {
                        Tuple<long, long> withoutMsbResult = Ways(residue);
                        withoutMsb = withoutMsbResult.Item1 + withoutMsbResult.Item2;
                    }
                    else
                    {
                        withoutMsb = withMsbResult.Item2;
                    }
                }
                result = Tuple.Create(withMsb, withoutMsb);
                cache.Add(x, result);
            }

            return result;
        }
    }
}
