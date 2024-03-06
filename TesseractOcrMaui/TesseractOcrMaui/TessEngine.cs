﻿#if IOS
using TesseractOcrMaui.IOS;
#else
using TesseractOcrMaui.ImportApis;
#endif

namespace TesseractOcrMaui;

/// <summary>
/// Tesseract engine that can process images with native library bindings.
/// </summary>
public class TessEngine : DisposableObject, ITessEngineConfigurable
{
    /// <summary>
    /// Create new Tess engine with native Tesseract api.
    /// </summary>
    /// <param name="languages">
    /// Language means traineddata file names without extension as a '+' separated list.
    /// For example 'fin+swe+eng'
    /// </param>
    /// <param name="logger">Logger to be used, by default NullLogger.</param>
    /// <exception cref="ArgumentNullException">If languages or traineddatapath is null.</exception>
    /// <exception cref="TesseractException">If Tesseract api cannot be initialized with given parameters or for some other reason.</exception>
    public TessEngine(string languages, ILogger? logger = null)
        : this(languages, string.Empty, logger)
    {
    }

    /// <summary>
    /// Create new Tess engine with native Tesseract api.
    /// </summary>
    /// <param name="languages">
    /// Language means traineddata file names without extension as a '+' separated list.
    /// For example 'fin+swe+eng'
    /// </param>
    /// <param name="traineddataPath">Full path to traineddata folder. Do not include file name.</param>
    /// <param name="logger">Logger to be used, by default NullLogger.</param>
    /// <exception cref="ArgumentNullException">If languages or traineddatapath is null.</exception>
    /// <exception cref="TesseractException">If Tesseract api cannot be initialized with given parameters or for some other reason.</exception>
    public TessEngine(string languages, string traineddataPath, ILogger? logger = null)
        : this(languages, traineddataPath, EngineMode.Default, new Dictionary<string, object>(), logger)
    {
    }

    /// <summary>
    /// Create new Tess engine with native Tesseract api.
    /// </summary>
    /// <param name="languages">
    /// Language means traineddata file names without extension as a '+' separated list.
    /// For example 'fin+swe+eng'
    /// </param>
    /// <param name="traineddataPath">Full path to traineddata folder. Do not include file name.</param>
    /// <param name="mode">Engine mode to be used when recognizing images.</param>
    /// <param name="initialOptions">Optional Tesseract parameters to be used.</param>
    /// <param name="logger">Logger to be used, by default NullLogger.</param>
    /// <exception cref="ArgumentNullException">If languages or traineddatapath is null.</exception>
    /// <exception cref="TesseractException">If Tesseract api cannot be initialized with given parameters or for some other reason.</exception>
    public TessEngine(string languages, string traineddataPath, EngineMode mode,
        IDictionary<string, object> initialOptions, ILogger? logger = null)
    {
        _logger = logger ?? NullLogger<TessEngine>.Instance;
        if (string.IsNullOrEmpty(languages))
        {
            _logger.LogError("Cannot initilize '{ctor}' with null or empty language.", nameof(TessEngine));
            throw new ArgumentNullException(nameof(languages));
        }
        if (traineddataPath is null)
        {
            _logger.LogError("Cannot initilize '{ctor}' with null trained data path.", nameof(TessEngine));
            throw new ArgumentNullException(nameof(traineddataPath));
        }
        // Debug: This line throws if dll are not copied to correct folder.
        Handle = new(this, TesseractApi.CreateApi());
        Initialize(languages, traineddataPath, mode, initialOptions);
    }

    /// <summary>
    /// Handle to used Tesseract api.
    /// </summary>
    internal HandleRef Handle { get; private set; }

    /// <inheritdoc/>
    public PageSegmentationMode DefaultSegmentationMode { get; set; } = PageSegmentationMode.Auto;

    /// <summary>
    /// Version of used Tesseract api. Returns null if version cannot be obtained.
    /// </summary>
    /// <returns>Version string if successful, otherwise null</returns>
    public static string? TryGetVersion() 
        => Marshal.PtrToStringAnsi(TesseractApi.GetVersion());


    /// <summary>
    /// Process image to TessPage.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="mode"></param>
    /// <returns>New Tess page containing information for recognizion.</returns>
    /// <exception cref="ArgumentNullException">image is null.</exception>
    /// <exception cref="ArgumentException">Image width or height has invalid values.</exception>
    /// <exception cref="PageNotDisposedException">Image already processed. You must dispose page after using.</exception>
    public TessPage ProcessImage(Pix image, PageSegmentationMode? mode = null) 
        => ProcessImage(image, null, new Rect(0, 0, image.Width, image.Height), mode);

