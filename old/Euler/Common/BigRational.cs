namespace Common
{
    using System.Numerics;

    public class BigRational
    {
        private BigInteger numerator;
        private BigInteger denominator;

        public BigRational(BigInteger numerator, BigInteger denominator)
            : this(numerator, denominator, true)
        {
        }

        public BigRational(BigInteger numerator, BigInteger denominator, bool shouldFactorize)
        {
            this.numerator = numerator;
            this.denominator = denominator;
            if (shouldFactorize)
            {
                Factorize();
            }
            if (this.denominator < 0)
            {
                this.denominator = BigInteger.Negate(this.denominator);
                this.numerator = BigInteger.Negate(this.numerator);
            }
        }

        public BigInteger Numerator { get { return this.numerator; } }

        public BigInteger Denominator { get { return this.denominator; } }

        public static BigRational One = new BigRational(BigInteger.One, BigInteger.One);

        public void Factorize()
        {
            if (denominator != 0)
            {
                BigInteger commonFactor = EulerUtil.CommonFactor(numerator, denominator, Mod, (t) => t == 0);
                numerator = numerator / commonFactor;
                denominator = denominator / commonFactor;
            }
        }

        private static BigInteger Mod(BigInteger a, BigInteger b)
        {
            BigInteger result;
            BigInteger.DivRem(a, b, out result);
            return result;
        }

        public static BigRational operator +(BigRational a, BigRational b)
        {
            return Add(a, b);
        }

        public static BigRational operator -(BigRational a, BigRational b)
        {
            return Subtract(a, b);
        }

        public static BigRational operator *(BigRational a, BigRational b)
        {
            return Multiply(a, b);
        }

        public static BigRational operator /(BigRational a, BigRational b)
        {
            return Divide(a, b);
        }

        public override string ToString()
        {
            if (this.Denominator == 0)
            {
                return "NaN";
            }
            else if (this.Denominator == 1)
            {
                return this.Numerator + "";
            }
            else
            {
                return this.Numerator + " / " + this.Denominator;
            }
        }

        private static BigRational Add(BigRational a, BigRational b)
        {
            return new BigRational(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        private static BigRational Subtract(BigRational a, BigRational b)
        {
            return new BigRational(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);
        }

        public static BigRational Multiply(BigRational a, BigRational b)
        {
            // TODO - Bug - it is possible that factoring is required
            return new BigRational(a.Numerator * b.Numerator, a.Denominator * b.Denominator, false);
        }

        public static BigRational Divide(BigRational a, BigRational b)
        {
            // TODO - Bug - it is possible that factoring is required
            return new BigRational(a.Numerator * b.Denominator, a.Denominator * b.Numerator, false);
        }
    }
}
