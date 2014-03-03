using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace PrintImage
{
    class PrintImage
    {
        static String filename;

        static void usage()
        {
            Console.WriteLine("usage : PrintImage.exe [image_filename]");
            Console.WriteLine("");
            Environment.Exit(-1);
        }

        static bool FileCheck(String path)
        {
            if (File.Exists(path) == false) return false;

            return true;
        }

        static void PrintImageHandler(object sender, PrintPageEventArgs e)
        {
            Image img = Image.FromFile(filename);
            
            Rectangle rect = e.MarginBounds;

            e.Graphics.DrawImage(img, rect);
            e.HasMorePages = false;
            img.Dispose();
        }

        static int Main(string[] args)
        {
            if (args.Length == 0) usage();

            filename = args[0];
            if (FileCheck(filename) == false)
            {
                Console.WriteLine("error: file not found...filename={0}", filename);
                Console.WriteLine("");
                return -1;
            }

            PrintDocument print_document = new PrintDocument();
            print_document.PrintPage += new PrintPageEventHandler(PrintImageHandler);
            print_document.Print();

            return 0;
        }
    }
}
