using System;
using NUnit.Framework;

namespace SudokuSolver.Tests
{
    [TestFixture]
    public class SudokuSolverTest
    {
        private SudokuSolver _solver;

        [SetUp]
        public void SetUp()
        {
            _solver = new SudokuSolver();
        }

        [Test]
        public void CreationTest()
        {
            _solver.ToString();

            Assert.That(_solver.ToString(), Is.EqualTo("[SudokuSolver]"));
        }

        [Test]
        public void Sudoku2x2Solver()
        {
            var solver = new SudokuSolver();

            var problem = new Square(3);
            problem.Structure[0, 0] = 2;
            problem.Structure[0, 1] = 8;
            problem.Structure[0, 7] = 4;
            problem.Structure[1, 0] = 4;
            problem.Structure[1, 5] = 5;
            problem.Structure[2, 0] = 9;
            problem.Structure[2, 1] = 6;
            problem.Structure[2, 2] = 7;
            problem.Structure[2, 5] = 3;
            problem.Structure[2, 6] = 2;
            problem.Structure[3, 4] = 6;
            problem.Structure[3, 5] = 2;
            problem.Structure[3, 6] = 7;
            problem.Structure[3, 7] = 8;
            problem.Structure[4, 3] = 7;
            problem.Structure[4, 7] = 2;
            problem.Structure[5, 0] = 7;
            problem.Structure[5, 3] = 9;
            problem.Structure[5, 8] = 4;
            problem.Structure[6, 4] = 1;
            problem.Structure[6, 8] = 5;
            problem.Structure[7, 4] = 5;
            problem.Structure[8, 0] = 1;
            problem.Structure[8, 4] = 2;
            problem.Structure[8, 7] = 9;
            problem.Structure[8, 8] = 3;

            var result = solver.Solve(problem);

            if (result.Solved)
                Console.WriteLine(result.Square.ToString());
            else
            {
                Console.WriteLine("Not solved");
            }
        }
    }
}
