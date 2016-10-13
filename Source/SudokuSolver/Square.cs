using System.Text;

namespace SudokuSolver
{
    public class Square
    {
        public Square(uint squareSize)
        {
            SquareSize = squareSize;

            var doubleSquareSize = SquareSize*SquareSize;
            Structure = new int[doubleSquareSize, doubleSquareSize];
            
            for (var i = 0; i < doubleSquareSize; ++i)
                for (var j = 0; j < doubleSquareSize; ++j)
                    Structure[i, j] = -1;
        }

        public uint SquareSize { get; private set; }

        public int[,] Structure { get; private set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var squareX = 0; squareX < SquareSize; ++squareX)
            {
                for (var x = 0; x < SquareSize; ++x)
                {
                    sb.Append("|");

                    for (var squareY = 0; squareY < SquareSize; ++squareY)
                    {
                        for (var y = 0; y < SquareSize; ++y)
                            sb.Append(Structure[x + squareX * SquareSize, y + squareY * SquareSize]);

                        sb.Append("|");
                    }

                    sb.AppendLine();
                }

                for (var y = 0; y < SquareSize * SquareSize + SquareSize - 1; ++y)
                    sb.Append("-");

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}