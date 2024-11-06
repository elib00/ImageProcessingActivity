using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingActivity
{
    public partial class Form2 : Form
    {
        Bitmap imageA, imageB, resultImage;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = imageB;
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = imageA;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resultImage = new Bitmap(imageA.Width, imageA.Height);
            Color myGreen = Color.FromArgb(0, 255, 0);
            int grayGreen = (int)((myGreen.R + myGreen.G + myGreen.B) / 3);
            int threshold = 5;

            for (int y = 0; y < imageB.Height; y++)
            {
                for (int x = 0; x < imageB.Width; x++)
                {
                    Color pixel = imageB.GetPixel(x, y);
                    Color backPixel = imageA.GetPixel(x, y);
                    int gray = (int)((pixel.R + pixel.G + pixel.B) / 3);
                    int subtractValue = Math.Abs(gray - grayGreen);
                    if (subtractValue < threshold)
                    {
                        resultImage.SetPixel(x, y, backPixel);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
;
                }
            }

            pictureBox3.Image = resultImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }
    }
}
