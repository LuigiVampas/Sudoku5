using System.Collections.Generic;
using Microsoft.Z3;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        private Context _context;
        private BoolExpr _solution;

        public SudokuSolver()
        {
            _context = new Context();
        }

        public Solution Solve(Square problem)
        {
            var givenData = Parse(problem);

            InitializeSolution();

            var bigSquareSize = problem.SquareSize * problem.SquareSize;

            var intVariables = InitializeVariables(givenData, bigSquareSize);

            AddRowRules(intVariables, bigSquareSize);
            AddColumnRules(intVariables, bigSquareSize);
            AddSquareRules(intVariables, problem.SquareSize);

            return new Solution();
        }

        private Dictionary<Coordinate, int> Parse(Square problem)
        {
            var result = new Dictionary<Coordinate, int>();

            for (uint x = 0; x < problem.SquareSize; ++x)
                for (uint y = 0; y < problem.SquareSize; ++y)
                {
                    if (problem.Structure[x, y] >= 0)
                        result[new Coordinate { X = x, Y = y }] = problem.Structure[x, y];
                }

            return result;
        }

        private IntExpr[,] InitializeVariables(Dictionary<Coordinate, int> givenData, uint bigSquareSize)
        {
            var intVariables = new IntExpr[bigSquareSize, bigSquareSize];

            for (uint x = 0; x < bigSquareSize; ++x)
                for (uint y = 0; y < bigSquareSize; ++y)
                {
                    var variable = AddIntVariable(x + " " + y, bigSquareSize);

                    var coordinate = new Coordinate { X = x, Y = y };

                    if (givenData.ContainsKey(coordinate))
                        AddValue(variable, givenData[coordinate]);

                    intVariables[x,y] = variable;
                }

            return intVariables;
        }

        private void InitializeSolution()
        {
            _solution = _context.MkTrue();
        }

        private void AddValue(IntExpr variable, int value)
        {
            var newValueExpr = _context.MkInt(value);

            AddRule(_context.MkEq(variable, newValueExpr));
        }

        private IntExpr AddIntVariable(string name, uint bigSqaureSize)
        {
            var intConst = _context.MkIntConst(name);

            AddRule(CreateIntervalFor(intConst, 1, bigSqaureSize));

            return intConst;
        }

        private BoolExpr CreateIntervalFor(IntExpr intConst, uint min, uint max)
        {
            var minConst = _context.MkInt(min);
            var maxConst = _context.MkInt(max);

            var leExpr = _context.MkLe(intConst, maxConst);
            var geExpr = _context.MkGe(minConst, intConst);

            return _context.MkAnd(leExpr, geExpr);
        }

        private void AddRowRules(IntExpr[,] variables, uint bigSquareSize)
        {
            for (var rowNumber = 0; rowNumber < bigSquareSize; ++rowNumber)
                AddElementNonEqualityRuleForRow(variables, rowNumber, bigSquareSize);
        }

        private void AddElementNonEqualityRuleForRow(IntExpr[,] variables, int rowNumber, uint bigSquareSize)
        {
            var elements = new IntExpr[bigSquareSize];

            for (var elementIndex = 0; elementIndex < bigSquareSize; ++elementIndex)
                elements[elementIndex] = variables[rowNumber, elementIndex];

            AddNonEqualityRule(elements);
        }

        private void AddColumnRules(IntExpr[,] variables, uint bigSquareSize)
        {
            
        }

        private void AddSquareRules(IntExpr[,] variables, uint squareSize)
        {
            
        }

        private void AddNonEqualityRule(IntExpr[] variables)
        {
            for (var firstVariableIndex = 0; firstVariableIndex < variables.Length; ++firstVariableIndex)
                for (var secondVariableIndex = firstVariableIndex + 1;
                    secondVariableIndex < variables.Length;
                    ++secondVariableIndex)
                {
                    var firstVariable = variables[firstVariableIndex];
                    var secondVariable = variables[secondVariableIndex];

                    AddRule(_context.MkNot(_context.MkEq(firstVariable, secondVariable)));
                }
        }

        private void AddRule(BoolExpr rule)
        {
            _solution = _context.MkAnd(_solution, rule);
        }

        public override string ToString()
        {
            return "[SudokuSolver]";
        }
    }
}
