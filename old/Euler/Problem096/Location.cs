namespace SudokuSolver
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    [DebuggerDisplay("({Name})")]
    internal class Location
    {
        private int row;
        private int column;

        public Location(int row, int column)
        {
            if (row <= 0 || row > 9)
            {
                throw new ArgumentException("1 <= row <= 9 is required.");
            }

            if (column <= 0 || column > 9)
            {
                throw new ArgumentException("1 <= column <= 9 is required.");
            }

            this.row = row;
            this.column = column;
        }

        public static IEnumerable<Location> Locations
        {
            get
            {
                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        yield return new Location(i, j);
                    }
                }
            }
        }

        public int Row
        {
            get
            {
                return this.row;
            }
        }

        public int Column
        {
            get
            {
                return this.column;
            }
        }

        public string Name
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "({0}.{1})", this.Row, this.Column);
            }
        }

        public override bool Equals(object obj)
        {
            Location that = obj as Location;
            if (that == null)
            {
                return false;
            }

            return this.Row == that.Row && this.Column == that.Column;
        }

        public override int GetHashCode()
        {
            return this.Row ^ this.Column;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
