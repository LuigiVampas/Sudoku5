using System.Drawing;
using System.Windows.Forms;

namespace SudokuSolverInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_Paint_1(object sender, PaintEventArgs e)
        {
            Point currentPoint = new Point(0, 0);
            Size size = new Size(dataGridView1.Width / 3,
                                     dataGridView1.Height / 3);
            Pen myPen = new Pen(Color.Red, 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    currentPoint.X = i * dataGridView1.Width / 3;
                    currentPoint.Y = j * dataGridView1.Height / 3;
                    Rectangle rect = new Rectangle(currentPoint, size);
                    e.Graphics.DrawRectangle(myPen, rect);
                }
            }
        }
    }
}