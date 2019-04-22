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
        static void ShowAllBytes(string path)
        {
            using (BinaryReader br = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                int i = 1;
                while (true)
                {

                    try
                    {
                        Console.WriteLine(i + " " + br.ReadByte());
                        if (i % 3 == 0)
                            Console.WriteLine("==========");
                    }
                    catch { }
                    i++;
                }
            }
        }

        static string imagePath = @"C:\Users\Nikita Pedorenko\Desktop\image.bmp";
        static string bmpPath = "bmp2x2.bmp";
        static void Main(string[] args)
        {
            //ShowAllBytes(bmpPath);
            Image image = new Image(bmpPath);
            List<Pixel> pixels = image.Enlarge(3);
            Console.ReadLine();
        }
    }
}