using CTI.Metron.ImagePreProcessor;
using SkiaSharp;

Console.WriteLine("Image Contrast Enhancer starting...");
try
{
    var configManager = new ConfigurationManager();   
    var imageFiles = Directory.GetFiles(configManager.SourceDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".jpeg"));
    Directory.CreateDirectory(configManager.DestinationDirectory);
    foreach (var imagePath in imageFiles)
    {
        using var inputStream = File.OpenRead(imagePath);
        using var originalBitmap = SKBitmap.Decode(inputStream);
        ImageProcessor.PreprocessImage(originalBitmap);
    }
    Console.WriteLine("Image processing complete. Check the destination directory for results.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
