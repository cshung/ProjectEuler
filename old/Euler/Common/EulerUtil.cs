namespace Common
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Numerics;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    public static class EulerUtil
    {
        public static IEnumerable<TNode> GetShortestPath<TNode>(Dictionary<TNode, TNode> parents, TNode targetNode)
        {
            var current = targetNode;
            Stack<TNode> path = new Stack<TNode>();
            do
            {
                path.Push(current);
                current = parents[current];
            } while (current != null);
            while (path.Count != 0)
            {
                current = path.Pop();
                yield return current;
            }
        }

        public static Dictionary<TNode, TNode> BellmanFord<TNode>(TNode source, TNode[] nodes, Tuple<TNode, TNode, int>[] edges) where TNode : class
        {
            Dictionary<TNode, int> d = new Dictionary<TNode, int>();
            Dictionary<TNode, TNode> p = new Dictionary<TNode, TNode>();

            // Initialize Single Source
            foreach (TNode node in nodes)
            {
                d.Add(node, int.MaxValue);
                p.Add(node, null);
            }
            d[source] = 0;

            // Bellman Ford Iteration
            for (int i = 1; i <= nodes.Length - 1; i++)
            {
                foreach (var edge in edges)
                {
                    if (d[edge.Item1] != int.MaxValue)
                    {
                        // Relax 
                        if (d[edge.Item2] > d[edge.Item1] + edge.Item3)
                        {
                            d[edge.Item2] = d[edge.Item1] + edge.Item3;
                            p[edge.Item2] = edge.Item1;
                        }
                    }
                }
            }

            return p;
        }

        // http://en.wikipedia.org/wiki/Tree_of_primitive_Pythagorean_triples
        //
        // sumMax is the maximum sum that this algorithm will return - if the sum is larger than this sum it will not be returned
        // 
        // I have proved that following the matrices - the sum has to increase, so we can cut the search short by stopping once the sum exceed.
        // The proof is rather easy, note that taking sum of a column vector is just left multiply by all ones, and matrix multiplication is associative
        //
        // So we can get to the sum of the result without actually computing it. 
        // One can easy get sum = 5a - 5b + 7c (with A), sum = 5a + 5b + 7c (with B) and sum = -5a + 5b + 7c (with C)
        //
        // Finally, note that c > a and c > b and therefore the sum must be increased.
        //
        public static IEnumerable<Tuple<int, int, int>> GetPrimitivePythTriples(int? sumMax, int? oneLegMax)
        {
            var root = new Tuple<int, int, int>(3, 4, 5);
            Queue<Tuple<int, int, int>> bfsQueue = new Queue<Tuple<int, int, int>>();
            bfsQueue.Enqueue(root);
            while (bfsQueue.Count > 0)
            {
                var visiting = bfsQueue.Dequeue();
                int a = visiting.Item1;
                int b = visiting.Item2;
                int c = visiting.Item3;
                bool pass = true;
                pass = pass && (!sumMax.HasValue || (sumMax.HasValue && a + b + c <= sumMax.Value));
                pass = pass && (!oneLegMax.HasValue || (oneLegMax.HasValue && a <= oneLegMax.Value || b <= oneLegMax.Value));

                if (pass)
                {
                    yield return visiting;
                    bfsQueue.Enqueue(Tuple.Create(
                        1 * a - 2 * b + 2 * c,
                        2 * a - 1 * b + 2 * c,
                        2 * a - 2 * b + 3 * c));
                    bfsQueue.Enqueue(Tuple.Create(
                        1 * a + 2 * b + 2 * c, 
                        2 * a + 1 * b + 2 * c, 
                        2 * a + 2 * b + 3 * c));
                    bfsQueue.Enqueue(Tuple.Create(
                        -1 * a + 2 * b + 2 * c, 
                        -2 * a + 1 * b + 2 * c, 
                        -2 * a + 2 * b + 3 * c));
                }
            }
        }

        public static long EulerPhi(long i)
        {
            return EulerUtil.BruteForceFactor(i).GroupBy(t => t).Select(t => EulerUtil.Power(t.Key, t.Count() - 1, (x, y) => x * y, 1) * (t.Key - 1)).Aggregate(1L, (x, y) => x * y);
        }

        public static Tuple<int, List<int>> ContinuedFraction(QuadraticIrrational start)
        {
            QuadraticIrrational current = start;
            int floor = current.IntegerPart();
            current = current.NextForContinuedFraction();
            QuadraticIrrational first = current;
            List<int> period = new List<int>();
            while (true)
            {
                period.Add(current.IntegerPart());
                current = current.NextForContinuedFraction();
                if (first.Equals(current))
                {
                    break;
                }
            }

            return Tuple.Create(floor, period);
        }

        public static string ReadResourceAsString(string resourceName)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Assembly.GetEntryAssembly().GetManifestResourceStream(resourceName).CopyTo(ms);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
        }

        public static int[,] CreateSpiral(int size)
        {
            int[,] spiral = new int[size, size];
            int i, j;
            i = j = size / 2;
            int d = 3; // 0 -> left, 1 -> down, 2 -> right, 3 -> up
            int l = 1;
            int L = 0;
            for (int current = 1; current <= size * size; current++)
            {
                spiral[i, j] = current;

                // Use up the current sprint
                l--;
                // If the current sprint is used up, figure out new sprint and direction.
                if (l == 0)
                {
                    d = (d + 1) % 4;
                    if (d == 0 || d == 2)
                    {
                        // One should use a longer sprint on horizontal move
                        L += 1;
                    }
                    l = L;
                }
                switch (d)
                {
                    case 0: j++; break;
                    case 1: i++; break;
                    case 2: j--; break;
                    case 3: i--; break;
                }
            }
            return spiral;
        }

        public static BigInteger BinomialCoefficient(long n, long c)
        {
            return BinomialCoefficient(n, c, BuildPascalTriangle(n));
        }

        public static BigInteger BinomialCoefficient(long n, long c, BigInteger[,] pascalTriangle)
        {
            return pascalTriangle[n - 1, c];
        }

        public static BigInteger[,] BuildPascalTriangle(long n)
        {
            BigInteger[,] content = new BigInteger[n, n + 1];
            content[0, 0] = BigInteger.One;
            content[0, 1] = BigInteger.One;
            for (long i = 1; i < n; i++)
            {
                content[i, 0] = 1;
                for (long j = 1; j < i + 2; j++)
                {
                    content[i, j] = content[i - 1, j - 1] + content[i - 1, j];
                }
                content[i, i + 1] = 1;
            }
            return content;
        }

        public static string ToConcatString<T>(this IEnumerable<T> t, string separator = "")
        {
            return t.Aggregate(string.Empty, (s, x) => s + ((s.Length == 0) ? string.Empty : separator) + x);
        }

        // The Art of Computer Programming: Generating number of multiple radix number, defined as each digit having a different base.
        public static IEnumerable<IEnumerable<int>> MultiRadixNumbers(List<int> bases)
        {
            List<int> current = new List<int>();
            for (int i = 0; i < bases.Count; i++)
            {
                current.Add(0);
            }
            while (true)
            {
                yield return current;
                int currentDigit = 0;
                while (true)
                {
                    if (current[currentDigit] < bases[currentDigit])
                    {
                        current[currentDigit]++;
                        break;
                    }
                    else
                    {
                        current[currentDigit] = 0;
                        currentDigit++;
                        if (currentDigit == current.Count)
                        {
                            yield break;
                        }
                    }
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> available)
        {
            List<T> availableList = available.ToList();
            return Permutations(availableList.Select(t => Pair<bool, T>.Create(true, t)).ToArray(), availableList.Count);
        }

        private static IEnumerable<IEnumerable<T>> Permutations<T>(Pair<bool, T>[] available, int count)
        {
            if (count == 0)
            {
                yield return new List<T> { };
            }

            for (int i = 0; i < available.Length; i++)
            {
                if (available[i].Item1)
                {
                    available[i].Item1 = false;
                    foreach (IEnumerable<T> recursiveResult in Permutations(available, count - 1))
                    {
                        yield return new List<T> { available[i].Item2 }.Concat(recursiveResult);
                    }

                    available[i].Item1 = true;
                }
            }
        }

        public static IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> source, int count)
        {
            List<T> availableList = source.ToList();
            return Combinations(availableList.ToArray(), count, 0);
        }

        private static IEnumerable<IEnumerable<T>> Combinations<T>(T[] available, int count, int startingIndex)
        {
            if (count < 0)
            {
                yield break;
            }

            if (count == 0)
            {
                yield return new List<T> { };
            }

            for (int i = startingIndex; i < available.Length; i++)
            {
                foreach (IEnumerable<T> recursiveResult in Combinations(available, count - 1, i + 1))
                {
                    yield return new List<T> { available[i] }.Concat(recursiveResult);
                }
            }
        }

        public static IEnumerable<Tuple<T, U>> StackLists<T, U>(IEnumerable<T> first, IEnumerable<U> second)
        {
            IEnumerable<Tuple<T, int>> indexedMultiples = first.Select((i, j) => Tuple.Create(i, j));
            IEnumerable<Tuple<U, int>> indexedCode = second.Select((i, j) => Tuple.Create(i, j));
            return indexedMultiples.Join(indexedCode, a => a.Item2, a => a.Item2, (a, b) => Tuple.Create(a.Item1, b.Item1));
        }

        public static IEnumerable<int> Primes(int max)
        {
            if (max >= 2)
            {
                yield return 2;
            }

            // sieve[i] represents 2i + 3; [So sieve[0] = 3, sieve[1] = 5] ...
            BitArray sieve = new BitArray(max / 2);
            int i = 0;
            while (true)
            {
                int num = (2 * i) + 3;
                int current_i = i;
                var indexes = Enumerable.Range(1, (sieve.Length - 1 - current_i) / num).Select(t => (t * num) + current_i).ToList();
                Parallel.ForEach<int>(indexes, delegate(int index) { sieve[index] = true; });
                sieve[i] = false;
                i++;
                while (i < sieve.Length && sieve[i])
                {
                    i++;
                }

                if (i >= sieve.Length)
                {
                    break;
                }
            }

            for (int j = 0; j < sieve.Length; j++)
            {
                if (!sieve[j])
                {
                    int num = (2 * j) + 3;
                    if (num <= max)
                    {
                        yield return num;
                    }
                }
            }
        }

        public static T CommonFactor<T>(T m, T n, Func<T, T, T> mod, Func<T, bool> isZero)
        {
            while (true)
            {
                T r = mod(m, n);
                if (isZero(r))
                {
                    return n;
                }

                m = n;
                n = r;
            }
        }

        public static long CommonFactor(long m, long n)
        {
            while (true)
            {
                long r = m % n;
                if (r == 0)
                {
                    return n;
                }

                m = n;
                n = r;
            }
        }

        public static int CommonFactor(int m, int n)
        {
            while (true)
            {
                int r = m % n;
                if (r == 0)
                {
                    return n;
                }

                m = n;
                n = r;
            }
        }

        public static long CommonMultiple(long m, long n)
        {
            return m * n / EulerUtil.CommonFactor(m, n);
        }

        // Brute-force factoring 
        public static IEnumerable<long> BruteForceFactor(long n)
        {
            double bound = Math.Ceiling(Math.Sqrt(n));
            for (long i = 2; i <= bound; i++)
            {
                if (n % i == 0)
                {
                    yield return i;
                    long quotient = n / i;
                    if (quotient > 1)
                    {
                        foreach (long otherFactors in BruteForceFactor(quotient))
                        {
                            yield return otherFactors;
                        }
                    }

                    yield break;
                }
            }

            yield return n;
        }

        // Pollard's rho algorithm for factoring (note that it does not factor 2^n)
        public static List<long> PollardFactor(long n)
        {
            List<long> factors = new List<long>();
            while (true)
            {
                // Factor it
                long x = 2;
                long y = 2;
                long d = 1;
                while (d == 1)
                {
                    x = PollardSequence(x);
                    y = PollardSequence(PollardSequence(y));
                    long diff = x - y;
                    if (diff < 0)
                    {
                        diff = -diff;
                    }
                    d = EulerUtil.CommonFactor(diff, n);
                    if (d == n)
                    {
                        factors.Add(n);
                        break;
                    }
                }
                if (d == n) { break; }
                factors.Add(d);
                n = n / d;
            }

            return factors;
        }

        private static long PollardSequence(long x)
        {
            return (x % int.MaxValue) * (x % int.MaxValue) + 1;
        }

        public static long ModPower(long a, long u, long n)
        {
            //return long.Parse(BigInteger.ModPow(new BigInteger(a), new BigInteger(u), new BigInteger(n)).ToString());

            if (u == 0)
            {
                return 1;
            }

            if (u == 1)
            {
                return a;
            }

            long recursiveResult = ModPower(a, u / 2, n);
            long product = (recursiveResult * recursiveResult) % n;
            if (u % 2 == 1)
            {
                product = (product * a) % n;
            }

            return product;
        }

        // Implementing Euler's Criterion
        // http://en.wikipedia.org/wiki/Euler%27s_criterion
        public static bool IsQuadraticResidue(long a, long p)
        {
            return EulerUtil.ModPower(a, (p - 1) / 2, p) == 1;
        }

        // Find out the value k where k * k = a mod p
        // Implementing the Tonelli–Shanks algorithm
        // http://en.wikipedia.org/wiki/Tonelli%E2%80%93Shanks_algorithm
        public static long ModularSquareRoot(long n, long p)
        {
            if (!EulerUtil.IsQuadraticResidue(n, p))
            {
                throw new ArgumentException(string.Format("{0} is not a quadratic residue mod {1}", n, p));
            }

            long S = 0;
            long Q = p - 1;
            while (Q % 2 == 0)
            {
                S++;
                Q /= 2;
            }

            if (S == 1)
            {
                return ModPower(n, (p + 1) / 4, p);
            }

            // Brute-force search for a quadratic non-residue should be fine?
            // Not sure - bigger number will tell us the truth
            int z = 0;
            for (int i = 1; i < p; i++)
            {
                if (!IsQuadraticResidue(i, p))
                {
                    z = i;
                    break;
                }
            }
            long c = ModPower(z, Q, p);

            long R = ModPower(n, (Q + 1) / 2, p);
            long t = ModPower(n, Q, p);
            long M = S;

            while (t != 1)
            {
                long current = t;
                int i = 0;
                do
                {
                    //current = (current * current) % p;
                    current = long.Parse(BigInteger.ModPow(new BigInteger(current), new BigInteger(2), new BigInteger(p)).ToString());
                    i++;
                } while (current != 1);
                long b = ModPower(c, (long)Math.Pow(2, M - i - 1), p);
                //R = (R * b) % p;
                R = long.Parse(BigInteger.ModPow(BigInteger.Multiply(new BigInteger(R), new BigInteger(b)), BigInteger.One, new BigInteger(p)).ToString());
                //c = (b * b) % p;
                c = long.Parse(BigInteger.ModPow(new BigInteger(b), new BigInteger(2), new BigInteger(p)).ToString());
                //t = (t * c) % p;
                t = long.Parse(BigInteger.ModPow(BigInteger.Multiply(new BigInteger(t), new BigInteger(c)), BigInteger.One, new BigInteger(p)).ToString());
                M = i;
            }

            return R;
        }

        // Rabin-Miller Test from CLRS
        public static bool IsPrime(long n)
        {
            if (n == 1)
            {
                return false;
            }

            if (n % 2 == 0)
            {
                return false;
            }

            Random png = new Random(1);
            for (int j = 1; j <= 5000; j++)
            {
                long a = (long)(png.NextDouble() * (n - 2)) + 1;
                if (Witness(a, n))
                {
                    return false;
                }
            }

            return true;
        }

        public static T Power<T>(T value, int power, Func<T, T, T> multiply, T one)
        {
            if (power == 0)
            {
                return one;
            }
            else if (power == 1)
            {
                return value;
            }
            else
            {
                T recursiveResult = Power(value, power / 2, multiply, one);
                T result = multiply(recursiveResult, recursiveResult);
                if (power % 2 == 1)
                {
                    result = multiply(result, value);
                }
                return result;
            }
        }

        public static long Power(long value, int power)
        {
            return Power<long>(value, power, (x, y) => x * y, 1L);
        }

        private static bool Witness(long a, long n)
        {
            long t = 0;
            long u = n - 1;
            while (u % 2 == 0)
            {
                u = u / 2;
                t++;
            }

            long x0 = ModPower(a, u, n);
            long x1 = 1;
            for (int i = 1; i <= t; i++)
            {
                x1 = (x0 * x0) % n;
                if (x1 == 1 && x0 != 1 && x0 != (n - 1))
                {
                    return true;
                }

                x0 = x1;
            }

            if (x1 != 1)
            {
                return true;
            }

            return false;
        }

        // It returns the floor, round, ceiling of the square root
        public static Tuple<BigInteger, BigInteger, BigInteger> SquareRoot(BigInteger candidate)
        {
            BigRational square = new BigRational(candidate, BigInteger.One);
            BigRational currentOverEstimate = square;
            while (true)
            {
                currentOverEstimate = (currentOverEstimate + square / currentOverEstimate) * new BigRational(BigInteger.One, new BigInteger(2));
                BigRational currentUnderEstimate = square / currentOverEstimate;
                BigRational currentGap = currentOverEstimate - currentUnderEstimate;
                BigInteger quotient = currentOverEstimate.Numerator / currentOverEstimate.Denominator;
                if (currentGap.Numerator < currentGap.Denominator)
                {
                    // The difference between lower bound and upper bound is less than 1.
                    // So, quotient, as the floor of the current over estimate, is the only possibility 

                    BigInteger squareRootCeiling = quotient;
                    BigInteger ceilingDifference = squareRootCeiling * squareRootCeiling - candidate;

                    if (ceilingDifference == BigInteger.Zero)
                    {
                        // The floor, round and ceiling are all the same for perfect square.
                        return Tuple.Create(squareRootCeiling, squareRootCeiling, squareRootCeiling);
                    }
                    else
                    {
                        BigInteger squareRootFloor = squareRootCeiling - BigInteger.One;
                        BigInteger floorDifference = candidate - squareRootFloor * squareRootFloor;

                        BigInteger squareRootRound;

                        if (ceilingDifference > floorDifference)
                        {
                            squareRootRound = squareRootFloor;
                        }
                        else
                        {
                            squareRootRound = squareRootCeiling;
                        }

                        return Tuple.Create(squareRootFloor, squareRootRound, squareRootCeiling);
                    }
                    
                }
                currentOverEstimate = new BigRational(quotient + BigInteger.One, BigInteger.One);
            }
        }

        public static bool IsPerfectSquare(BigInteger candidate, out BigInteger root)
        {
            var roots = SquareRoot(candidate);
            // if ceiling == floor, the square root is an integer
            root = roots.Item3;
            return roots.Item1 == roots.Item3;
        }

        public static bool IsPentagonNumber(long candidate)
        {
            BigInteger scaled = new BigInteger(24) * new BigInteger(candidate) + new BigInteger(1);
            BigInteger root;
            if (IsPerfectSquare(scaled, out root))
            {
                BigInteger remainder;
                BigInteger.DivRem(root, new BigInteger(6), out remainder);
                if (remainder == 5)
                {
                    return true;
                }
            }
            return false;
        }
        public static TSrc ArgMax<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                throw new InvalidOperationException("Sequence has no elements.");

            TSrc t_try, t = e.Current;
            if (!e.MoveNext())
                return t;

            TArg v, max_val = fn(t);
            do
            {
                if ((v = fn(t_try = e.Current)).CompareTo(max_val) > 0)
                {
                    t = t_try;
                    max_val = v;
                }
            }
            while (e.MoveNext());
            return t;
        }

        public static TSrc ArgMin<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                throw new InvalidOperationException("Sequence has no elements.");

            TSrc t_try, t = e.Current;
            if (!e.MoveNext())
                return t;

            TArg v, min_val = fn(t);
            do
            {
                if ((v = fn(t_try = e.Current)).CompareTo(min_val) < 0)
                {
                    t = t_try;
                    min_val = v;
                }
            }
            while (e.MoveNext());
            return t;
        }

        public static int IndexOfMax<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                return -1;

            int max_ix = 0;
            TSrc t = e.Current;
            if (!e.MoveNext())
                return max_ix;

            TArg tx, max_val = fn(t);
            int i = 1;
            do
            {
                if ((tx = fn(e.Current)).CompareTo(max_val) > 0)
                {
                    max_val = tx;
                    max_ix = i;
                }
                i++;
            }
            while (e.MoveNext());
            return max_ix;
        }

        public static int IndexOfMin<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn)
            where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                return -1;

            int min_ix = 0;
            TSrc t = e.Current;
            if (!e.MoveNext())
                return min_ix;

            TArg tx, min_val = fn(t);
            int i = 1;
            do
            {
                if ((tx = fn(e.Current)).CompareTo(min_val) < 0)
                {
                    min_val = tx;
                    min_ix = i;
                }
                i++;
            }
            while (e.MoveNext());
            return min_ix;
        }

        public static IEnumerable<List<int>> Subsets(int n)
        {
            List<int> indexes = Enumerable.Range(1, n).ToList();
            foreach (var v in EulerUtil.MultiRadixNumbers(Enumerable.Range(1, n).Select(_ => 1).ToList()))
            {
                yield return Enumerable.Zip(v, indexes, (x, y) => x * y).Where(e => e != 0).ToList();
            }
        }

    }
}
