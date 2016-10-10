using System;
using System.Collections.Generic;
using Microsoft.Z3;

namespace SudokuSolver
{
    public class SudokuSolver
    {
        private Context _context;
        private BoolExpr _solution;
        private IntExpr[,] _variables;
        private uint _bigSquareSize;
        private uint _smallSquareSize;

        public SudokuSolver()
        {
            _context = new Context();
        }

        public Solution Solve(Square problem)
        {
            InitializeSquareSizes(problem);
            InitializeSolution();

            var givenData = Parse(problem);
            InitializeVariables(givenData);

            AddRowRules();
            AddColumnRules();
            AddSquareRules();

            return FormSolution();
        }

        private void InitializeSquareSizes(Square problem)
        {
            _smallSquareSize = problem.SquareSize;
            _bigSquareSize = _smallSquareSize * _smallSquareSize;
        }
        
        private void InitializeSolution()
        {
            _solution = _context.MkTrue();
        }

        private Dictionary<Coordinate, int> Parse(Square problem)
        {
            var result = new Dictionary<Coordinate, int>();

            for (uint x = 0; x < _bigSquareSize; ++x)
                for (uint y = 0; y < _bigSquareSize; ++y)
                {
                    if (problem.Structure[x, y] >= 0)
                        result[new Coordinate { X = x, Y = y }] = problem.Structure[x, y];
                }

            return result;
        }

        private void InitializeVariables(Dictionary<Coordinate, int> givenData)
        {
            _variables = new IntExpr[_bigSquareSize, _bigSquareSize];

            for (uint x = 0; x < _bigSquareSize; ++x)
                for (uint y = 0; y < _bigSquareSize; ++y)
                {
                    var variable = AddIntVariable(x + " " + y);

                    var coordinate = new Coordinate { X = x, Y = y };

                    if (givenData.ContainsKey(coordinate))
                        AddValue(variable, givenData[coordinate]);

                    _variables[x, y] = variable;
                }
        }

        private void AddValue(IntExpr variable, int value)
        {
            var newValueExpr = _context.MkInt(value);

            AddRule(_context.MkEq(variable, newValueExpr));
        }

        private IntExpr AddIntVariable(string name)
        {
            var intConst = _context.MkIntConst(name);

            AddRule(CreateIntervalFor(intConst, 1, _bigSquareSize));

            return intConst;
        }

        private BoolExpr CreateIntervalFor(IntExpr intConst, uint min, uint max)
        {
            var minConst = _context.MkInt(min);
            var maxConst = _context.MkInt(max);

            var leExpr = _context.MkLe(intConst, maxConst);
            var geExpr = _context.MkGe(intConst, minConst);

            return _context.MkAnd(leExpr, geExpr);
        }

        private void AddRowRules()
        {
            for (var rowNumber = 0; rowNumber < _bigSquareSize; ++rowNumber)
                AddElementNonEqualityRuleForRow(rowNumber);
        }

        private void AddElementNonEqualityRuleForRow(int rowNumber)
        {
            var elements = new IntExpr[_bigSquareSize];

            for (var elementIndex = 0; elementIndex < _bigSquareSize; ++elementIndex)
                elements[elementIndex] = _variables[rowNumber, elementIndex];

            AddNonEqualityRule(elements);
        }

        private void AddColumnRules()
        {
            for (var columnNumber = 0; columnNumber < _bigSquareSize; ++columnNumber)
                AddElementNonEqualityRuleForColumn(columnNumber);
        }

        private void AddElementNonEqualityRuleForColumn(int columnNumber)
        {
            var elements = new IntExpr[_bigSquareSize];

            for (var elementIndex = 0; elementIndex < _bigSquareSize; ++elementIndex)
                elements[elementIndex] = _variables[elementIndex, columnNumber];

            AddNonEqualityRule(elements);
        }

        private void AddSquareRules()
        {
            for (var squareX = 0; squareX < _smallSquareSize; ++squareX)
                for (var squareY = 0; squareY < _smallSquareSize; ++squareY)
                    AddElementNonEqualityRuleForSquare(squareX, squareY);
        }

        private void AddElementNonEqualityRuleForSquare(int squareX, int squareY)
        {
            var elements = new IntExpr[_bigSquareSize];

            for (var x = 0; x < _smallSquareSize; ++x)
                for (var y = 0; y < _smallSquareSize; ++y)
                    elements[x * _smallSquareSize + y] = _variables[squareX * _smallSquareSize + x, squareY * _smallSquareSize + y];

            AddNonEqualityRule(elements);
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

        private Solution FormSolution()
        {
            var solution = new Solution();

            var solver = _context.MkSolver("QF_LIA");

            solver.Assert(_solution);

            if (solver.Check() == Status.SATISFIABLE)
            {
                solution.Solved = true;

                solution.Square = CreateSolution(solver);

                return solution;
            }

            solution.Solved = false;
            solution.Square = null;

            return solution;
        }

        private Square CreateSolution(Solver solver)
        {
            var result = new Square(_smallSquareSize);

            for (var x = 0; x < _bigSquareSize; ++x)
                for (var y = 0; y < +_bigSquareSize; ++y)
                {
                    result.Structure[x, y] = Convert.ToInt32(solver.Model.ConstInterp(_variables[x, y]).ToString());
                }

            return result;
        }

        public override string ToString()
        {
            return "[SudokuSolver]";
        }
    }
}
