using System;
using System.Collections.Generic;
using System.IO;

namespace LAB4OP
{

    class Image
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FileSize { get; set; } // = 54 + h * w * 3 + (((3 * w) % 4 == 0) ? 0 : ((4 - (3 * w) % 4) * w)) + 2;
        private byte[] info;
        public List<Pixel> Pixels { get; set; }

        private List<Pixel> GetPixels(BinaryReader br)
        {
            List<Pixel> pixels = new List<Pixel>();
            int skipAmount = ((3 * this.Width) % 4 == 0) ? 0 : ((4 - (3 * this.Width) % 4));
            for (int i = 0; i < this.Width * this.Height; i++)
            {
                if (i % this.Width == 0 && i != 0)
                    br.ReadBytes(skipAmount);
                pixels.Add(new Pixel(br.ReadBytes(3)));
            }
            return pixels;
        }

        public Image() { }

        public Image(string path)
        {
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                info = br.ReadBytes(54);
                this.Width = (info[18] | info[19] << 8 | info[20] << 16 | info[21] << 24);
                this.Height = (info[22] | info[23] << 8 | info[24] << 16 | info[25] << 24);
                this.FileSize = (info[2] | info[3] << 8 | info[4] << 16 | info[5] << 24);
                this.Pixels = GetPixels(br);
            }
        }

        private List<Pixel> MultiplyPixels(int multiplier)
        {
            List<Pixel> newPixels = new List<Pixel>();

            for (int i = 0; i < this.Height; i++)
            {
                List<Pixel> temp = new List<Pixel>();

                for (int j = 0; j < this.Width; j++)
                {
                    for (int k = 0; k < multiplier; k++)
                    {
                        temp.Add(Pixels[j + i * this.Width]);
                    }
                }

                for (int n = 0; n < multiplier; n++)
                {
                    newPixels.AddRange(temp);
                }
                
            }

            return newPixels;
        }

        public Image Enlarge(int multiplier)
        {
            Image enlarged = new Image();
            enlarged.Height = this.Height * multiplier;
            enlarged.Width = this.Width * multiplier;
            enlarged.FileSize = 54 + enlarged.Height * enlarged.Width * 3 + (((3 * enlarged.Width) % 4 == 0) ? 0 : ((4 - (3 * enlarged.Width) % 4) * enlarged.Width)) + 2;
            enlarged.info = this.info;

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

            enlarged.Pixels = this.MultiplyPixels(multiplier);

            return enlarged;
        }

        public void SaveTo(string path)
        {
            int skipAmount = ((3 * this.Width) % 4 == 0) ? 0 : ((4 - (3 * this.Width) % 4));
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                bw.Write(this.info);
                for (int i = 0; i < Pixels.Count; i++)
                {
                    if (i % this.Width == 0 && i != 0)
                        bw.Write(new byte[skipAmount]);
                    bw.Write(Pixels[i].ToArray());
                }
                bw.Write(new byte[2]);
            }
        }
    }
}