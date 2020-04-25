using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageToPixelArray
{
    internal class Recompiler
    {

        public string OutputFolder = @"Images\Output";

        public void Prompt(List<Decompiler.pixelInfo> pixels)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nRecompile the image? Y/N: ");

            Console.ForegroundColor = ConsoleColor.White;
            var read = Console.ReadLine().ToUpper();

            Console.ForegroundColor = ConsoleColor.Yellow;
            switch (read)
            {
                case "Y":
                    Console.Clear();
                    Console.WriteLine("Initializing Recompiler..");
                    Recompile(pixels);
                    break;

                case "N":
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    return;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"'{read}' is not a valid option. Closing down..");
                    break;
            }
        }

        internal void Recompile(List<Decompiler.pixelInfo> pixels)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nOutput file name (without file extensions): ");

            Console.ForegroundColor = ConsoleColor.White;
            var read = Console.ReadLine();

            string completePath = $@"{OutputFolder}\{read}.png";
            string fileName = $"{read}.png";

            if (!Directory.Exists(OutputFolder))
            {
                Directory.CreateDirectory(OutputFolder);
            }

            if(File.Exists(completePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n{fileName} already exists! Do you wish to delete it? Y/N: ");

                Console.ForegroundColor = ConsoleColor.White;
                var deleteOrRename = Console.ReadLine();

                switch(deleteOrRename.ToUpper())
                {
                    case "Y":
                        File.Delete(completePath);
                        break;

                    case "N":
                        Prompt(pixels);
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"'{read}' is not a valid option.");
                        Prompt(pixels);
                        return;
                }
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();

            var lastInList = pixels[pixels.Count - 1];

            Bitmap pic = new Bitmap(lastInList.posX, lastInList.posY, PixelFormat.Format32bppArgb);

            int currentPixel = 0;

            for (int x = 0; x < lastInList.posX; x++)
            {
                currentPixel++;

                for (int y = 0; y < lastInList.posY; y++)
                {
                    currentPixel++;

                    var c = pixels[currentPixel];

                    pic.SetPixel(x, y, c.pixel);
                }
            }

            pic.Save(completePath, ImageFormat.Png);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nTime elapsed: {sw.ElapsedMilliseconds}ms");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n'{fileName}' was saved to {OutputFolder}!");

            sw.Stop();

            Console.WriteLine("");
        }
    }
}