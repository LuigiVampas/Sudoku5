using System;

namespace SudokuSolver
{
    public class Square
    {
        public Square(uint squareSize)
        {
            SquareSize = squareSize;
            Structure = new int[SquareSize*SquareSize, SquareSize*SquareSize];
        }

        public uint SquareSize { get; private set; }

        public int[,] Structure { get; private set; }
    }
}