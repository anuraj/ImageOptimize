using System.CommandLine;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

var outputOption = new Option<string>(
    aliases: new[] { "-o", "--output" },
    description: "Output directory")
{
    IsRequired = false
};

var qualityOption = new Option<int>(
    aliases: new[] { "-q", "--quality" },
    description: "Image quality (1-100)")
{
    IsRequired = false
};

qualityOption.SetDefaultValue(75);

var rootCommand = new RootCommand
{
    outputOption,
    qualityOption
};

rootCommand.Description = "Image Optimize - A simple command line tool to optimize images in a directory.";

rootCommand.Name = "dotnet-image-optimize";

var sourceArgument = new Argument<string>("source")
{
    Arity = ArgumentArity.ExactlyOne,
    Description = "Source images directory"
};

var supportedFormats = new[] { ".png", ".jpg", ".jpeg", ".gif" };

sourceArgument.SetDefaultValue(Directory.GetCurrentDirectory());
rootCommand.Add(sourceArgument);

rootCommand.SetHandler((source, output, quality) =>
{
    try
    {
        if (!Directory.Exists(source))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Source directory {source} does not exist.");
            Console.ResetColor();
        }

        if (string.IsNullOrEmpty(output))
        {
            output = source;
        }
        else
        {
            if (!Directory.Exists(output))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Output directory does not exist. Creating it now.");
                Console.ResetColor();
            }
        }

        var files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories)
            .Where(file => supportedFormats.Contains(Path.GetExtension(file).ToLowerInvariant()));

        if (!files.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No images found in the source directory {source}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total images found : {files.Count()}, starting optimization with quality {quality}.");
            Console.ResetColor();
            long totalImageSizeSaved = 0;
            foreach (var file in files)
            {
                long originalSize = new FileInfo(file).Length;
                using var image = Image.Load(file);
                var encoder = new JpegEncoder
                {
                    Quality = quality
                };
                image.Save(file, encoder);
                long compressedSize = new FileInfo(file).Length;
                long sizeSaved = originalSize - compressedSize;
                totalImageSizeSaved += sizeSaved;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total images optimized {files.Count()}, size reduced: {totalImageSizeSaved / 1024} kb");
            Console.ResetColor();
        }
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();
    }
}, sourceArgument, outputOption, qualityOption);

return rootCommand.Invoke(args);