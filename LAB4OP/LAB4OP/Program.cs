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
        static void Menu(string outputPath, int multiplier)
        {
            Console.Write($"Enlarging image {multiplier} times...");
            Console.WriteLine("  Done.");
            Console.WriteLine($"Written result to {outputPath}");

        }

        static void Main(string[] args)
        {
            string inputPath = args[0];
            string outputPath = args[1];
            int multiplier = int.Parse(args[2]);
            Menu(outputPath, multiplier);
            (new Image(inputPath)).Enlarge(multiplier).SaveTo(outputPath);
        }
    }
}