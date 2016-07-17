using System;
using System.IO;
using System.Threading.Tasks;
using ImageProcessorCore;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var jpgFiles = Directory.GetFiles(Directory.GetCurrentDirectory(),
                "*.jpg", SearchOption.AllDirectories);
            var pngFiles = Directory.GetFiles(Directory.GetCurrentDirectory(),
                "*.png", SearchOption.AllDirectories);
            var gifFiles = Directory.GetFiles(Directory.GetCurrentDirectory(),
                 "*.gif", SearchOption.AllDirectories);
            ProcessImage(jpgFiles);
            ProcessImage(pngFiles);
            ProcessImage(gifFiles);
        }

        private static void ProcessImage(string[] sourceImages)
        {
            int bytesSaved = 0;
            foreach (var sourceImage in sourceImages)
            {
                var bytes = File.ReadAllBytes(sourceImage);
                using (var stream = new MemoryStream(bytes))
                {
                    var image = new Image(stream);
                    using (var ms = new MemoryStream())
                    {
                        image.Quality = 10;
                        image.Save(ms);
                        var currentBytes = ms.ToArray();
                        ms.Flush();
                        ms.Dispose();
                        File.WriteAllBytes(sourceImage, currentBytes);

                        bytesSaved += (bytes.Length - currentBytes.Length);
                    }
                }
            }
        }
    }
}
