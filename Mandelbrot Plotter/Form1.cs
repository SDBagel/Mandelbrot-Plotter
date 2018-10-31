using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mandelbrot_Plotter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GraphPoint(int x, int y, int scale)
        {
            var newpt = new PictureBox
            {
                BackColor = Color.Orange,
                Location = new Point((panel1.Width / 2 + x*scale)-2, (panel1.Height / 2 - y*scale)-2),
                Size = new Size(4, 4)
            };

            panel1.Controls.Add(newpt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uI = textBox1.Text;
            int real = int.Parse(uI.Split('+')[0].Trim());
            int imaginary = int.Parse(uI.Split('+')[1].Trim());

            int originalreal = real;
            int originalimaginary = imaginary;

            richTextBox1.Text += $"Z[1] = {real} + {imaginary}i \n";

            panel1.Controls.Clear();
            GraphPoint(real, imaginary, (int)numericUpDown2.Value);
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                int newreal = real ^ 2 - imaginary ^ 2 + originalreal;
                int newimaginary = 2 * (real * imaginary) + originalimaginary;

                GraphPoint(newreal, newimaginary, (int)numericUpDown2.Value);

                real = newreal;
                imaginary = newimaginary;
            }
        }
    }
}
