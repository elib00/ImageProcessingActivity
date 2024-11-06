using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessingActivity
{
    static class BasicDIP
    {

        public static void Equalisation(ref Bitmap a, ref Bitmap b, int degree)
        {
            int height = a.Height;
            int width = a.Width;
            int numSamples, histSum;
            int[] Ymap = new int[256];
            int[] hist = new int[256];
            int percent = degree;
            // compute the histogram from the sub-image
            Color nakuha;
            Color gray;
            Byte graydata;
            //compute greyscale
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    nakuha = a.GetPixel(x, y);
                    graydata = (byte)((nakuha.R + nakuha.G + nakuha.B) / 3);
                    gray = Color.FromArgb(graydata, graydata, graydata);
                    a.SetPixel(x, y, gray);
                }
            }
            //histogram 1d data;
            for (int x = 0; x < a.Width; x++)
            {
                for (int y = 0; y < a.Height; y++)
                {
                    nakuha = a.GetPixel(x, y);
                    hist[nakuha.B]++;

                }
            }
            // remap the Ys, use the maximum contrast (percent == 100) 
            // based on histogram equalization
            numSamples = (a.Width * a.Height);   // # of samples that contributed to the histogram
            histSum = 0;
            for (int h = 0; h < 256; h++)
            {
                histSum += hist[h];
                Ymap[h] = histSum * 255 / numSamples;
            }

            // if desired contrast is not maximum (percent < 100), then adjust the mapping
            if (percent < 100)
            {
                for (int h = 0; h < 256; h++)
                {
                    Ymap[h] = h + ((int)Ymap[h] - h) * percent / 100;
                }
            }

            b = new Bitmap(a.Width, a.Height);
            // enhance the region by remapping the intensities
            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    // set the new value of the gray value
                    Color temp = Color.FromArgb(Ymap[a.GetPixel(x, y).R], Ymap[a.GetPixel(x, y).G], Ymap[a.GetPixel(x, y).B]);
                    b.SetPixel(x, y, temp);
                }

            }
        }

        public static void Brightness(ref Bitmap a, ref Bitmap b, int value)
        {
            Color pixel;
            b = new Bitmap(a.Width, a.Height);

            for (int y = 0; y < a.Height; y++)
            {
                for(int x = 0; x < a.Width; x++)
                {
                    pixel = a.GetPixel(x, y);

                    int adjustedRed, adjustedGreen, adjustedBlue;
                    if (value > 0)
                    {
                        adjustedRed = Math.Min(pixel.R + value, 255);
                        adjustedGreen = Math.Min(pixel.G + value, 255);
                        adjustedBlue = Math.Min(pixel.B + value, 255);
                    }
                    else
                    {
                        adjustedRed = Math.Max(pixel.R + value, 0);
                        adjustedGreen = Math.Max(pixel.G + value, 0);
                        adjustedBlue = Math.Max(pixel.B + value, 0);
                    }

                    Color adjustedBrightnessColor = Color.FromArgb(adjustedRed, adjustedGreen, adjustedBlue);
                    b.SetPixel(x, y, adjustedBrightnessColor);
                }
            }
        }
        public static void Histogram(ref Bitmap a, ref Bitmap b)
        {
            Color pixel;
            Color gray;
            Byte grayData;
            int[] histogramHashMap = new int[256];

            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    pixel = a.GetPixel(x, y);
                    grayData = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    gray = Color.FromArgb(grayData, grayData, grayData);
                    a.SetPixel(x, y, gray);

                    histogramHashMap[grayData]++;
                }
            }

            b = new Bitmap(256, 800);
            for (int y = 0; y < b.Height; y++)
            {
                for (int x = 0; x < b.Width; x++)
                {
                    b.SetPixel(x, y, Color.White);
                }
            }

            for(int x = 0; x < b.Width; x++)
            {
                for(int y = 0; y < Math.Min(histogramHashMap[x] / 5, b.Height - 1); y++)
                {
                    b.SetPixel(x, (b.Height - 1) - y, Color.Black);
                }
            }

        }
    }
}
