namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class QuadraticIrrational
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public int D { get; set; }

        public int IntegerPart()
        {
            return (int)Math.Floor((A + B * Math.Sqrt(C)) / D);
        }

        public QuadraticIrrational NextForContinuedFraction()
        {
            int F = IntegerPart();
            int newA = D * A - D * D * F;
            int newB = -D * B;
            int newC = C;
            int newD = A * A - 2 * A * D * F + D * D * F * F - B * B * C;
            // Try factorize
            int commonFactor = EulerUtil.CommonFactor(newD, EulerUtil.CommonFactor(newA, newB));
            newA = newA / commonFactor;
            newB = newB / commonFactor;
            newD = newD / commonFactor;
            return new QuadraticIrrational { A = newA, B = newB, C = newC, D = newD };
        }

        // Intentionally implemented such that if B is not exactly the same the value wouldn't count
        public override bool Equals(object obj)
        {
            QuadraticIrrational that = obj as QuadraticIrrational;
            if (that != null)
            {
                return this.A == that.A && this.B == that.B && this.C == that.C && this.D == that.D;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.A.GetHashCode() ^ this.B.GetHashCode() ^ this.C.GetHashCode() ^ this.D.GetHashCode();
        }
    }
}
