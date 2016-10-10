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
    }
}
