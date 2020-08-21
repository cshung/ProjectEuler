namespace SudokuSolver
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    internal sealed class OptionTable
    {
        private Dictionary<Location, HashSet<int>> options;
        private SudokuGame problem;

        public OptionTable(SudokuGame problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException("problem");
            }

            this.problem = problem;
            this.options = new Dictionary<Location, HashSet<int>>();
            foreach (Location location in Location.Locations)
            {
                if (problem.GetValue(location) == 0)
                {
                    this.options.Add(location, new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
                }
            }

            this.EliminateOptionsByBoxes();
        }

        public SudokuGame Problem
        {
            get
            {
                return this.problem;
            }
        }

        // Not unit tested - for speed solving
        public bool ValueSet { get; set; }

        public void RemoveOptionFromLocation(Location location, int value)
        {
            HashSet<int> optionSet = this.GetOptionSet(location);
            if (!optionSet.Contains(value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "{0} is not an available option.", value));
            }

            optionSet.Remove(value);
        }

        public bool IsOptionAvailable(Location location, int value)
        {
            return this.GetOptionSet(location).Contains(value);
        }

        public HashSet<int> GetOptionsFromLocationForUnitTest(Location location)
        {
            return new HashSet<int>(this.GetOptionSet(location));
        }

        // Intentionally not unit tested - just proxy method
        public bool IsEmpty(Location location)
        {
            return this.problem.IsEmpty(location);
        }

        public void SetValue(Location location, int value)
        {
            this.ValueSet = true;
            this.options.Remove(location);
            this.problem.SetValue(location, value);
            
            // TODO, 2, performance - one could have Eliminate once after setting many values, and eliminate the intersecting boxes only
            this.EliminateOptionsByBoxes();
        }

        public bool IsFilled(Location location)
        {
            return this.problem.IsFilled(location);
        }

        public int GetValue(Location location)
        {
            return this.problem.GetValue(location);
        }

        public bool TryGetSingleOption(Location location, out int singleOption)
        {
            singleOption = 0;
            HashSet<int> optionSet = this.GetOptionSet(location);
            if (optionSet.Count == 1)
            {
                singleOption = optionSet.First();
                return true;
            }
            else
            {
                return false;
            }
        }

        // TODO, add feature, shall we also style the borders?
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("<table border='1'>");
            for (int tableRowIndex = 0; tableRowIndex < 27; tableRowIndex++)
            {
                result.Append("<tr>");
                for (int tableColIndex = 0; tableColIndex < 27; tableColIndex++)
                {
                    int cellRowIndex = tableRowIndex / 3;
                    int cellColIndex = tableColIndex / 3;
                    int miniCellRowIndex = tableRowIndex % 3;
                    int miniCellColIndex = tableColIndex % 3;
                    Location cellLocation = new Location(cellRowIndex + 1, cellColIndex + 1);
                    if (this.IsFilled(cellLocation))
                    {
                        if ((miniCellRowIndex == 0) && (miniCellColIndex == 0))
                        {
                            result.Append("<td rowspan='3' colspan='3'>");
                            result.Append(this.GetValue(cellLocation));
                            result.Append("</td>");
                        }
                    }
                    else
                    {   
                        int valueCandidate = (miniCellRowIndex * 3) + miniCellColIndex + 1;
                        result.Append("<td>");
                        if (this.IsOptionAvailable(cellLocation, valueCandidate))
                        {
                            result.Append(valueCandidate);
                        }
                        else
                        {
                            result.Append("&nbsp;");
                        }

                        result.Append("</td>");
                    }
                }

                result.Append("</tr>");
            }

            result.Append("</table>");
            return result.ToString();
        }

        public void EliminateIntersection()
        {
            IEnumerable<Box> blocks = from box in Box.Boxes where box.IsBlock select box;
            foreach (Box block in blocks)
            {
                foreach (Box nonBlock in block.IntersectingNonBlocks)
                {
                    // TODO, design, look like Box specific logic
                    HashSet<Location> blockLocations = new HashSet<Location>(block.Locations);
                    HashSet<Location> nonBlockLocations = new HashSet<Location>(nonBlock.Locations);
                    HashSet<Location> intersection = new HashSet<Location>();
                    foreach (Location location in blockLocations)
                    {
                        if (nonBlockLocations.Contains(location))
                        {
                            intersection.Add(location);
                        }
                    }

                    foreach (Location location in intersection)
                    {
                        blockLocations.Remove(location);
                        nonBlockLocations.Remove(location);
                    }

                    HashSet<int> blockValues = block.GetValues(this.problem);
                    HashSet<int> nonBlockValues = nonBlock.GetValues(this.problem);
                    for (int value = 1; value <= 9; value++)
                    {
                        // TODO, design, we can make this code more functional. 
                        this.EliminateIntersection(blockLocations, nonBlockLocations, blockValues, value);
                        this.EliminateIntersection(nonBlockLocations, blockLocations, nonBlockValues, value);
                    }
                }
            }
        }

        private void EliminateIntersection(HashSet<Location> leftLocations, HashSet<Location> rightLocations, HashSet<int> leftValues, int value)
        {
            if (!leftValues.Contains(value))
            {
                bool possible = false;
                IEnumerable<Location> unfilledLeftLocations = from l in leftLocations where !this.IsFilled(l) select l;
                foreach (Location unfilledBlockLocation in unfilledLeftLocations)
                {
                    if (this.IsOptionAvailable(unfilledBlockLocation, value))
                    {
                        possible = true;
                    }
                }

                if (!possible)
                {
                    // It is impossible in the block only region - meaning it must be in the intersection
                    IEnumerable<Location> unfilledNonBlockLocations = from l in rightLocations where !this.IsFilled(l) select l;
                    foreach (Location unfilledNonBlockLocation in unfilledNonBlockLocations)
                    {
                        if (this.IsOptionAvailable(unfilledNonBlockLocation, value))
                        {
                            this.RemoveOptionFromLocation(unfilledNonBlockLocation, value);
                        }
                    }
                }
            }
        }

        private void EliminateOptionsByBoxes()
        {
            foreach (Box box in Box.Boxes)
            {
                foreach (int value in box.GetValues(this.problem))
                {
                    foreach (Location location in box.Locations)
                    {
                        if (this.problem.IsEmpty(location) && this.IsOptionAvailable(location, value))
                        {
                            this.RemoveOptionFromLocation(location, value);
                        }
                    }
                }
            }
        }

        private HashSet<int> GetOptionSet(Location location)
        {
            if (!this.options.ContainsKey(location))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Location {0} is already occupied", location));
            }

            return this.options[location];
        }

        public bool Solved
        {
            get
            {
                return this.options.Count == 0;
            }
        }

        // For quick experimentation of back-tracking solver
        public Dictionary<Location, HashSet<int>> Options
        {
            get
            {
                return this.options;
            }
        }
    }
}
