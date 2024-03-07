using OpenCvSharp;
using SkiaSharp;

namespace CTI.Metron.ImagePreProcessor
{
    public class ImageProcessor(ConfigurationManager configuration)
    {
        public SKBitmap PreprocessImage(SKBitmap bitmap)
        {
            // Rescale the image (example: half the size)
            SKBitmap rescaledBitmap = bitmap.Resize(new SKImageInfo(bitmap.Width / configuration.ScaleWidth, bitmap.Height / configuration.ScaleHeight), SKFilterQuality.High);

            // Convert to grayscale
            SKBitmap grayscaleBitmap = new(rescaledBitmap.Width, rescaledBitmap.Height);
            using (var canvas = new SKCanvas(grayscaleBitmap))
            {
                var paint = new SKPaint
                {
                    ColorFilter = SKColorFilter.CreateColorMatrix(
                    [
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0,
                        0,
                        0,
                        1,
                        0
                    ])
                };
                canvas.DrawBitmap(rescaledBitmap, 0, 0, paint);
            }

            // Adjust contrast (example: increase contrast by 50%)
            //var contrastFilter = SKImageFilter.CreateColorFilter(SKColorFilter.CreateContrastFilter(1.5f));
            var skHighContrastConfig = new SKHighContrastConfig(grayscale: false, SKHighContrastConfigInvertStyle.NoInvert, 0f);
            var skColorFilter = SKColorFilter.CreateHighContrast(skHighContrastConfig);
            var contrastFilter = SKImageFilter.CreateColorFilter(cf: skColorFilter);
            SKBitmap contrastBitmap = new(grayscaleBitmap.Width, grayscaleBitmap.Height);
            using (var canvas = new SKCanvas(contrastBitmap))
            {
                var paint = new SKPaint { ImageFilter = contrastFilter };
                canvas.DrawBitmap(grayscaleBitmap, 0, 0, paint);
            }
            //return contrastBitmap;
            // Binarization using a threshold 
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
                        byte[] pixelData = brightness < configuration.Threshold ? [0, 0, 0, 255] : [255, 255, 255, 255];

                        // Write the pixel data to the destination bitmap
                        System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, addr + y * bytesPerRow + x * bytesPerPixel, bytesPerPixel);
                    }
                }
            }
            return binarizedBitmap;
            //// Note: Denoising can be quite complex and might require specific algorithms or libraries,
            //// which are not straightforward to implement with SkiaSharp directly.
            //// It often involves filtering techniques or more advanced processing.
            //SKBitmap denoisedBitMap = new(binarizedBitmap.Width, binarizedBitmap.Height);
            //denoisedBitMap = ApplyMedianFilter(binarizedBitmap);
            //return denoisedBitMap;
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
        public static SKBitmap ConvertToGrayscale(SKBitmap originalBitmap)
        {
            SKBitmap grayscaleBitmap = new(originalBitmap.Width, originalBitmap.Height, SKColorType.Gray8, SKAlphaType.Premul);
            using (var canvas = new SKCanvas(grayscaleBitmap))
            {
                canvas.Clear(SKColors.White);
                var paint = new SKPaint
                {
                    ColorFilter = SKColorFilter.CreateColorMatrix(
                    [
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0.2126f,
                        0.7152f,
                        0.0722f,
                        0,
                        0,
                        0,
                        0,
                        0,
                        1,
                        0
                    ])
                };
                canvas.DrawBitmap(originalBitmap, 0, 0, paint);
            }
            return grayscaleBitmap;
        }
        public static SKBitmap BinarizeImage(SKBitmap contrastBitmap, int threshold)
        {
            // Initialize a new bitmap for the binarized image
            SKBitmap binarizedBitmap = new(contrastBitmap.Width, contrastBitmap.Height);

            int width = contrastBitmap.Width;
            int height = contrastBitmap.Height;

            // Lock the bits of the bitmap for direct memory access
            using (var pixmap = binarizedBitmap.PeekPixels())
            {
                IntPtr addr = pixmap.GetPixels();
                int bytesPerRow = pixmap.RowBytes; // Calculate the number of bytes used to store a single row of pixels
                int bytesPerPixel = pixmap.BytesPerPixel;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Get the pixel color from the contrastBitmap
                        var color = contrastBitmap.GetPixel(x, y);

                        // Calculate the brightness using the luminosity method
                        var brightness = 0.2126f * color.Red + 0.7152f * color.Green + 0.0722f * color.Blue;

                        // Decide the color (black or white) based on the threshold
                        byte[] pixelData = brightness < threshold ? [0, 0, 0, 255] : [255, 255, 255, 255];

                        // Write the new pixel data to the binarized bitmap
                        System.Runtime.InteropServices.Marshal.Copy(pixelData, 0, addr + y * bytesPerRow + x * bytesPerPixel, bytesPerPixel);
                    }
                }
            }
            return binarizedBitmap;
        }
        public void PreprocessImageViaOpenCV(string imagePath, bool removeAllContours = false)
        {
            // Ensure configuration is accessible or passed as a parameter
            int thresholdValue = configuration.Threshold; // Example: Set appropriately
            string destinationDirectory = configuration.DestinationDirectory; // Example: Set appropriately

            using (Mat src = Cv2.ImRead(imagePath, ImreadModes.Color))
            using (Mat gray = new Mat())
            using (Mat thresholded = new Mat())
            {
                // Convert to grayscale and apply thresholding
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.Threshold(gray, thresholded, thresholdValue, 255, ThresholdTypes.Binary);

                // Find contours
                Cv2.FindContours(thresholded, out Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                if (!removeAllContours)
                {
                    // Find and draw the largest contour
                    int largestContourIndex = FindLargestContourIndex(contours);
                    if (largestContourIndex >= 0)
                    {
                        Cv2.DrawContours(src, contours, largestContourIndex, new Scalar(255, 255, 255), Cv2.FILLED);
                    }
                }
                else
                {
                    // Remove all detected contours
                    foreach (var contour in contours)
                    {
                        Cv2.DrawContours(src, new Point[][] { contour }, -1, new Scalar(0, 0, 0), Cv2.FILLED);
                    }
                }

                // Save the modified image
                var fileName = Path.GetFileNameWithoutExtension(imagePath) + "_modified.png"; // Adding "_modified" to differentiate from original
                var outputPath = Path.Combine(destinationDirectory, fileName);
                Cv2.ImWrite(outputPath, src);
            }
        }

        private int FindLargestContourIndex(Point[][] contours)
        {
            double largestArea = 0;
            int largestContourIndex = -1; // Default to -1, indicating no contour found
            for (int i = 0; i < contours.Length; i++)
            {
                double area = Cv2.ContourArea(contours[i]);
                if (area > largestArea)
                {
                    largestArea = area;
                    largestContourIndex = i;
                }
            }
            return largestContourIndex;
        }

        public void ContourDetection(string imagePath, bool removeAllContours = false)
        {
            using (Mat src = Cv2.ImRead(imagePath, ImreadModes.Color))
            {
                // Convert to grayscale and apply threshold
                using (Mat gray = new Mat())
                using (Mat thresholded = new Mat())
                {
                    Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
                    Cv2.Threshold(gray, thresholded, 128, 255, ThresholdTypes.Binary);

                    // Find contours
                    Cv2.FindContours(thresholded, out Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                    if (!removeAllContours)
                    {
                        // Find the largest contour if not removing all
                        double largestArea = 0;
                        int largestContourIndex = -1;
                        for (int i = 0; i < contours.Length; i++)
                        {
                            double area = Cv2.ContourArea(contours[i]);
                            if (area > largestArea)
                            {
                                largestArea = area;
                                largestContourIndex = i;
                            }
                        }

                        // Remove the largest contour by drawing it with the background color
                        if (largestContourIndex != -1)
                        {
                            Cv2.DrawContours(src, contours, largestContourIndex, new Scalar(255, 255, 255), Cv2.FILLED);
                        }
                    }
                    else
                    {
                        // Remove all detected contours
                        foreach (var contour in contours)
                        {
                            Cv2.DrawContours(src, new Point[][] { contour }, -1, new Scalar(255, 255, 255), Cv2.FILLED);
                        }
                    }

                    // Save or display the modified image             
                    var fileName = Path.GetFileNameWithoutExtension(imagePath) + "_modified.png";
                    var outputPath = Path.Combine(configuration.DestinationDirectory, fileName);
                    Cv2.ImWrite(outputPath, src);
                }
            }
        }
        public void RemoveBorder(string imagePath)
        {
            using (Mat src = Cv2.ImRead(imagePath, ImreadModes.Grayscale))
            using (Mat thresholded = new Mat())
            {
                // Apply binary thresholding
                Cv2.Threshold(src, thresholded, 128, 255, ThresholdTypes.Binary);

                // Find contours
                Cv2.FindContours(thresholded, out Point[][] contours, out HierarchyIndex[] hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

                double largestArea = 0;
                int largestContourIndex = -1;

                // Assume the largest contour is the border
                for (int i = 0; i < contours.Length; i++)
                {
                    double area = Cv2.ContourArea(contours[i]);
                    if (area > largestArea)
                    {
                        largestArea = area;
                        largestContourIndex = i;
                    }
                }

                // Mask the border by drawing over it
                if (largestContourIndex >= 0)
                {
                    Cv2.DrawContours(src, contours, largestContourIndex, new Scalar(255, 255, 255), thickness: Cv2.FILLED);
                }

                // Optionally, crop the image if the border has a regular shape and is at the edges
                // This would require calculating the bounding box for the contour and using it to define the crop region
                // Rect boundingBox = Cv2.BoundingRect(contours[largestContourIndex]);
                // Mat cropped = new Mat(src, boundingBox);

                // Save the result
                var fileName = Path.GetFileNameWithoutExtension(imagePath) + "_modified.png";
                var outputPath = Path.Combine(configuration.DestinationDirectory, fileName);
                Cv2.ImWrite(outputPath, src);
            }
        }

    }
}
