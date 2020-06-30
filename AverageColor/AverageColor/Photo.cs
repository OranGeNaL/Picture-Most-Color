using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AverageColor
{
    public static class Photo
    {
        public static Bitmap bitmap;
        public static string imgAdress;
        //public static int maxDelta = 10;

        public static Color getModeColor(Bitmap sourceImage, int maxDelta)
        {
            Rectangle rect = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, sourceImage.PixelFormat);
            List<ColorMode> colorModes = new List<ColorMode>();

            // Get the address of the first line.
            IntPtr ptr = bitmapData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bitmapData.Stride) * sourceImage.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);



            for (int i = 0; i < sourceImage.Height; i++)
            {
                for(int j = 0; j < sourceImage.Width; j += 2)
                {
                    Color imgColor = Color.FromArgb(rgbValues[sourceImage.Height * i + j], rgbValues[sourceImage.Height * i + j + 1], rgbValues[sourceImage.Height * i + j + 2]);
                    bool exist = false;
                    foreach(ColorMode x in colorModes)
                    {
                        if (ColorDelta(imgColor/*sourceImage.GetPixel(j, i)*/, x.color) < maxDelta)
                        {
                            x.mode++;
                            //x.color = GetAverageColor(x.color, imgColor);
                            exist = true;
                            break;
                        }
                    }

                    if(!exist)
                    {
                        colorModes.Add(new ColorMode(imgColor));
                    }
                }
            }


            for(int i = 0; i < colorModes.Count; i++)
            {
                for(int j = 0; j < colorModes.Count - 1 - i; j++)
                {
                    if(colorModes[j].mode < colorModes[j + 1].mode)
                    {
                        ColorMode tempColorMode = new ColorMode(colorModes[j].color, colorModes[j].mode);

                        colorModes[j] = new ColorMode(colorModes[j + 1].color, colorModes[j + 1].mode);
                        colorModes[j + 1] = new ColorMode(tempColorMode.color, tempColorMode.mode);
                    }
                }
            }

            return colorModes[0].color;
        }

        public static int ColorDelta(Color color1, Color color2)
        {
            int[] res = new int[3];
            res[0] = Math.Abs(color1.R - color2.R);
            res[1] = Math.Abs(color1.G - color2.G);
            res[2] = Math.Abs(color1.B - color2.B);

            return res[0] + res[1] + res[2];
        }

        public static Color GetAverageColor(params Color[] colors)
        {
            int rSum = 0;
            int gSum = 0;
            int bSum = 0;

            foreach(Color x in colors)
            {
                rSum += x.R;
                gSum += x.G;
                bSum += x.B;
            }

            return Color.FromArgb(rSum / colors.Length, gSum / colors.Length, bSum / colors.Length);
        }

        public static Color GetMostColor(params Color[] colors)
        {
            return new Color();
        }
    }
}
