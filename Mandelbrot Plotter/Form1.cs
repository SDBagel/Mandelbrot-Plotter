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

        private void GenerateAndDisplayValues(double real, double imaginary, bool clearExisting = false)
        {
            double originalreal = real;
            double originalimaginary = imaginary;

            Random rnd = new Random();
            Color color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

            if (clearExisting)
            {
                richTextBox1.Text = "";
                GraphPlane.Controls.Clear();
            }
                
            GraphPoint(real, imaginary, (double)ScaleUpDown.Value, color);
            richTextBox1.Text += $"Z[1] = {real} + {imaginary}i \n";
            for (int i = 1; i < numericUpDown1.Value; i++)
            {
                double newreal = (real * real) - (imaginary * imaginary) + originalreal;
                double newimaginary = 2 * (real * imaginary) + originalimaginary;

                richTextBox1.Text += $"Z[{i + 1}] = {real} + {imaginary}i \n";
                GraphPoint(newreal, newimaginary, (double)ScaleUpDown.Value, color);

                real = newreal;
                imaginary = newimaginary;
            }

            CreateAxis((int)ScaleUpDown.Value);
        }

        private void GraphPoint(double x, double y, double scale, Color ptColor)
        {
            Point origin = new Point(GraphPlane.Width / 2, GraphPlane.Height / 2);

            var newpt = new PictureBox
            {
                BackColor = ptColor,
                Location = new Point(origin.X + (int)Math.Round(x*scale) - 2, origin.Y - (int)Math.Round(y*scale) - 2),
                Size = new Size(4, 4)
            };

            GraphPlane.Controls.Add(newpt);
        }

        private void CreateAxis(double scale)
        {
            var xAx = new PictureBox
            {
                BackColor = Color.Black,
                Location = new Point(0, GraphPlane.Height / 2),
                Size = new Size(GraphPlane.Width, 1)
            };

            var yAx = new PictureBox
            {
                BackColor = Color.Black,
                Location = new Point(GraphPlane.Width / 2, 0),
                Size = new Size(1, GraphPlane.Height)
            };

            GraphPlane.Controls.Add(xAx);
            GraphPlane.Controls.Add(yAx);

            Point origin = new Point(GraphPlane.Width / 2, GraphPlane.Height / 2);

            var oMarker = new AxisMarker
            {
                Location = new Point(origin.X - 28, origin.Y)
            };
            oMarker.label1.Text = "0";
            GraphPlane.Controls.Add(oMarker);

            var xMarker = new AxisMarker
            {
                Location = new Point(GraphPlane.Width/2 - 28 + GraphPlane.Width/4, (GraphPlane.Height / 2))
            };

            string val = $"{(xMarker.Location.X - GraphPlane.Width / 2 + 28) / scale}";
            if (val.Length > 4)
                val = $"{(xMarker.Location.X - GraphPlane.Width / 2 + 28) / scale}".Substring(0, 4);
            xMarker.label1.Text = val;
            GraphPlane.Controls.Add(xMarker);

            xMarker = new AxisMarker
            {
                Location = new Point(GraphPlane.Width / 2 - 28 - GraphPlane.Width / 4, (GraphPlane.Height / 2))
            };

            val = $"{(xMarker.Location.X - GraphPlane.Width / 2 + 28) / scale}";
            if (val.Length > 5)
                val = $"{(xMarker.Location.X - GraphPlane.Width / 2 + 28) / scale}".Substring(0, 5);
            xMarker.label1.Text = val;
            GraphPlane.Controls.Add(xMarker);

            var yMarker = new AxisMarker
            {
                Location = new Point(GraphPlane.Width / 2 - 28, (GraphPlane.Height / 2) - (GraphPlane.Height / 4))
            };

            val = $"{(-yMarker.Location.Y + GraphPlane.Height / 2) / scale}";
            if (val.Length > 4)
                val = $"{(-yMarker.Location.Y + GraphPlane.Height / 2) / scale}".Substring(0, 5);
            yMarker.label1.Text = val;
            GraphPlane.Controls.Add(yMarker);

            yMarker = new AxisMarker
            {
                Location = new Point(GraphPlane.Width / 2 - 28, (GraphPlane.Height / 2) + (GraphPlane.Height / 4))
            };

            val = $"{-(yMarker.Location.Y - GraphPlane.Height / 2) / scale}";
            if (val.Length > 4)
                val = $"{-(yMarker.Location.Y - GraphPlane.Height / 2) / scale}".Substring(0, 5);
            yMarker.label1.Text = val;
            GraphPlane.Controls.Add(yMarker);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string uI = textBox1.Text.TrimEnd('i');
                double real = double.Parse(uI.Split('+')[0].Trim());
                double imaginary = double.Parse(uI.Split('+')[1].Trim());

                GenerateAndDisplayValues(real, imaginary, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string uI = textBox1.Text.TrimEnd('i');
                double real = double.Parse(uI.Split('+')[0].Trim());
                double imaginary = double.Parse(uI.Split('+')[1].Trim());
                
                GenerateAndDisplayValues(real, imaginary);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateAxis((int)ScaleUpDown.Value);
        }
    }
}
