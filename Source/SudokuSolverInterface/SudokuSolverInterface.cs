using System;
using System.Windows.Forms;

namespace SudokuSolverInterface
{
    static class SudokuSolverInterface
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
