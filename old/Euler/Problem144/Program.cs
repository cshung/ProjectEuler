using System;

namespace Problem144
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal startX = 0;
            decimal startY = (decimal)10.1;

            decimal endX = (decimal)1.4;
            decimal endY = (decimal)-9.6;

            //Console.WriteLine(startX + "," + startY);
            //Console.WriteLine(endX + "," + endY);

            int count = 1;
            while (true)
            {
                // Calculus - slope of ellipse
                decimal slope = -4 * endX / endY;

                // Translate
                decimal shiftedX = startX - endX;
                decimal shiftedY = startY - endY;

                // Rotate and Reflect and Rotate Back
                // Note that I short circuited the Translate back and use that as direction directly
                decimal dx = (-shiftedX - 2 * slope * shiftedY + slope * slope * shiftedX) / (1 + slope * slope);
                decimal dy = (shiftedY - 2 * slope * shiftedX - slope * slope * shiftedY) / (1 + slope * slope);

                // Solve the parameter of the line pointing from end(x|y) with direction d(x|y)
                decimal t = -(8 * endX * dx + 2 * endY * dy) / (4 * dx * dx + dy * dy);

                // Back substitute to the line the get the point on the ellipse after the beam reflection
                decimal clippedX = endX + t * dx;
                decimal clippedY = endY + t * dy;

                // Iterates
                startX = endX;
                startY = endY;

                endX = clippedX;
                endY = clippedY;

                //Console.WriteLine(clippedX + "," + clippedY);

                if (clippedX < (decimal)0.01 && clippedX > (decimal)-0.01 && clippedY > 0)
                {
                    break;
                }
                count++;
            }

            Console.WriteLine(count);
        }
    }
}
