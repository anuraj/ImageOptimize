# Image Optimize

This is a dotnet CLI tool which helps you to reduce the size of the Images.

## Get started
Install .NET 6 or newer and run this command:

`dotnet tool install --global dotnet-image-optimize`

Run the tool with `dotnet-image-optimize` command.

## Usage

```
Description:
  Image Optimize - A simple command line tool to optimize images in a directory.

Usage:
  dotnet-image-optimize <source> [options]

Arguments:
  <source>  Source images directory [default: C:\Windows]

Options:
  -o, --output <output>    Output directory
  -q, --quality <quality>  Image quality (1-100) [default: 75]
  --version                Show version information
  -?, -h, --help           Show help and usage information
```