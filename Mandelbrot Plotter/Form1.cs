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

        private void GraphPoint(double x, double y, double scale, Color ptColor)
        {
            var newpt = new PictureBox
            {
                BackColor = ptColor,
                Location = new Point((int)Math.Round(GraphPlane.Width / 2 + x*scale-2.0), (int)Math.Round(GraphPlane.Height / 2 - y*scale-2)),
                Size = new Size(4, 4)
            };

            GraphPlane.Controls.Add(newpt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string uI = textBox1.Text.TrimEnd('i');
                double real = double.Parse(uI.Split('+')[0].Trim());
                double imaginary = double.Parse(uI.Split('+')[1].Trim());

                double originalreal = real;
                double originalimaginary = imaginary;

                richTextBox1.Text = "";

                Random rnd = new Random();
                Color color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

                GraphPlane.Controls.Clear();
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    double newreal = (real * real) - (imaginary * imaginary) + originalreal;
                    double newimaginary = 2 * (real * imaginary) + originalimaginary;

                    richTextBox1.Text += $"Z[{i + 1}] = {real} + {imaginary}i \n";
                    GraphPoint(newreal, newimaginary, (double)numericUpDown2.Value, color);

                    real = newreal;
                    imaginary = newimaginary;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string uI = textBox1.Text.TrimEnd('i');
                double real = double.Parse(uI.Split('+')[0].Trim());
                double imaginary = double.Parse(uI.Split('+')[1].Trim());

                double originalreal = real;
                double originalimaginary = imaginary;

                Random rnd = new Random();
                Color color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    double newreal = (real * real) - (imaginary * imaginary) + originalreal;
                    double newimaginary = 2 * (real * imaginary) + originalimaginary;

                    richTextBox1.Text += $"X[{i + 1}] = {real} + {imaginary}i \n";
                    GraphPoint(newreal, newimaginary, (double)numericUpDown2.Value, color);

                    real = newreal;
                    imaginary = newimaginary;
                }
            }
        }
    }
}
