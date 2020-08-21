using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem120
{
    class Program
    {
        /*
         * Lemma: If m = n mod a, then ma = na mod a^2 
         * 
         * Proof:
         * m = n mod a => m - n = ka
         *             => ma - na = ka^2
         *             => ma = na mod a^2
         *             
         * Consider   (a - 1)^n + (a + 1)^n
         *          = Sum[r = 0 to n] C(n,r) a^r (-1)^{n - r} + C(n,r) a^r
         *          = Sum[r = 0 to n] C(n r) a^r ((-1)^{n - r} + 1)
         *          = 2 Sum[r = 0 to n, (n - r) % 2 == 0] C(n r) a^r
         *          = 2na (if n is odd) or 2 (if n is even)                    (mod a^2)
         *          
         * So the problem of finding r_max is simply 
         * maximize 2na (mod a^2), n is integer > 0, n is odd
         * =>
         * maximize 2(2p+1)a (mod a^2), p is integer >= 0
         * =>
         * maximize 4pa + 2a (mod a^2), p is integer >= 0
         * 
         * [... What I was thinking ...]
         * I am trying to make claim based on the extended Euclidean algorithm, but the + 2a part is troublesome, so I simply ignored that for the moment and 
         * notice the result will get shifted by 2a. Also use the lemma above, we cancel the 'a' on both side
         * 
         * The problem becomes investigate the possible set of values for 4p (mod a)
         * 
         * There are only 3 possible values for g = gcd(4, a), it could be 4, 2 or 1
         * By extended Euclidean algorithm, there exists m, n such that 4m + na = g, which means 4m = g (mod a)
         * 
         * Given that, we know we can choose p = km to make 4p = 4km = kg % a
         * In particular, if gcd(a, 4) == 1, then we can choose k = (a - 3)   so that 4p = a - 3              (mod a), and therefore 4pa = (a - 3)a % a^2 => 4pa + 2a = (a - 1)a % a^2.
         *                if gcd(a, 4) == 2, then we can choose k = (a/2 - 2) so that 4p = (a/2 - 2)2 = a - 4 (mod a), and therefore 4pa = (a - 4)a % a^2 => 4pa + 2a = (a - 2)a % a^2.
         *                if gcd(a, 4) == 4, then we can choose k = (a/4 - 1) so that 4p = (a/4 - 1)4 = a - 4 (mod a), and therefore 4pa = (a - 4)a % a^2 => 4pa + 2a = (a - 2)a % a^2.
         *                
         * Note that these are the maximum possible values for 4pa + 2a (mod a^2), to see why, first note that it is impossible for the value not a multiple of a, so (a - 1)a 
         * is in fact the maximum possible, also, if a is divisible by 2, 4pa + 2a must be divisible by 4, and therefore (a - 2)a is the maximum possible for it.
         * 
         * Now we know the maximum value for 4pa + 2a % a^2 is 
         * (a-1)a if gcd(a, 4) == 1
         * (a-2)a if gcd(a, 4) == 2 or 4
         * 
         * It can easily translated into the code below, the latter condition can simply be interpreted as a is even.
         * If we wanted to go further, we notice this is really just sum of squares, we can even compute the value directly.
         */
        static void Main(string[] args)
        {
            long rmax_sum = 0;
            for (int a = 3; a <= 1000; a++)
            {   
                int aSquared = a * a;
                int rmax2;
                if (a % 2 == 0)
                {
                    rmax2 = a * (a - 2);
                }
                else
                {
                    rmax2 = a * (a - 1);
                }

                rmax_sum += rmax2;
            }

            Console.WriteLine(rmax_sum);
        }
    }
}
