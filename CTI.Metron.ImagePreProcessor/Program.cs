using CTI.Metron.ImagePreProcessor;
using SkiaSharp;

Console.WriteLine("Image Contrast Enhancer starting...");
try
{
    var configManager = new ConfigurationManager();   
    var imageFiles = Directory.GetFiles(configManager.SourceDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png") || file.EndsWith(".jpeg"));
    ImageProcessor imageProcessor = new ImageProcessor(configManager);
    Directory.CreateDirectory(configManager.DestinationDirectory);
    foreach (var imagePath in imageFiles)
    {
        #region Using SkiaSharp
        using var inputStream = File.OpenRead(imagePath);
        using var originalBitmap = SKBitmap.Decode(inputStream);
        var processedBitmap = imageProcessor.PreprocessImage(originalBitmap); // Assuming this method returns a SKBitmap

        // Save the processed image to the destination directory as PNG
        var fileName = Path.GetFileNameWithoutExtension(imagePath) + ".png"; // Change file extension to .png
        var outputPath = Path.Combine(configManager.DestinationDirectory, fileName);
        using var outputStream = File.OpenWrite(outputPath);
        using var image = SKImage.FromBitmap(processedBitmap);
        image.Encode(SKEncodedImageFormat.Png, 100).SaveTo(outputStream); // Encode as PNG
        #endregion

        #region Using OpenCV
        //imageProcessor.PreprocessImageViaOpenCV(imagePath);      
        #endregion
        //Console.WriteLine($"Processed and saved as PNG: {outputPath}");

    }
    Console.WriteLine("Image processing complete. Check the destination directory for results.");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}
