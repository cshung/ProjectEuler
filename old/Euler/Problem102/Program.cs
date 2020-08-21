namespace Problem102
{
    using Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        /*
         * For each directed line segment, we wanted to know whether the origin is on the left or right of it.
         * To determine that, we transform the problem, W.L.O.G, let the directed line segment be (x1, y1) -> (x2, y2)
         * 
         * First, translate (x1, y1) to the origin, so the problem becomes (0, 0) -> (x2 - x1, y2 - y2), is (-x1, -y1) on the left or right.
         * 
         * Now, consider a general rotation and scale matrix, it must be of form [s,0;0;s] * [C,-S;S,C] where s is the scaling factor 
         * and C, S are the cosine and sine of the angle respectively.
         * 
         * The angle of the line segment has cosine = (x2 - x1)/Length(L), sine = (y2 - y1)/Length(L), we wanted to rotate it clockwise with this 
         * angle so that the line segment lies on the x-axis, we also want to scale by Length(L) to stay in integers, so the final transformation 
         * matrix is:
         * 
         * [(x2 - x1),(y2 - y1);(y1 - y2),(x2 - x1)]
         * 
         * It can easily be verified that ((x2 - x1),(y2 - y1)) transform to (L^2, 0), so the origin is on the left of the original line segment
         * if its transformed coordinate has positive y, otherwise it is on the right.
         * 
         * The y coordinate of the translated origin is -x1(y1 - y2)-y1(x2 - x1) = x1y2-x1y1+x1y1-x2y1 = x1y2-x2y1.
         * 
         * The another way to interpret this result is that, consider in the three dimensional space, we compute (x1, y1) x (x2, y2), if it is positive, 
         * then the origin is on the left of the line segment. I could have present it this way, but the above was the thought process I gone through.
         * 
         * Finally, if the origin stay consistently on one side for all 3 sides, then it is inside the triangle.
         */
        static void Main(string[] args)
        {
            string input = EulerUtil.ReadResourceAsString("Problem102.triangles.txt");
            string[] inputLines = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<Tuple<int, int, int, int, int, int>> triangles = inputLines.Select((line) => line.Split(',').Select(t=> int.Parse(t)).ToArray()).Select(lineSplitted => Tuple.Create(lineSplitted[0], lineSplitted[1], lineSplitted[2], lineSplitted[3], lineSplitted[4], lineSplitted[5]));
            int good = 0;
            foreach (var triangle in triangles)
            {
                int x1 = triangle.Item1;
                int y1 = triangle.Item2;
                int x2 = triangle.Item3;
                int y2 = triangle.Item4;
                int x3 = triangle.Item5;
                int y3 = triangle.Item6;
                int cross1 = x1 * y2 - x2 * y1;
                int cross2 = x2 * y3 - x3 * y2;
                int cross3 = x3 * y1 - x1 * y3;
                if (cross1 > 0 && cross2 > 0 && cross3 > 0)
                {
                    good++;
                }
                if (cross1 < 0 && cross2 < 0 && cross3 < 0)
                {
                    good++;
                }
            }
            Console.WriteLine(good);
        }
    }
}
