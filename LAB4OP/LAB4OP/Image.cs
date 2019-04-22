using System.IO;

namespace LAB4OP
{

    class Image
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int FileSize { get; set; }
        private byte[] info;
        public Pixel[] Pixels { get; set; }
    }
}