using System.Collections.Generic;

namespace LAB4OP
{
    class ImageEnlargerX
    {
        static public Image ScaleImage(Image image, double multiplier)
        {
            int width = image.Width;
            int height = image.Width;
            int multipliedWidth = (int)(width * multiplier);
            int multipliedHeight = (int)(height * multiplier);
            Image newImage = new Image(multipliedWidth, multipliedHeight);
            double widthRatio = (double)(width - 1) / (multipliedWidth - 1 > 1 ? multipliedWidth - 1 : multipliedWidth);
            double heightRatio = (double)(height - 1) / (multipliedHeight - 1 > 1 ? multipliedHeight - 1 : multipliedHeight);
            for (int i = 0; i < multipliedHeight; i++)
            {
                for (int j = 0; j < multipliedWidth; j++)
                {
                    double x = j * widthRatio;
                    double y = i * heightRatio;
                    int leftX = (int)x;
                    int leftY = (int)y;
                    int rightX = leftX + 1 < width ? leftX + 1 : leftX;
                    int rightY = leftY + 1 < height ? leftY + 1 : leftY;
                    Pixel LUCorner = image.GetPixelAt(leftX, leftY);
                    Pixel RUCorner = image.GetPixelAt(rightX, leftY);
                    Pixel LLCorner = image.GetPixelAt(leftX, rightY);
                    Pixel RLCorner = image.GetPixelAt(rightX, rightY);
                    Pixel interpolationUpper = ColorLinearInterpolation(x, leftX, leftX + 1, LUCorner, RUCorner);
                    Pixel interpolationLower = ColorLinearInterpolation(x, leftX, leftX + 1, LLCorner, RLCorner);
                    Pixel value = ColorLinearInterpolation(y, leftY, leftY + 1, interpolationUpper, interpolationLower);
                    newImage.SetPixel(j, i, value);
                }
            }
            return newImage;
        }

        static private Pixel ColorLinearInterpolation(double coord, int coord1, int coord2, Pixel pixel1, Pixel pixel2)
        {
            return new Pixel(
                    (byte)LinearInterpolation(coord, coord1, coord2,ToInt(pixel1.R), ToInt(pixel2.R)),
                    (byte)LinearInterpolation(coord, coord1, coord2,ToInt(pixel1.G), ToInt(pixel2.G)),
                    (byte)LinearInterpolation(coord, coord1, coord2,ToInt(pixel1.B), ToInt(pixel2.B)));
        }


        public static Image Enlarge(double multiplier, Image image)
        {
            Image enlarged = new Image();
            enlarged.Height = (int)(image.Height * multiplier);
            enlarged.Width = (int)(image.Width * multiplier);
            enlarged.FileSize = 54 + enlarged.Height * enlarged.Width * 3 + (((3 * enlarged.Width) % 4 == 0) ? 0 : ((4 - (3 * enlarged.Width) % 4) * enlarged.Width)) + 2;
            enlarged.info = image.info;

            int width = enlarged.Width;
            for (int i = 18; i < 22; i++)
            {
                enlarged.info[i] = (byte)(width & 0xFF);
                width >>= 8;
            }

            int height = enlarged.Height;
            for (int i = 22; i < 26; i++)
            {
                enlarged.info[i] = (byte)(height & 0xFF);
                height >>= 8;
            }

            int size = enlarged.FileSize;
            for (int i = 2; i < 6; i++)
            {
                enlarged.info[i] = (byte)(size & 0xFF);
                size >>= 8;
            }

            enlarged.Pixels = ScaleImage(image, multiplier).Pixels;

            return enlarged;
        }

        static private double LinearInterpolation(double pos, int pos1, int pos2, int pixel1, int pixel2)
        {
            return (pos2 - pos) / (pos2 - pos1) * pixel1 + (pos - pos1) / (pos2 - pos1) * pixel2;
        }

        static private int ToInt(byte b)
        {
            return (b + 256) % 256;
        }
    }
}