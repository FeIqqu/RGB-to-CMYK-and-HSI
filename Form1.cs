using System;
using System.Drawing;
using System.Windows.Forms;

namespace RGB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void labelRGBToHex()
        {
            try
            {
                int red = int.Parse(textBox1.Text);
                int green = int.Parse(textBox2.Text);
                int blue = int.Parse(textBox3.Text);

                if (red < 0 || red > 255 || green < 0 || green > 255 || blue < 0 || blue > 255)
                {
                    return;
                }

                label3.Text = $"#{red:X2}{green:X2}{blue:X2}";

                panel8.BackColor = Color.FromArgb(red, green, blue);

                trackBar1.Value = red;
                trackBar2.Value = green;
                trackBar3.Value = blue;
            }
            catch (FormatException) { }
        }

        private void labelRGBToCMYKandHSI()
        {
            try
            {
                int red = int.Parse(textBox1.Text);
                int green = int.Parse(textBox2.Text);
                int blue = int.Parse(textBox3.Text);

                if (red < 0 || red > 255 || green < 0 || green > 255 || blue < 0 || blue > 255)
                {
                    MessageBox.Show("Значения RGB должны быть в диапазоне от 0 до 255.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // CMYK
                double rPrime = red / 255.0;
                double gPrime = green / 255.0;
                double bPrime = blue / 255.0;

                double K = 1 - Math.Max(Math.Max(rPrime, gPrime), bPrime);

                double C = (K < 1) ? (1 - rPrime - K) / (1 - K) : 0;
                double M = (K < 1) ? (1 - gPrime - K) / (1 - K) : 0;
                double Y = (K < 1) ? (1 - bPrime - K) / (1 - K) : 0;

                textBox6.Text = Math.Round(C * 100).ToString();
                textBox5.Text = Math.Round(M * 100).ToString();
                textBox4.Text = Math.Round(Y * 100).ToString();
                textBox7.Text = Math.Round(K * 100).ToString();

                // HSI
                double H, S, I;

                if (red + green + blue == 0)
                {
                    H = 0;
                    S = 0;
                    I = 0;
                }
                else
                {
                    int _M = Math.Min(Math.Min(red, green), blue);


                    if (red == green && green == blue) H = 0;
                    else
                    {
                        double A = Math.Acos((red - (green + blue) / 2) / Math.Sqrt(red * red + green * green + blue * blue - red * green - red * blue - green * blue));
                        H = (blue <= green ? A : 2 * Math.PI - A);
                    }

                    S = (double)100 * (1 - ((3 * _M) / (double)(red + green + blue)));
                    I = ((double)100 / 255) * ((red + green + blue) / (double)3);
                }

                textBox10.Text = Math.Round((H * 180 / Math.PI), 2).ToString();
                textBox9.Text = Math.Round(S, 2).ToString();
                textBox8.Text = Math.Round(I, 2).ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные числовые значения для RGB.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            labelRGBToHex();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            labelRGBToHex();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            labelRGBToHex();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelRGBToCMYKandHSI();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelRGBToHex();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = trackBar2.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox3.Text = trackBar3.Value.ToString();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.Cancel)
                return;

            textBox1.Text = colorDialog.Color.R.ToString();
            textBox2.Text = colorDialog.Color.G.ToString();
            textBox3.Text = colorDialog.Color.B.ToString();
        }
    }
}
