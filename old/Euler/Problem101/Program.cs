using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using Common;

namespace Problem101
{
    class Program
    {
        static void Main(string[] args)
        {
            // Horner's Rule for evaluating polynomial
            //   n^10 - n^9 + n^8 - n^7 + n^6 - n^5 + n^4 - n^3 + n^2 - n + 1
            // = n(n^9 - n^8 + n^7 - n^6 + n^5 - n^4 + n^3 - n^2 + n^1 - 1) + 1
            // = n(n(n^8 - n^7 + n^6 - n^5 + n^4 - n^3 + n^2 - n + 1) - 1) + 1
            // = n(n(n(n^7 - n^6 + n^5 - n^4 + n^3 - n^2 + n - 1) + 1) - 1) + 1
            // = n(n(n(n(n^6 - n^5 + n^4 - n^3 + n^2 - n + 1) - 1) + 1) - 1) + 1
            // = n(n(n(n(n(n^5 - n^4 + n^3 - n^2 + n - 1) + 1) - 1) + 1) - 1) + 1
            // = n(n(n(n(n(n(n^4 - n^3 + n^2 - n + 1) - 1) + 1) - 1) + 1) - 1) + 1
            // = n(n(n(n(n(n(n(n^3 - n^2 + n - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1
            // = n(n(n(n(n(n(n(n(n^2 - n + 1) - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1
            // = n(n(n(n(n(n(n(n(n(n - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1

            var xs = Enumerable.Range(1, 15).Select(t => (BigInteger)t).ToList();
            var ys = xs.Select(t => Evaluate(t)).ToList();

            BigRational bopSum = new BigRational(0, 1);
            for (int numberOfTerms = 1; numberOfTerms <= 10; numberOfTerms++)
            {
                BigPolynomial lagrange = new BigPolynomial(new List<BigRational> { new BigRational(0, 1) });
                for (int i = 0; i < numberOfTerms; i++)
                {
                    BigPolynomial lagrangeTerm = new BigPolynomial(new List<BigRational> { new BigRational(1, 1) });
                    for (int j = 0; j < numberOfTerms; j++)
                    {
                        BigInteger denominator;
                        if (i != j)
                        {
                            denominator = xs[i] - xs[j];
                            lagrangeTerm = BigPolynomial.Multiply(lagrangeTerm, new BigPolynomial(new List<BigRational> { new BigRational(-xs[j], denominator), new BigRational(1, denominator) }));
                        }
                    }
                    lagrangeTerm = BigPolynomial.Multiply(lagrangeTerm, new BigPolynomial(new List<BigRational> { new BigRational(ys[i], 1) }));
                    lagrange = BigPolynomial.Add(lagrange, lagrangeTerm);
                }

                //for (int i = 1; i <= numberOfTerms; i++)
                //{
                //    Console.WriteLine(lagrange.Evaluate(new BigRational(i, 1)) + " = " + ys[i - 1]);
                //}

                for (int i = numberOfTerms + 1; i <= 15; i++)
                {
                    BigRational estimate = lagrange.Evaluate(new BigRational(i, 1));
                    if (!BigInteger.Equals(estimate, ys[i - 1]))
                    {
                        bopSum += estimate;
                        break;
                    }
                }
            }
            Console.WriteLine(bopSum);
        }

        private static BigInteger Evaluate(BigInteger n)
        {
            return n * (n * (n * (n * (n * (n * (n * (n * (n * (n - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1) - 1) + 1;
        }
    }

    class BigPolynomial
    {
        // The 1st entry in the list is the constant term, and so on ...
        private List<BigRational> coefficients;

        public BigPolynomial(List<BigRational> coefficients)
        {
            this.coefficients = coefficients;
        }

        public static BigPolynomial Add(BigPolynomial first, BigPolynomial second)
        {
            IEnumerable<BigRational> firstCoefficients = first.coefficients;
            IEnumerable<BigRational> secondCoefficients = second.coefficients;
            if (first.coefficients.Count > second.coefficients.Count)
            {
                secondCoefficients = secondCoefficients.Concat(Enumerable.Range(1, first.coefficients.Count - second.coefficients.Count).Select(_ => new BigRational(0, 1)));
            }
            else if (second.coefficients.Count > first.coefficients.Count)
            {
                firstCoefficients = firstCoefficients.Concat(Enumerable.Range(1, second.coefficients.Count - first.coefficients.Count).Select(_ => new BigRational(0, 1)));
            }

            List<BigRational> resultCoefficients = Enumerable.Zip(firstCoefficients, secondCoefficients, (x, y) => x + y).ToList();

            return new BigPolynomial(resultCoefficients);
        }

        public static BigPolynomial Multiply(BigPolynomial first, BigPolynomial second)
        {
            // Naive polynomial multiplication
            BigPolynomial current = new BigPolynomial(new List<BigRational> { new BigRational(0, 1) });
            for (int i = 0; i < second.coefficients.Count; i++)
            {
                BigRational ratio = second.coefficients[i];
                var multiplied = first.coefficients.Select(t => t * ratio);
                var shifted = Enumerable.Range(1, i).Select(_ => new BigRational(0, 1)).Concat(multiplied);
                current = BigPolynomial.Add(current, new BigPolynomial(shifted.ToList()));
            }

            return current;
        }

        public BigRational Evaluate(BigRational input)
        {
            if (this.coefficients.Count == 1)
            {
                return this.coefficients[0];
            }
            else
            {
                return this.coefficients[0] + new BigPolynomial(this.coefficients.Skip(1).ToList()).Evaluate(input) * input;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in this.coefficients.Reverse<BigRational>())
            {
                sb.Append(element.ToString());
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
