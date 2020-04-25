using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageToPixelArray
{
    public class Decompiler
    {
        readonly static string fileLocation = @"Images";

        public List<pixelInfo> Decompile(string fileName)
        {
            CheckDirectory();

            List<pixelInfo> pixels = new List<pixelInfo>();

            string completePath = $@"{fileLocation}\{fileName}.png";

            Bitmap img = new Bitmap(completePath);
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);

                    pixelInfo info = new pixelInfo();

                    info.posX = i;
                    info.posY = j;
                    info.pixel = pixel;

                    pixels.Add(info);
                }
            }

            writeToFile(pixels);

            Recompiler r = new Recompiler();

            r.Prompt(pixels);

            return pixels;
        }

        private static void writeToFile(List<pixelInfo> pixels)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            File.WriteAllText($@"{fileLocation}\image1.txt", "");

            TextWriter tw = new StreamWriter($@"{fileLocation}\image1.txt");

            int currentPixel = 0;

            foreach (var obj in pixels)
            {
                currentPixel = currentPixel + 1;

                tw.WriteLine($"{obj.pixel.R}, {obj.pixel.G}, {obj.pixel.B} ({obj.posX}, {obj.posY})");

                //Console.WriteLine($"Wrote {currentPixel}/{pixels.Count}");
            }

            tw.Close();

            Console.WriteLine("\n\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Time Elapsed: {sw.ElapsedMilliseconds/1000} seconds");

            sw.Stop();

            Summarize();
        }

        private static void Summarize()
        {
            FileInfo info = new FileInfo($@"{fileLocation}\image1.txt");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nLog file size: {info.Length/1000}kB");
        }

        private static void CheckDirectory()
        {
            if(!Directory.Exists(fileLocation))
            {
                Directory.CreateDirectory(fileLocation);
            }
        }

        public class pixelInfo
        {
            public  int posX { set; get; }
            public  int posY { set; get; }
            public  Color pixel { set; get; }
        }
    }
}