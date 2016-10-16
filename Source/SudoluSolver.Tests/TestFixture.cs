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

<<<<<<< HEAD
            var problem = new Square(3)
            {
                Structure =
                {
                    [1, 3] = 7,
                    [1, 5] = 1,
                    [1, 8] = 2,
                    [2, 2] = 9,
                    [2, 7] = 4,
                    [3, 0] = 8,
                    [3, 1] = 1,
                    [3, 5] = 7,
                    [3, 8] = 4,
                    [4, 3] = 2,
                    [4, 4] = 4,
                    [4, 6] = 3,
                    [5, 0] = 4,
                    [5, 5] = 5,
                    [5, 6] = 9,
                    [6, 1] = 7,
                    [6, 3] = 3,
                    [7, 0] = 5,
                    [7, 8] = 8,
                    [8, 7] = 6,
                    [8, 1] = 8,
                    [8, 4] = 2,
                    [8, 5] = 9,
                    [8, 7] = 6
                }
            };
=======
            var problem = new Square(3);

            problem.Structure[1, 3] = 7;
            problem.Structure[1, 5] = 1;
            problem.Structure[1, 8] = 2;
            problem.Structure[2, 2] = 9;
            problem.Structure[2, 7] = 4;
            problem.Structure[3, 0] = 8;
            problem.Structure[3, 1] = 1;
            problem.Structure[3, 5] = 7;
            problem.Structure[3, 8] = 4;
            problem.Structure[4, 3] = 2;
            problem.Structure[4, 4] = 4;
            problem.Structure[4, 6] = 3;
            problem.Structure[5, 0] = 4;
            problem.Structure[5, 5] = 5;
            problem.Structure[5, 6] = 9;
            problem.Structure[6, 1] = 7;
            problem.Structure[6, 3] = 3;
            problem.Structure[7, 0] = 5;
            problem.Structure[7, 8] = 8;
            problem.Structure[8, 1] = 8;
            problem.Structure[8, 4] = 2;
            problem.Structure[8, 5] = 9;
            problem.Structure[8, 7] = 6;
>>>>>>> 989b185c206ba49ad554a6e1e9d8b202469c3de8

            var result = solver.Solve(problem);

            Console.WriteLine(result.Solved ? result.Square.ToString() : "Not solved");
        }
    }
}
