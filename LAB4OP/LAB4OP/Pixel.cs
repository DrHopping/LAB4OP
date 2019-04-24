namespace LAB4OP
{
    class Pixel
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Pixel(byte R, byte G, byte B)
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

        public byte[] ToArray()
        {
            return new byte[] { this.B, this.G, this.R };
        }

        public static Pixel operator +(Pixel p1, Pixel p2)
        {
            return new Pixel((byte)((p1.R + p2.R) / 2), (byte)((p1.G + p2.G) / 2), (byte)((p1.B + p2.B) / 2));
        }
    }
}