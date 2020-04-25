using System;

namespace ImageToPixelArray
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Title = "Image Decompile/Recompile by Cruxial";

            Console.WriteLine("Application started.\n");

            Decompiler d = new Decompiler();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Instructions:");
            Console.WriteLine("Head to the source location of this project and get some .png images and paste them into the Images folder.");
            Console.WriteLine("Enter the name of the file below. Keep in mind that this is case sensitive.\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter the file name you want to decompile (without file extensions): ");

            Console.ForegroundColor = ConsoleColor.White;
            var read = Console.ReadLine();

            d.Decompile(read);
        }
    }
}
