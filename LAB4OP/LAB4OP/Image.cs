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

        public Image(string path)
        {
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                info = br.ReadBytes(54);


                this.Width = (info[18] | info[19] << 8 | info[20] << 16 | info[21] << 24);
                this.Height = (info[22] | info[23] << 8 | info[24] << 16 | info[25] << 24);
                this.FileSize = (info[2] | info[3] << 8 | info[4] << 16 | info[5] << 24);

                Pixel[] pixels = new Pixel[this.Width * this.Height];

                for (int i = 0; i < pixels.Length; i++)
                {
                    if(i % this.Width == 0 && i != 0)
                        br.ReadBytes(2);
                    pixels[i] = new Pixel(br.ReadBytes(3));
                }

                this.Pixels = pixels;
            }
        }
    }
}