using Microsoft.Extensions.Logging;
using SkiaSharp;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TesseractOcrMaui;
using TesseractOcrMaui.Results;
#nullable enable
namespace TesseractOcrMauiTestApp;

public partial class MainPage : ContentPage
{
    public MainPage(ITesseract tesseract, ILogger<MainPage> logger)
    {
        InitializeComponent();
        Tesseract = tesseract;

        logger.LogInformation($"--------------------------------");
        logger.LogInformation($"-   {nameof(TesseractOcrMaui)} Demo   -");
        logger.LogInformation($"--------------------------------");

        var rid = RuntimeInformation.RuntimeIdentifier;
        logger.LogInformation("Running on rid '{rid}'", rid);
    }

    ITesseract Tesseract { get; }

    // This class includes examples of using the TesseractOcrMaui library.
    private async void DEMO_Recognize_AsImage_FromCamera(object sender, EventArgs e)
    {
        try
        {
            // Check if camera is available
            if (MediaPicker.IsCaptureSupported)
            {
                // Capture a photo
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null)
                {
                    // Get the local app data directory
                    string localPath = Path.Combine(FileSystem.AppDataDirectory, $"{DateTime.Now:yyyyMMdd_hhmmss}.png");

                    // Open the photo stream
                    using (var stream = await photo.OpenReadAsync())
                    {
                        var originalBitmap = SKBitmap.Decode(stream);

                        // Preprocess the image
                        var preprocessedBitmap = PreprocessImage(originalBitmap);

                        // Save the preprocessed image as a PNG
                        using (var image = SKImage.FromBitmap(preprocessedBitmap))
                        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                        using (var fileStream = File.OpenWrite(localPath))
                        {
                            // Write the PNG data to the file
                            data.SaveTo(fileStream);
                        }
                    }

                    // Now localPath has the path to the preprocessed and saved image in PNG format
                    // Display the preprocessed image in the UI
                    DisplayPreprocessedImage(localPath);

                    // You can now use Tesseract or another OCR tool to recognize text from this image
                    var result = await Tesseract.RecognizeTextAsync(localPath);

                    // Show output
                    ShowOutput("FromCamera", result);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle or log the error
            Debug.WriteLine($"Error capturing photo: {ex.Message}");
        }
    }

    private void DisplayPreprocessedImage(string imagePath)
    {
        // Assuming you have an Image control named PreprocessedImageView in your XAML
        // Set the source of the Image control to the preprocessed image file path
        PreprocessedImageView.Source = ImageSource.FromFile(imagePath);
    }

    private async void DEMO_Recognize_AsImage(object sender, EventArgs e)
    {
        // Select image (Not important)
        var path = await GetUserSelectedPath();
        if (path is null)
        {
            return;
        }
        //using var originalBitmap = SKBitmap.Decode(path);
        //var preprocessedBitmap = PreprocessImage(originalBitmap);
        //// Save the preprocessed image as a PNG
        //using (var image = SKImage.FromBitmap(preprocessedBitmap))
        //using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        //using (var fileStream = File.OpenWrite(path))
        //{
        //    // Write the PNG data to the file
        //    data.SaveTo(fileStream);
        //}
        // Recognize image 
        var result = await Tesseract.RecognizeTextAsync(path);
        DisplayPreprocessedImage(path);

        // Show output (Not important)
        ShowOutput("FromPath", result);
    }

    private async void DEMO_Recognize_AsBytes(object sender, EventArgs e)
    {
        // Select image (Not important)
        var path = await GetUserSelectedPath();
        if (path is null)
        {
            return;
        }

        // File to byte array (Use your own way)
        using FileStream stream = new(path, FileMode.Open, FileAccess.Read);
        byte[] buffer = new byte[stream.Length];
        stream.Read(buffer);

        // recognize bytes
        var result = await Tesseract.RecognizeTextAsync(buffer);
        
        
        // Show output (Not important)
        ShowOutput("FromBytes", result);
    }

    private async void DEMO_Recognize_AsConfigured(object sender, EventArgs e)
    {
        // Select image (Not important)
        var path = await GetUserSelectedPath();
        if (path is null)
        {
            return;
        }

        Tesseract.EngineConfiguration = (engine) =>
        {
            // Engine uses DefaultSegmentationMode, if no other is passed as method parameter.
            // If ITesseract is injected to page, this is only way of setting PageSegmentationMode.
            // PageSegmentationMode defines how ocr tries to look for text, for example singe character or single word.
            // By default uses PageSegmentationMode.Auto.
            engine.DefaultSegmentationMode = TesseractOcrMaui.Enums.PageSegmentationMode.SingleWord;
            
            engine.SetCharacterWhitelist("abcdefgh");   // These characters ocr is looking for
            engine.SetCharacterBlacklist("abc");        // These characters ocr is not looking for
            // Now ocr should be only finding characters 'defgh'
            // You can also notice that setting character listing will set ocr confidence to 0

        };

        // You can also set engine mode by uncommenting line below
        //Tesseract.EngineMode = TesseractOcrMaui.Enums.EngineMode.TesseractOnly;

        // Recognize image 
        var result = await Tesseract.RecognizeTextAsync(path);


        // For this example I reset engine configuration, because same object is used in other examples
        Tesseract.EngineConfiguration = null;

        // Show output (Not important)
        ShowOutput("FromPath, Configured", result);

    }


    private async void DEMO_GetVersion(object sender, EventArgs e)
    {
        string version = Tesseract.TryGetTesseractLibVersion() ?? "Failed";
        await DisplayAlert("Tesseract version", version, "OK");
    }


    // Not important for package 

    private static async Task<string?> GetUserSelectedPath()
    {
#if IOS
        var pickResult = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
        {
            Title = "Pick jpeg or png image"
        });
#else
        var pickResult = await FilePicker.PickAsync(new PickOptions()
        {
            PickerTitle = "Pick jpeg or png image",
            // Currently usable image types
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>()
            {
                [DevicePlatform.Android] = new List<string>() { "image/png", "image/jpeg" },
                [DevicePlatform.WinUI] = new List<string>() { ".png", ".jpg", ".jpeg" },
            })
        });
#endif
        return pickResult?.FullPath;
    }

    private void ShowOutput(string imageMode, RecognizionResult result)
    {
        // Show output (Not important)
        fileModeLabel.Text = $"File mode: {imageMode}";
        if (result.NotSuccess())
        {
            confidenceLabel.Text = $"Confidence: -1";
            resultLabel.Text = $"Recognizion failed: {result.Status}";
            return;
        }
        confidenceLabel.Text = $"Confidence: {result.Confidence}";
        resultLabel.Text = result.RecognisedText;
    }

    public static SKBitmap PreprocessImage(SKBitmap bitmap)
    {
        // Rescale the image (example: half the size)
        SKBitmap rescaledBitmap = bitmap.Resize(new SKImageInfo(bitmap.Width / 2, bitmap.Height / 2), SKFilterQuality.High);

        // Convert to grayscale
        SKBitmap grayscaleBitmap = new(rescaledBitmap.Width, rescaledBitmap.Height);
        using (var canvas = new SKCanvas(grayscaleBitmap))
        {
            var paint = new SKPaint
            {
                ColorFilter = SKColorFilter.CreateColorMatrix(new float[]
                {
                0.2126f, 0.7152f, 0.0722f, 0, 0,
                0.2126f, 0.7152f, 0.0722f, 0, 0,
                0.2126f, 0.7152f, 0.0722f, 0, 0,
                0, 0, 0, 1, 0
                })
            };
            canvas.DrawBitmap(rescaledBitmap, 0, 0, paint);
        }

        // Adjust contrast (example: increase contrast by 50%)
        //var contrastFilter = SKImageFilter.CreateColorFilter(SKColorFilter.CreateContrastFilter(1.5f));
        var skHighContrastConfig = new SKHighContrastConfig(grayscale: false, SKHighContrastConfigInvertStyle.NoInvert,0f);
        var skColorFilter = SKColorFilter.CreateHighContrast(skHighContrastConfig);
        var contrastFilter = SKImageFilter.CreateColorFilter(cf : skColorFilter);
        SKBitmap contrastBitmap = new(grayscaleBitmap.Width, grayscaleBitmap.Height);
        using (var canvas = new SKCanvas(contrastBitmap))
        {
            var paint = new SKPaint { ImageFilter = contrastFilter };
            canvas.DrawBitmap(grayscaleBitmap, 0, 0, paint);
        }
        //return contrastBitmap;
        // Binarization using a simple threshold (example: threshold = 75)
        SKBitmap binarizedBitmap = new(contrastBitmap.Width, contrastBitmap.Height);
        // Assuming contrastBitmap is an SKBitmap you've previously created
        int width = contrastBitmap.Width;
        int height = contrastBitmap.Height;
        // Lock the bits of the bitmap for direct memory access
        using (var pixmap = binarizedBitmap.PeekPixels())
        {
            IntPtr addr = pixmap.GetPixels();

            // Calculate the number of bytes used to store a single row of pixels in the bitmap
            // This accounts for any padding bytes that are added to each row in some bitmap formats
            int bytesPerRow = pixmap.RowBytes;
            int bytesPerPixel = pixmap.BytesPerPixel;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Calculate the address of the pixel to read in the source bitmap
                    var color = contrastBitmap.GetPixel(x, y);
                    var brightness = 0.2126f * color.Red + 0.7152f * color.Green + 0.0722f * color.Blue;

                    // Calculate the address of the pixel to write in the destination bitmap
                    //threshold - 120 for picture on laptop screen
                    //threshold 75 - for actual pic on electric meter
                    byte[] pixelData = brightness < 75 ? new byte[] { 0, 0, 0, 255 } : new byte[] { 255, 255, 255, 255 };
                    
                    // Write the pixel data to the destination bitmap
                    System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, addr + y * bytesPerRow + x * bytesPerPixel, bytesPerPixel);
                }
            }
        }

        // Note: Denoising can be quite complex and might require specific algorithms or libraries,
        // which are not straightforward to implement with SkiaSharp directly.
        // It often involves filtering techniques or more advanced processing.

        return binarizedBitmap;
    }
    public static SKBitmap ApplyMedianFilter(SKBitmap sourceBitmap, int kernelSize = 3)
    {
        SKBitmap resultBitmap = new(sourceBitmap.Width, sourceBitmap.Height);
        int[] dx = { -1, 0, 1, -1, 0, 1, -1, 0, 1 };
        int[] dy = { -1, -1, -1, 0, 0, 0, 1, 1, 1 };
        for (int y = 0; y < sourceBitmap.Height; y++)
        {
            for (int x = 0; x < sourceBitmap.Width; x++)
            {
                List<byte> neighborPixels = new();
                for (int i = 0; i < kernelSize * kernelSize; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    if (nx >= 0 && ny >= 0 && nx < sourceBitmap.Width && ny < sourceBitmap.Height)
                    {
                        var color = sourceBitmap.GetPixel(nx, ny);
                        neighborPixels.Add((byte)((color.Red + color.Green + color.Blue) / 3));
                    }
                }
                neighborPixels.Sort();
                byte medianValue = neighborPixels[neighborPixels.Count / 2];
                resultBitmap.SetPixel(x, y, new SKColor(medianValue, medianValue, medianValue));
            }
        }
        return resultBitmap;
    }
}

