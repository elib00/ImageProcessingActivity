using ImageProcess2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;

namespace ImageProcessingActivity
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        Device[] devices;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            devices = DeviceManager.GetAllDevices();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //processed.Save(saveFileDialog1.FileName);
            string filePath = saveFileDialog1.FileName;

            switch (saveFileDialog1.FilterIndex)
            {
                case 1:
                    processed.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case 2:
                    processed.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                default:
                    processed.Save(filePath);
                    break;
            }
        }

        private void pixelCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    pixel = loaded.GetPixel(x, y);
                    processed.SetPixel(x, y, pixel);
                }
            }

            pictureBox2.Image = processed;
        }

        private void inversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    pixel = loaded.GetPixel(x, y);
                    Color inverted = Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B);
                    processed.SetPixel(x, y, inverted);
                }
            }

            pictureBox2.Image = processed;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CS-50
            //sepia algorithm 
            //https://cs50.harvard.edu/college/2019/fall/psets/4/filter/less/#:~:text=Sepia,original%20values%20of%20the%20three.

            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    pixel = loaded.GetPixel(x, y);  
                    int sepiaRed = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int sepiaGreen = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int sepiaBlue = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);
                    Color sepia = Color.FromArgb(Math.Min(sepiaRed, 255), Math.Min(sepiaGreen, 255), Math.Min(sepiaBlue, 255)); //if mulapas og 255
                    processed.SetPixel(x, y, sepia);
                }
            }

            pictureBox2.Image = processed;

        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasicDIP.Histogram(ref loaded, ref processed);
            pictureBox2.Image = processed;

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            BasicDIP.Brightness(ref loaded, ref processed, trackBar1.Value);
            pictureBox2.Image = processed;
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            devices[0].ShowWindow(pictureBox1);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
        {
            devices[0].Stop();
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 subtractionForm = new Form2();
            subtractionForm.Show();
        }

        private void smoothToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);
            BitmapFilter.Smooth(processed, 1);
            pictureBox2.Image = processed;
        }

        private void sharpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);
            BitmapFilter.Sharpen(processed, 11);
            pictureBox2.Image = processed; 
        }

        private void gaussianBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);
            BitmapFilter.GaussianBlur(processed, 6);
            pictureBox2.Image = processed;
        }

        private void edgeDetectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded);
            BitmapFilter.EdgeDetectQuick(processed);
            pictureBox2.Image = processed;
        }

        private void embossLaplascianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossLaplacian(processed);
            pictureBox2.Image = processed;
        }

        private void embossHorizontalVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossHorizontalVertical(processed);
            pictureBox2.Image = processed;
        }

        private void embossAllDirectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossAllDirections(processed);
            pictureBox2.Image = processed;
        }

        private void embossLossyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossLossy(processed);
            pictureBox2.Image = processed;
        }

        private void embossHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossHorizontal(processed);
            pictureBox2.Image = processed;
        }

        private void embossVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loaded == null) return;
            processed = new Bitmap(loaded);
            BitmapFilter.EmbossVertical(processed);
            pictureBox2.Image = processed;
        }

        private void grayscalingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            Color pixel;
            int average;
            for (int y = 0; y < loaded.Height; y++)
            {
                for (int x = 0; x < loaded.Width; x++)
                {
                    pixel = loaded.GetPixel(x, y);
                    average = (int) (pixel.R + pixel.G + pixel.B) / 3;
                    Color gray = Color.FromArgb(average, average, average);
                    processed.SetPixel(x, y, gray);
                }
            }

            pictureBox2.Image = processed;
        }
    }
}
