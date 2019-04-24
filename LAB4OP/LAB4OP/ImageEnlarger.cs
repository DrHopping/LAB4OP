using System.Collections.Generic;

namespace LAB4OP
{
    class ImageEnlarger
    {
      
        static private Image ScaleImage(Image image, int multiplier)
        {
            int height = image.Height;
            int width = image.Width;
            Image newImage = new Image(width * multiplier, height * multiplier);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Pixel p = image.GetPixelAt(j, i);
                    SetScaledPixel(newImage, j, i, p, multiplier);
                }
            }
            return newImage;
        }

        static private void SetScaledPixel(Image image, int row, int column, Pixel p, int multiplier)
        {
            for (int i = row * multiplier; i < (row + 1) * multiplier; i++)
            {
                for (int j = column * multiplier; j < (column + 1) * multiplier; j++)
                {
                    image.SetPixel(i, j, p);
                }
            }
        }

        public static Image Enlarge(int multiplier, Image image)
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
    }
}