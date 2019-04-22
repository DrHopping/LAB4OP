namespace LAB4OP
{
    class Pixel
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }

        public Pixel(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public Pixel(byte[] bgr)
        {
            this.R = bgr[2];
            this.G = bgr[1];
            this.B = bgr[0];
        }
    }
}