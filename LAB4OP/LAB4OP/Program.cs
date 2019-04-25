using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4OP
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "TRU256.bmp";
            string outputPath = "Test.bmp";
            double multiplier = double.Parse("5");
            Console.Write($"Enlarging image {multiplier} times...");
            Image im = ImageEnlargerX.Enlarge(multiplier,ImageReader.Read(inputPath));
            ImageReader.SaveTo(im, outputPath);
            Console.WriteLine("  Done.");
            Console.WriteLine($"Written result to {outputPath}");


            Console.ReadLine();
        }
    }
}