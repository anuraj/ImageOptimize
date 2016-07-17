# ImageOptimize

A dotnet CLI tool for image optimization.

## How to use ImageOptimize?

ImageOptimize is a dotnet CLI tool which will help you to modify image quality while publishing the image. You can run this tool as standalone as well. This tool is using [ImageProcessorCore](https://github.com/JimBobSquarePants/ImageProcessor) for manipulating the images. To use ImageOptimize first you need to reference ImageOptimize in your project.json in the tools section.
```
"tools": {
  "BundlerMinifier.Core": "2.0.238",
  "Microsoft.AspNetCore.Razor.Tools": "1.0.0-preview2-final",
  "Microsoft.AspNetCore.Server.IISIntegration.Tools": "1.0.0-preview2-final",
  "Microsoft.EntityFrameworkCore.Tools": "1.0.0-preview2-final",
  "ImageOptimize" : "1.0.0"
}
```

And you need to run the `dotnet restore` command, which will download the ImageOptimize to your system. Once `dotnet restore` successfully completed. You can run the ImageOptimize tool using `dotnet imgopt` command, by default the tool will compress all the JPG, PNG, and GIF images inside the current directory and sub directories. Default image quality will be 10. You can customize these settings using `ImageOptimize.json` file. Here is the structure of the file.

```
{
    "sourceDirectory": "C:\\SampleWebApp",
    "imageTypes": "*.jpg;*.png;*.gif",
    "quality" : "100"
}
```
In the configuration file, sourceDirectory should be absolute path, imageTypes should be string, seperated by semi-colon, and quality is number.

[![Build status](https://ci.appveyor.com/api/projects/status/xqbtp576j3li8oqf?svg=true)](https://ci.appveyor.com/project/anuraj/imageoptimize)
