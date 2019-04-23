using System.Collections.Generic;

namespace LAB4OP
{
    class ImageEnlarger
    {
        static private List<Pixel> MultiplyPixels(Image image,int multiplier)
        {
            List<Pixel> newPixels = new List<Pixel>();

            for (int row = 0; row < image.Height; row++)
            {
                List<Pixel> temp = new List<Pixel>();

                for (int element = 0; element < image.Width; element++)
                {
                    for (int copy = 0; copy < multiplier; copy++)
                    {
                        temp.Add(image.Pixels[element + row * image.Width]);
                    }
                }

                for (int paste = 0; paste < multiplier; paste++)
                {
                    newPixels.AddRange(temp);
                }
            }
            return newPixels;
        }

        public static Image Enlarge(int multiplier, Image image)
        {
            Image enlarged = new Image();
            enlarged.Height = image.Height * multiplier;
            enlarged.Width = image.Width * multiplier;
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

            enlarged.Pixels = MultiplyPixels(image,multiplier);

            return enlarged;
        }
    }
}