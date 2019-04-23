using System;
using System.Collections.Generic;
using System.IO;

namespace LAB4OP
{
    class Image
    {
        public int Width { get; set;}
        public int Height { get; set; }
        public int FileSize { get; set; } // = 54 + h * w * 3 + (((3 * w) % 4 == 0) ? 0 : ((4 - (3 * w) % 4) * w)) + 2;
        public byte[] info;
        public List<Pixel> Pixels { get; set; }


        public Image() { }

        public Image(byte[] info, int width, int height, int fileSize, List<Pixel> pixels)
        {
            this.info = info;
            this.Width = width;
            this.Height = height;
            this.FileSize = fileSize;
            this.Pixels = pixels;
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