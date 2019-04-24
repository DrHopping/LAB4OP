using System.Collections.Generic;
using System.IO;

namespace LAB4OP
{
    class ImageReader
    {
        static public Image Read(string path)
        {
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var info = br.ReadBytes(54);
                int width = (info[18] | info[19] << 8 | info[20] << 16 | info[21] << 24);
                int height = (info[22] | info[23] << 8 | info[24] << 16 | info[25] << 24);
                int fileSize = (info[2] | info[3] << 8 | info[4] << 16 | info[5] << 24);
                var pixels = GetPixels(br, width, height);
                return new Image(info,width,height,fileSize,pixels);
            }
        }

        private static Pixel[,] GetPixels(BinaryReader br, int width, int height)
        {
            Pixel[,] pixels = new Pixel[height,width];
            int skipAmount = ((3 * width) % 4 == 0) ? 0 : ((4 - (3 * width) % 4));
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixels[i,j] = new Pixel(br.ReadBytes(3));
                }
                br.ReadBytes(skipAmount);
            }
            return pixels;
        }

        static public void SaveTo(Image image,string path)
        {
            int skipAmount = ((3 * image.Width) % 4 == 0) ? 0 : ((4 - (3 * image.Width) % 4));
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                bw.Write(image.info);
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {
                        bw.Write(image.Pixels[i,j].ToArray());
                    }
                    bw.Write(new byte[skipAmount]);
                }
                bw.Write(new byte[2]);
            }
        }
    }
}