    /// <summary>
    /// Process image to TessPage.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="inputName"></param>
    /// <param name="region"></param>
    /// <param name="mode"></param>
    /// <returns>New Tess page containing information for recognizion.</returns>
    /// <exception cref="ArgumentNullException">image is null.</exception>
    /// <exception cref="ArgumentException">Region is out of bounds.</exception>
    /// <exception cref="PageNotDisposedException">Image already processed. You must dispose page after using.</exception>
    public TessPage ProcessImage(Pix image, string? inputName, Rect region, PageSegmentationMode? mode)
    {
        if (image is null)
        {
            _logger.LogError("{cls}: Cannot process null image.", nameof(TessEngine));
            throw new ArgumentNullException(nameof(image));
        }
        if (region.X1 < 0 || region.Y1 < 0 || region.X1 > image.Width || region.Y2 > image.Height)
        {
            _logger.LogError("{cls}: Image region out of bounds, cannot process.", nameof(TessEngine));
            throw new ArgumentException($"Image {region} out of bounds, " +
                $"must be within image bounds", nameof(region));
        }
        if (_processCount > 0)
        {
            _logger.LogError("{cls}: Already has one image process. You must dispose {page} after using it.", 
                nameof(TessEngine), nameof(TessPage));
            throw new PageNotDisposedException("You must dispose old TessPage after using it.");
        }

        _processCount++;

#if IOS
        TesseractApi.SetPageSegmentationMode(Handle, (int?)mode ?? (int)DefaultSegmentationMode);
#else
        TesseractApi.SetPageSegmentationMode(Handle, mode ?? DefaultSegmentationMode);
#endif

        TesseractApi.SetImage(Handle, image.Handle);
        if (string.IsNullOrEmpty(inputName) is false)
        {
            TesseractApi.SetInputName(Handle, inputName);
        }

        TessPage page = new(this, image, inputName, region, mode, _logger);
        page.Disposed += OnIteratorDisposed;
        return page;
    }

    /// <summary>
    /// Set tesseract library variable for debug purposes.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns>True if success, otherwise false.</returns>
    public bool SetDebugVariable(string name, string value)
    {
        bool success = TesseractApi.SetDebugVariable(Handle, name, value) is not 0;
        _logger.LogInformation("Set Tesseract DEBUG variable '{name}' to value '{value}', success {success}",
            name, value, success);
        return success;
    }
    
    /// <inheritdoc/>
    public bool SetVariable(string name, string value)
    {
        bool success = TesseractApi.SetVariable(Handle, name, value) is not 0;
        _logger.LogInformation("Set Tesseract variable '{name}' to value '{value}', success {success}",
            name, value, success);
        return success;
    }

    /// <summary>
    /// Print all tesseract library variables to given file.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns>True if success, otherwise false.</returns>
    public bool TryPrintVariablesToFile(string fileName)
    {
        return TesseractApi.PrintVariablesToFile(Handle, fileName) is not 0;
    }

    /// <summary>
    /// Get bool variable from tesseract library.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns>True if success, otherwise false.</returns>
    public bool TryGetBoolVar(string name, out bool value)
    {
        if (TesseractApi.GetBoolVariable(Handle, name, out int result) is not 0)
        {
            value = result is not 0;
            return true;
        }
        value = false;
        return false;
    }

    /// <summary>
    /// Get int variable from tesseract library.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns>True if success, otherwise false.</returns>
    public bool TryGetIntVar(string name, out int value)
    {
        return TesseractApi.GetIntVariable(Handle, name, out value) is not 0;
    }

    /// <summary>
    /// Get double variable from tesseract library.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns>True if success, otherwise false.</returns>
    public bool TryGetDoubleVar(string name, out double value)
    {
        return TesseractApi.GetDoubleVariable(Handle, name, out value) is not 0;
    }

    /// <summary>
    /// Get string variable from tesseract library.
    /// </summary>
    /// <param name="name"></param>
    /// <returns>String value of library variable or empty if can't find with given name.</returns>
    public string GetStringVar(string name)
    {
        return TesseractApi.GetStringVariable(Handle, name) ?? string.Empty;
    }


    int _processCount = 0;
    readonly ILogger _logger;

    /// <summary>
    /// Initialize new Tesseract engine with given data.
    /// </summary>
    /// <param name="languages">
    /// '+' separated list of languages to be used with api. 
    /// Must have equivalent traineddatafile in specified traineddata directory.
    /// Do not include '.traineddata' extension or path.
    /// </param>
    /// <param name="traineddataPath">Path to directory containing traineddata files.</param>
    /// <param name="mode">Tesseract mode to be used.</param>
    /// <param name="initialOptions">Dictionary of option configurations.</param>
    /// <exception cref="TesseractException">If Api cannot be initialized for some reason.</exception>
    private void Initialize(string languages, string traineddataPath,
        EngineMode mode, IDictionary<string, object> initialOptions)
    {
        _logger.LogInformation("Initilize new '{cls}' with language '{lang}' and traineddata path '{path}'",
            nameof(TessEngine), languages, traineddataPath);

        languages ??= string.Empty;
        traineddataPath ??= string.Empty;

        int apiStatus = TessApi.BaseApiInit(Handle, languages, traineddataPath, mode, initialOptions);
        if (apiStatus is not 0)
        {
            _logger.LogError("Could not initialize new Tesseract api for {cls}", nameof(TessEngine));
            Dispose();
           
            // check if traineddata exists 
            bool traineddataExists = AnyTessdataFileExists(traineddataPath, languages.Split('+'));
            var inner = traineddataExists ? null :
                    new InvalidOperationException("No traineddata files found from path. " +
                    "Do you have correct path and file names?");

            throw new TesseractInitException("Cannot initialize Tesseract Api", inner);
        }
    }

    
    private static bool AnyTessdataFileExists(string path, string[] languages)
    {
        foreach (var language in languages)
        {
            string filePath = Path.Combine(path, $"{language}.traineddata");
            if (File.Exists(filePath))
            {
                return true;
            }
        }
        return false;
    }

    private void OnIteratorDisposed(object? sender, EventArgs e) => _processCount--;

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (Handle.Handle != nint.Zero)
        {
            TesseractApi.DeleteApi(Handle);
            Handle = new HandleRef(this, nint.Zero);
        }
    }
}
