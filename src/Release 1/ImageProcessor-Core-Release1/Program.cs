using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Filters.Photo;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Bnaya.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsParser = new CommandLineParser(args);
            string src = argsParser["src"];
            string dest = argsParser["dest"];
            dest = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                                dest,
                                DateTime.Now.ToString("HH-mm-ss"));
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            var paths = Directory.GetFiles(src);
            var sw = Stopwatch.StartNew();
            foreach (var path in paths)
            {
                ProcessImage(dest, path);
            }

            sw.Stop();
            Console.WriteLine($"Done: {sw.Elapsed}");
            Console.ReadKey();
        }

        private static void ProcessImage(string dest, string path)
        {
            byte[] photoBytes = File.ReadAllBytes(path);
            string name = Path.GetFileName(path);
            string target = $@"{dest}\{name}.jpg";
            Console.WriteLine(name);

            using (var outStream = new FileStream(target, FileMode.Create))
            //using (var outStream1 = new FileStream(target1, FileMode.Create))
            // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            using (var imageFactory = new ImageFactory(preserveExifData: true))
            {
                // Do your magic here
                var f = imageFactory.Load(photoBytes)
                    .RoundedCorners(new RoundedCornerLayer(190, true, true, true, true));

                f.Resize(new Size(600, 600))
                     .Filter(MatrixFilters.GreyScale)
                     .Save(outStream);
            }
        }
    }
}
