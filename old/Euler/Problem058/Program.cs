namespace Problem058
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<int> values = SpiralCorners();
            IEnumerator<int> valueEnumerator = values.GetEnumerator();
            int count = 1, primeCount = 0, side = 1;
            while (true)
            {
                valueEnumerator.MoveNext();
                int corner1 = valueEnumerator.Current;
                valueEnumerator.MoveNext();
                int corner2 = valueEnumerator.Current;
                valueEnumerator.MoveNext();
                int corner3 = valueEnumerator.Current;
                count += 4;
                side += 2;
                if (EulerUtil.IsPrime(corner1)) { primeCount++; }
                if (EulerUtil.IsPrime(corner2)) { primeCount++; }
                if (EulerUtil.IsPrime(corner3)) { primeCount++; }
                if (primeCount * 10 < count)
                {
                    break;
                }
            }
            Console.WriteLine(side);
        }

        static IEnumerable<int> SpiralCorners()
        {
            int size = int.MaxValue;
            // TODO, it would be nice to make this generic multiplexing
            var spiralCorners = Enumerable.Range(1, size).Select(i =>
                Tuple.Create(
                (4 * i * i - 2 * i + 1),
                (4 * i * i + 1),
                (4 * i * i + 2 * i + 1)
                ));
            foreach (var spiralCorner in spiralCorners)
            {
                yield return spiralCorner.Item1;
                yield return spiralCorner.Item2;
                yield return spiralCorner.Item3;
            }
        }
    }
}