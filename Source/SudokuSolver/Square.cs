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

        public void PutInto(uint i, uint j, int value)
        {
            if (i >= SquareSize*SquareSize)
                throw new ArgumentOutOfRangeException("i", "Wrong index");

            if (j >= SquareSize*SquareSize)
                throw new ArgumentOutOfRangeException("j", "Wrong index");

            Structure[i, j] = value;
        }
    }
}