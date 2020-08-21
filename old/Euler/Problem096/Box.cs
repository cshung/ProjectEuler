namespace SudokuSolver
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;

    [DebuggerDisplay("{Identity}")]
    internal class Box
    {
        public const string RowFormat = "Row {0}";
        public const string ColumnFormat = "Column {0}";
        public const string BlockFormat = "Block ({0},{1})";

        private static bool initialized = false;
        private static Collection<Box> boxes;
        private static Box[] rows;
        private static Box[] columns;

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member", Justification = "We are using all spaces.")]
        private int[,] indices = new int[2, 9];
        private BoxType boxType;

        private Box(int[,] indices, BoxType boxType, string identity)
        {
            this.boxType = boxType;
            this.Identity = identity;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < SudokuGame.SudokuSize; j++)
                {
                    this.indices[i, j] = indices[i, j];
                }
            }
        }

        private enum BoxType
        {
            Row,
            Column,
            Block,
        }

        public IEnumerable<Location> Locations
        {
            get
            {
                for (int i = 0; i < SudokuGame.SudokuSize; i++)
                {
                    yield return new Location(this.indices[0, i], this.indices[1, i]);
                }
            }
        }

        public string Identity { get; private set; }

        public bool IsRow
        {
            get
            {
                return this.boxType == BoxType.Row;
            }
        }

        public bool IsColumn
        {
            get
            {
                return this.boxType == BoxType.Column;
            }
        }

        public bool IsBlock
        {
            get
            {
                return this.boxType == BoxType.Block;
            }
        }

        public IEnumerable<Box> IntersectingNonBlocks
        {
            get
            {
                if (!this.IsBlock)
                {
                    throw new InvalidOperationException();
                }

                return this.IntersectingNonBlocksForBlock;
            }
        }

        internal static Collection<Box> Boxes
        {
            get
            {
                Box.EnsureInitialized();
                return Box.boxes;
            }
        }

        private static Box[] Rows
        {
            get
            {
                Box.EnsureInitialized();
                return Box.rows;
            }
        }

        private static Box[] Columns
        {
            get
            {
                Box.EnsureInitialized();
                return Box.columns;
            }
        }

        private IEnumerable<Box> IntersectingNonBlocksForBlock
        {
            get
            {
                Location[] locations = this.Locations.ToArray();
                yield return Box.Columns[locations[0].Column - 1];
                yield return Box.Columns[locations[1].Column - 1];
                yield return Box.Columns[locations[2].Column - 1];
                yield return Box.Rows[locations[0].Row - 1];
                yield return Box.Rows[locations[3].Row - 1];
                yield return Box.Rows[locations[6].Row - 1];
            }
        }

        public static Box CreateBoxForRowForUnitTest(int rowIndex)
        {
            return Box.CreateBoxForRow(rowIndex);
        }

        public static Box CreateBoxForColumnForUnitTest(int colIndex)
        {
            return Box.CreateBoxForColumn(colIndex);
        }

        public static Box CreateBoxForBlockForUnitTest(int blockRow, int blockCol)
        {
            return Box.CreateBoxForBlock(blockRow, blockCol);
        }

        public HashSet<int> GetValues(SudokuGame problem)
        {
            HashSet<int> values = new HashSet<int>();
            foreach (Location location in this.Locations)
            {
                int value = problem.GetValue(location);
                if (value != 0)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        // TODO, design, seems to be logic that belongs to solver.
        public bool ContainsDuplicate(SudokuGame problem)
        {
            HashSet<int> valueSeen = new HashSet<int>();
            foreach (Location location in this.Locations)
            {
                int value = problem.GetValue(location);
                if (value != 0)
                {
                    if (valueSeen.Contains(value))
                    {
                        return true;
                    }
                    else
                    {
                        valueSeen.Add(value);
                    }
                }
            }

            return false;
        }

        private static void EnsureInitialized()
        {
            if (!Box.initialized)
            {
                Box.Initialize();
            }
        }

        private static void Initialize()
        {
            Box.boxes = new Collection<Box>();
            Box.rows = new Box[9];
            Box.columns = new Box[9];
            for (int rowIndex = 1; rowIndex <= SudokuGame.SudokuSize; rowIndex++)
            {
                Box row = Box.CreateBoxForRow(rowIndex);
                Box.rows[rowIndex - 1] = row;
                Box.boxes.Add(row);
            }

            for (int columnIndex = 1; columnIndex <= SudokuGame.SudokuSize; columnIndex++)
            {
                Box column = Box.CreateBoxForColumn(columnIndex);
                Box.columns[columnIndex - 1] = column;
                Box.boxes.Add(column);
            }

            for (int blockRow = 1; blockRow <= 3; blockRow++)
            {
                for (int blockColumn = 1; blockColumn <= 3; blockColumn++)
                {
                    Box block = Box.CreateBoxForBlock(blockRow, blockColumn);
                    Box.boxes.Add(block);
                }
            }

            initialized = true;
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "We are using all spaces.")]
        private static Box CreateBoxForRow(int rowIndex)
        {
            int[,] indices = new int[2, 9];
            for (int columnIndex = 1; columnIndex <= SudokuGame.SudokuSize; columnIndex++)
            {
                indices[0, columnIndex - 1] = rowIndex;
                indices[1, columnIndex - 1] = columnIndex;
            }

            return new Box(indices, BoxType.Row, string.Format(CultureInfo.InvariantCulture, Box.RowFormat, rowIndex));
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "We are using all spaces.")]
        private static Box CreateBoxForColumn(int columnIndex)
        {
            int[,] indices = new int[2, 9];
            for (int rowIndex = 1; rowIndex <= SudokuGame.SudokuSize; rowIndex++)
            {
                indices[0, rowIndex - 1] = rowIndex;
                indices[1, rowIndex - 1] = columnIndex;
            }

            return new Box(indices, BoxType.Column, string.Format(CultureInfo.InvariantCulture, Box.ColumnFormat, columnIndex));
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "We are using all spaces.")]
        private static Box CreateBoxForBlock(int blockRow, int blockColumn)
        {
            int indicesIndex = 0;
            int[,] indices = new int[2, 9];
            for (int rowIndex = 0; rowIndex < SudokuGame.SudokuSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < SudokuGame.SudokuSize; columnIndex++)
                {
                    // blockRow/blockCol is 1 based index and therefore we need to -1 to fit the division rule
                    if ((rowIndex / 3 == (blockRow - 1)) && (columnIndex / 3 == (blockColumn - 1)))
                    {
                        indices[0, indicesIndex] = rowIndex + 1;
                        indices[1, indicesIndex] = columnIndex + 1;
                        indicesIndex++;
                    }
                }
            }

            return new Box(indices, BoxType.Block, string.Format(CultureInfo.InvariantCulture, Box.BlockFormat, blockRow, blockColumn));
        }
    }
}
