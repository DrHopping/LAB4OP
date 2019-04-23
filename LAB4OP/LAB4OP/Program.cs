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


        static public byte[] NewInfo(int size)
        {
            var newInfo = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                newInfo[i] = (byte)(size & 0xFF);
                size >>= 8;
            }
            return newInfo;
        }



        static void CopePaste(string inputPath, string outputPath)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(outputPath, FileMode.OpenOrCreate)))
            {
                using (BinaryReader br = new BinaryReader(File.Open(inputPath, FileMode.Open)))
                {
                    while (true)
                    {
                        try
                        {
                            bw.Write(br.ReadByte());
                        }
                        catch
                        {
                            bw.Close();
                        }
                    }
                }
            }
        }

        static string imagePath = @"C:\Users\Nikita Pedorenko\Desktop\image.bmp";
        static string bmpPath = "bmp2x2.bmp";
        static void Main(string[] args)
        {
            //var jo = NewInfo(3000056);
            //ShowAllBytes("bmp1000.bmp");
            //CopePaste("bmp1000.bmp", "new.bmp");
            Image image = new Image("bmp1000.bmp");
            //List<Pixel> pixels = image.Enlarge(3);
            image.SaveTo("new2.bmp");
            Console.ReadLine();
        }
    }
}