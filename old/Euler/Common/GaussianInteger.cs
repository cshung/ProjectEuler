namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using System.Numerics;

    public class GaussianInteger
    {
        private BigInteger real;

        private BigInteger imag;

        private GaussianInteger(BigInteger real, BigInteger imag)
        {
            this.real = real;
            this.imag = imag;
        }

        public static GaussianInteger Create(BigInteger real, BigInteger imag)
        {
            return new GaussianInteger(real, imag);
        }

        public BigInteger Real { get { return real; } }

        public BigInteger Imag { get { return imag; } }

        public static GaussianInteger Multiply(GaussianInteger a, GaussianInteger b)
        {
            return GaussianInteger.Create(a.Real * b.Real - a.Imag * b.Imag, a.Imag * b.Real + a.Real * b.Imag);
        }

        public static Tuple<GaussianInteger, GaussianInteger> Divide(GaussianInteger a, GaussianInteger b)
        {
            // To divide a Gaussian integer, we need to make sure the remainder has a smaller norm than the divisor
            // Turn out with this simple definition, it is still ambiguous what the quotient should be.
            // With that - we choose one that minimize the norm of the remainder.

            // Geometrically, we think of multiples of B lying on a square grid defined by B (as a vector) and Bi.
            // either a lies on the corner (i.e. divisible, norm of remainder = 0), or not
            // just find out the closest corner and we are done because with that, the remainder is smallest.

            // Conceptually, it is easier to reason about if B is simply 1. This can be achieved by rotating the whole space.
            // The rotation is done by multiplying the numbers by Conj(B/|B|), and then scaling by divide by |B|
            // Now, B' = 1, A' = A * Conj(B/|B|^2), and therefore A' is closest to the corner at Round(A') 
            // (i.e. Taking Round for real and imaginary parts, respectively)
            // 
            // Finally, we have to transform back to the original space, we simply do that by multiply by B (Because. B * Conj(B/|B|^2) = 1)
            // QB = Round(A')B
            //  Q = Round(A')
            //
            // It is obvious that Q is a Gaussian integer. R can be computed similarly

            GaussianInteger divisorConjugate = b.Conjugate();
            BigInteger denominator = b.Norm();
            GaussianInteger numeratorTransformed = Multiply(a, divisorConjugate);

            BigInteger realRemainder;
            BigInteger realQuotient = BigInteger.DivRem(numeratorTransformed.Real, denominator, out realRemainder);
            if (realRemainder > denominator / 2)
            {
                realQuotient = realQuotient + 1;
            }

            BigInteger imagRemainder;
            BigInteger imagQuotient = BigInteger.DivRem(numeratorTransformed.Imag, denominator, out imagRemainder);
            if (imagRemainder > denominator / 2)
            {
                imagQuotient = imagQuotient + 1;
            }

            GaussianInteger quotient = GaussianInteger.Create(realQuotient, imagQuotient);
            GaussianInteger remainder = Subtract(a, Multiply(b, quotient));

            return Tuple.Create(quotient, remainder);
        }

        public static GaussianInteger Subtract(GaussianInteger a, GaussianInteger b)
        {
            return GaussianInteger.Create(a.Real - b.Real, a.Imag - b.Imag);
        }

        public static GaussianInteger CommonFactor(GaussianInteger a, GaussianInteger b)
        {
            while (true)
            {
                GaussianInteger r = GaussianInteger.Divide(a, b).Item2;
                if (r.Norm() == 0)
                {
                    return b;
                }

                a = b;
                b = r;
            }
        }

        // http://stackoverflow.com/questions/2269810/whats-a-nice-method-to-factor-gaussian-integers
        // Note that there is a bug (that I don't bother to fix right now here) when n is a complex number
        // currently it only works for integers
        public static IEnumerable<GaussianInteger> Factorize(GaussianInteger n)
        {
            BigInteger real = n.Real;
            BigInteger imag = n.Imag;

            // This handles all the cases below
            // -1 - i
            //  0 - i
            //  1 - i
            // -1 + 0i
            //  0 + 0i
            //  1 + 0i
            // -1 + i
            //  0 + i
            //  1 + i
            // All of them are either unit or prime
            if ((real == -1 || real == 0 || real == 1) && (imag == -1 || imag == 0 || imag == 1))
            {
                yield return n;
                yield break;
            }

            IEnumerable<long> factors;
            if (imag == 0)
            {
                var realFactors = EulerUtil.BruteForceFactor(long.Parse(real.ToString())).ToList();
                var normFactors = realFactors.Concat(realFactors).ToList();
                normFactors.Sort();
                factors = normFactors;
            }
            else
            {
                Debug.Fail("Not supported");
                // Note that if we are factorizing an integer, factorizing the norm is significantly harder than factorizing the integer itself
                // So it is more wise to factorize the integer first
                //factors = Utilities.BruteForceFactor(n.Norm());
                factors = null;
            }

            // Now group this factor so each prime factor is handled only once.
            var groupedFactors = factors.GroupBy(z => z).Select(t => Tuple.Create(t.Key, t.Count()));
            
            foreach (var realFactorGroup in groupedFactors)
            {
                var realFactor = realFactorGroup.Item1;
                var realFactorPower = realFactorGroup.Item2;
                GaussianInteger factor = null;
                if (realFactor == 1)
                {
                    break;
                }
                else if (realFactor == 2)
                {
                    factor = GaussianInteger.Create(1L, 1L);
                }
                else if (realFactor % 4 == 3)
                {
                    factor = GaussianInteger.Create(realFactor, 0L);
                    Debug.Assert(realFactorPower % 2 == 0);
                    realFactorPower /= 2;
                }
                else
                {
                    BigInteger k = EulerUtil.ModularSquareRoot(realFactor - 1, realFactor);
                    factor = GaussianInteger.CommonFactor(GaussianInteger.Create(k, 1L), GaussianInteger.Create(realFactor, 0L));
                    var trialDivision = GaussianInteger.Divide(n, factor);
                    if (trialDivision.Item2.Real == 0 && trialDivision.Item2.Imag == 0)
                    {
                        // factor is good
                    }
                    else
                    {
                        factor = factor.Conjugate();
                    }
                }

                if (factor != null)
                {
                    for (int i = 0; i < realFactorPower; i++)
                    {
                        yield return factor;
                        factor = factor.Conjugate();
                        var divisionResult = GaussianInteger.Divide(n, factor);
                        n = divisionResult.Item1;
                        Debug.Assert(divisionResult.Item2.Real == 0);
                        Debug.Assert(divisionResult.Item2.Imag == 0);
                    }
                }
            }

            yield return n;
        }

        public BigInteger Norm()
        {
            return this.Real * this.Real + this.Imag * this.Imag;
        }

        public GaussianInteger Conjugate()
        {
            return GaussianInteger.Create(this.Real, -this.Imag);
        }

        public override string ToString()
        {
            if (this.Imag == 0)
            {
                return string.Format("({0})", this.Real.ToString());
            }
            else if (this.Real == 0)
            {
                if (this.Imag == 1)
                {
                    return "(i)";
                }
                else if (this.Imag == -1)
                {
                    return "(-i)";
                }
                else
                {
                    return string.Format("({0}i)", this.Imag.ToString());
                }
            }
            else
            {
                string imagString = string.Empty;
                if (this.Imag != 1 && this.Imag != -1)
                {
                    imagString = (this.Imag > 0 ? this.Imag : -this.Imag).ToString();
                }

                return string.Format("({0} {1} {2}i)", this.Real, this.Imag > 0 ? "+" : "-", imagString);
            }
        }

        public override bool Equals(object obj)
        {
            GaussianInteger that = obj as GaussianInteger;
            if (that == null)
            {
                return false;
            }
            else
            {
                return this.Real == that.Real && this.Imag == that.Imag;
            }
        }

        public override int GetHashCode()
        {
            return (int)(this.Real ^ this.Imag);
        }
    }
}