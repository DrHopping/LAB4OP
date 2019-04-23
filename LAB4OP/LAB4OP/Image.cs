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

    }
}