using System;
using System.Windows.Forms;
using SudokuSolver;

namespace SudokuSolverInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetSettings();
        }

        private void SetSettings()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void MakeGridNotSortable()
        {
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView1.Columns)
            {
                dataGridViewColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void MakeCellsASquare()
        {
            foreach (DataGridViewRow dataGridViewColumn in dataGridView1.Rows)
            {
                dataGridViewColumn.Height = dataGridView1.Columns[0].Width;
            }
        }

        private void CreateRowsAndColumnsInGrid(uint size)
        {
            for (var i = 0; i < size; ++i)
            {
                dataGridView1.Columns.Add("", "");
                dataGridView1.Rows.Add();
            }
        }

        private void create_field_Click(object sender, EventArgs e)
        {
            SetSettings();
            var size = GetSize();
            CreateRowsAndColumnsInGrid(size * size);
            MakeGridNotSortable();
            MakeCellsASquare();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && e.KeyChar != 8)
                e.Handled = true;
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress += Cell_KeyPress;
        }

        private void Cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
                e.KeyChar = Convert.ToChar("\0");
        }

        private void _info_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для того, чтобы решить судоку необходимо: \n" +
                            "1) Выставить размеры поля судоку (количество больших квадратов в строке \n" +
                            "2) Нажать клавишу \"Сгенерировать поле\" - появится поле для заполнения \n" +
                            "3) Заполнить поле исходными данными \n" +
                            "4) Нажать клавишу \"Решить\" - появится решение заданного судоку \n");
        }

        private uint GetSize()
        {
            try
            {
                return uint.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректные данные! Введите значение > 0");
            }

            return 0;
        }

        private void FillGrid(Solution result)
        {
            for (var i = 0; i < dataGridView1.RowCount; ++i)
            {
                for (var j = 0; j < dataGridView1.ColumnCount; ++j)
                {
                    dataGridView1.Rows[i].Cells[j].Value = result.Square.Structure[i, j];
                }
            }
        }

        private Square FillProblem(Square problem, uint size)
        {
            for (var i = 0; i < dataGridView1.RowCount; i++)
            {
                for (var j = 0; j < dataGridView1.ColumnCount; ++j)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        continue;
                    var value = Convert.ToInt32(dataGridView1.Rows[i].Cells[j].Value);

                    if (value < size * size && value > 0)
                        problem.Structure[i, j] = value;
                    else
                    {
                        SetSettings();
                        throw new ArgumentException("\nНеверный формат значения: ",
                            "Ячейка: " + i.ToString() + " " + j.ToString() + " " +
                            "\nЗначение: " + dataGridView1.Rows[i].Cells[j].Value.ToString());
                    }
                }
            }

            return problem;
        }

        private void _solve_Click(object sender, EventArgs e)
        {
            try
            {
                var size = GetSize();
                textBox1.Clear();
                var solver = new SudokuSolver.SudokuSolver();
                var problem = new Square(size);
                problem = FillProblem(problem, size);
 
                var result = solver.Solve(problem);
                if (result.Solved)
                    FillGrid(result);
                else
                {
                    MessageBox.Show("Not solved!!");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Необработанное исключение: " + exception.Message);
            }
        }
    }
}