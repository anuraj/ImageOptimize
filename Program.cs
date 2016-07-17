using System;
using System.IO;
using ImageProcessorCore;
using Microsoft.Extensions.Configuration;

namespace ImageOptimize
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .AddJsonFile("imageOptimize.json", optional: true);

            Configuration = builder.Build();

            var imageQuality = int.Parse(Configuration["quality"] ?? "10");
            var sourceDirectory = Configuration["sourceDirectory"];
            if (string.IsNullOrWhiteSpace(sourceDirectory))
            {
                sourceDirectory = Directory.GetCurrentDirectory();
            }

            if(!Directory.Exists(sourceDirectory))
            {
                Directory.CreateDirectory(sourceDirectory);
            }

            var imageTypes = Configuration["imageTypes"] ?? "*.png;*.jpg;*.gif";

            var types = imageTypes.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var type in types)
            {
                var files = GetFiles(sourceDirectory, type);
                ProcessImage(files, imageQuality);
            }
        }
        private static string[] GetFiles(string directory, string type)
        {
            return Directory.GetFiles(directory, type, SearchOption.AllDirectories);
        }
        private static void ProcessImage(string[] sourceImages, int imageQuality)
        {
            foreach (var sourceImage in sourceImages)
            {
                Console.WriteLine($"Processing file : {Path.GetFileName(sourceImage)}");
                var bytes = File.ReadAllBytes(sourceImage);
                using (var stream = new MemoryStream(bytes))
                {
                    var image = new Image(stream);
                    using (var ms = new MemoryStream())
                    {
                        image.Quality = imageQuality;
                        image.Save(ms);
                        var currentBytes = ms.ToArray();
                        ms.Flush();
                        ms.Dispose();
                        File.WriteAllBytes(sourceImage, currentBytes);
                    }
                }
            }
        }
    }
}
