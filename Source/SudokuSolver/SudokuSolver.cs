namespace SudokuSolver
{
    public class SudokuSolver
    {
        private uint _squareSize;

        public SudokuSolver(uint squareSize)
        {
            _squareSize = squareSize;
        }

        public Solution Solve(Square problem)
        {
            
        }

        public override string ToString()
        {
            return "SudokuSolver (squareSize=" + _squareSize + ")";
        }
    }
}
