namespace SudokuSolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class SudokuSolver
    {
        private OptionTable options;

        public SudokuSolver(SudokuGame problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException("problem");
            }

            this.options = new OptionTable(problem);
        }

        public OptionTable OptionsForUnitTest
        {
            get
            {
                return this.options;
            }
        }

        public bool Solve()
        {
            do
            {
                this.options.ValueSet = false;
                this.options.EliminateIntersection();
                this.SetCellsWithSingleOption();
                this.SetValueWithOnlyOneCellAvailable();
            }
            while (this.options.ValueSet);
            return this.options.Solved;
        }

        public void SetCellsWithSingleOption()
        {
            List<SolutionCandidate> solutionCandidates = new List<SolutionCandidate>();
            foreach (Location location in Location.Locations)
            {
                if (this.options.IsEmpty(location))
                {
                    int singleOption;
                    if (this.options.TryGetSingleOption(location, out singleOption))
                    {
                        solutionCandidates.Add(new SolutionCandidate(location, singleOption));
                    }
                }
            }

            foreach (SolutionCandidate solutionCandidate in solutionCandidates)
            {
                this.options.SetValue(solutionCandidate.Location, solutionCandidate.Value);
            }
        }

        public void SetValueWithOnlyOneCellAvailable()
        {
            List<SolutionCandidate> solutionCandidates = new List<SolutionCandidate>();
            foreach (Box box in Box.Boxes)
            {
                for (int value = 1; value <= 9; value++)
                {
                    Location foundLocation = null;
                    bool foundTwoLocations = false;
                    foreach (Location location in box.Locations)
                    {
                        if (!this.options.IsFilled(location))
                        {
                            if (this.options.IsOptionAvailable(location, value))
                            {
                                if (foundLocation == null)
                                {
                                    foundLocation = location;
                                }
                                else
                                {
                                    foundTwoLocations = true;
                                    break;
                                }
                            }
                        }
                    }

                    if (foundLocation != null)
                    {
                        if (!foundTwoLocations)
                        {
                            solutionCandidates.Add(new SolutionCandidate(foundLocation, value));
                        }
                    }
                }
            }

            // TODO, add feature, check if solutionCandidates contradicts.
            foreach (SolutionCandidate solutionCandidate in solutionCandidates)
            {
                if (!this.options.IsFilled(solutionCandidate.Location))
                {
                    this.options.SetValue(solutionCandidate.Location, solutionCandidate.Value);
                }
            }
        }

        public bool Check()
        {
            foreach (Box box in Box.Boxes)
            {
                if (box.ContainsDuplicate(this.options.Problem))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return this.options.ToString();
        }
    }
}
