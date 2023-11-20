using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using static LanguageExt.Prelude;

namespace CompanyNamePlaceHolder.Common.Web.Utility.Helpers;

// TODO: Return Validation instead of mutating ModelState

/// <summary>
/// Helper and extension methods for the <see cref="IFormFile"/> class.
/// </summary>
public class FileHelper
{
    // If you require a check on specific characters in the IsValidFileExtensionAndSignature
    // method, supply the characters in the _allowedChars field.
    private static readonly byte[] _allowedChars = System.Array.Empty<byte>();
    // For more file signatures, see the File Signatures Database (https://www.filesignatures.net/)
    // and the official specifications for the file types you wish to add.
    private static readonly Dictionary<string, List<byte[]>> _fileSignature = new()
    {
        { ".gif", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
        { ".png", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
        {
            ".jpeg",
            new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
        }
        },
        {
            ".jpg",
            new List<byte[]>
        {
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
            new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 },
        }
        },
        {
            ".zip",
            new List<byte[]>
        {
            new byte[] { 0x50, 0x4B, 0x03, 0x04 },
            new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x45 },
            new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
            new byte[] { 0x50, 0x4B, 0x05, 0x06 },
            new byte[] { 0x50, 0x4B, 0x07, 0x08 },
            new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 },
        }
        },
        { ".xlsx", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00, 0x06, 0x00 } } },
    };

    // **WARNING!**
    // In the following file processing methods, the file`s content isn`t scanned.
    // In most production scenarios, an anti-virus/anti-malware scanner API is
    // used on the file before making the file available to users or other
    // systems. For more information, see the topic that accompanies this sample
    // app.

    /// <summary>
    /// Applies standard validations on the <see cref="IFormFile"/>
    /// then saves the file to the specified <paramref name="targetFilePath"/>.
    /// </summary>
    /// <typeparam name="T">Type of the model class containing the <see cref="IFormFile"/> property</typeparam>
    /// <param name="formFile"></param>
    /// <param name="permittedExtensions">List of permitted file extensions</param>
    /// <param name="sizeLimit">Max file size limit</param>
    /// <param name="targetFilePath">Path where file will be saved</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Filename used for storage</returns>
    public static async Task<Validation<Error, string>> ProcessFormFile<T>(IFormFile formFile,
        string[] permittedExtensions, long sizeLimit, string targetFilePath,
        CancellationToken cancellationToken = default) =>
        await ProcessFormFile<T, string>(formFile,
                                         permittedExtensions,
                                         sizeLimit,
                                         cancellationToken: cancellationToken,
                                         f: s =>
                                         {
                                             var trustedFileNameForFileStorage = Path.GetRandomFileName();
                                             var filePath = Path.Combine(targetFilePath, trustedFileNameForFileStorage);
                                             using var file = File.Create(filePath);
                                             s.WriteTo(file);
                                             return filePath;
                                         });

    /// <summary>
    /// Applies standard validations on the <see cref="IFormFile"/>
    /// then converts it to <see cref="Stream"/>.
    /// </summary>
    /// <typeparam name="T">Type of the model class containing the <see cref="IFormFile"/> property</typeparam>
    /// <typeparam name="TRet">Type that will be returned by the specified parameter <paramref name="f"/></typeparam>
    /// <param name="formFile"></param>
    /// <param name="permittedExtensions">List of permitted file extensions</param>
    /// <param name="sizeLimit">Max file size limit</param>
    /// <param name="f"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Validation<Error, TRet>> ProcessFormFile<T, TRet>(IFormFile formFile,
        string[]? permittedExtensions, long sizeLimit, Func<MemoryStream, TRet> f, CancellationToken cancellationToken = default)
    {
        var fieldDisplayName = string.Empty;

        // Use reflection to obtain the display name for the model
        // property associated with this IFormFile. If a display
        // name isn`t found, error messages simply won`t show
        // a display name.
        MemberInfo? property = typeof(T).GetProperty(formFile.Name[(formFile.Name.IndexOf(".", StringComparison.Ordinal) + 1)..]);

        if (property != null)
        {
            if (property.GetCustomAttribute(typeof(DisplayAttribute)) is DisplayAttribute displayAttribute)
            {
                fieldDisplayName = $"{displayAttribute.Name} ";
            }
        }

        // Don`t trust the file name sent by the client. To display
        // the file name, HTML-encode the value.
        var trustedFileNameForDisplay = WebUtility.HtmlEncode(formFile.FileName);

        // Check the file length. This check doesn`t catch files that only have 
        // a BOM as their content.
        if (formFile.Length == 0)
        {
            return Error.New($"{fieldDisplayName}({trustedFileNameForDisplay}) is empty.");
        }

        if (formFile.Length > sizeLimit)
        {
            var megabyteSizeLimit = sizeLimit / 1048576;
            return Error.New($"{fieldDisplayName}({trustedFileNameForDisplay}) exceeds {megabyteSizeLimit:N1} MB.");
        }

        using var memoryStream = new MemoryStream();
        await formFile.CopyToAsync(memoryStream, cancellationToken);

        // Check the content length in case the file`s only
        // content was a BOM and the content is actually
        // empty after removing the BOM.
        if (memoryStream.Length == 0)
        {
            return Error.New($"{fieldDisplayName}({trustedFileNameForDisplay}) is empty.");
        }

        if (!IsValidFileExtensionAndSignature(formFile.FileName, memoryStream, permittedExtensions))
        {
            return Error.New($"{fieldDisplayName}({trustedFileNameForDisplay}) file type isn`t permitted or the file`s signature doesn`t match the file`s extension.");
        }

        return f(memoryStream);
    }

    /// <summary>
    /// Apply standard validation on a file received from a multipart request.
    /// then converts it to <see cref="Stream"/>.
    /// </summary>
    /// <param name="section"></param>
    /// <param name="contentDisposition"></param>
    /// <param name="permittedExtensions">List of permitted file extensions</param>
    /// <param name="sizeLimit">Max file size limit</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<Validation<Error, Stream>> ProcessStreamedFile(MultipartSection section,
        ContentDispositionHeaderValue contentDisposition, string[] permittedExtensions, long sizeLimit,
        CancellationToken cancellationToken = default)
    {
        using var memoryStream = new MemoryStream();
        await section.Body.CopyToAsync(memoryStream, cancellationToken);

        // Check if the file is empty or exceeds the size limit.
        if (memoryStream.Length == 0)
        {
            return Error.New("The file is empty.");
        }

        if (memoryStream.Length > sizeLimit)
        {
            var megabyteSizeLimit = sizeLimit / 1048576;
            return Error.New($"The file exceeds {megabyteSizeLimit:N1} MB.");
        }

        if (!IsValidFileExtensionAndSignature(contentDisposition.FileName, memoryStream, permittedExtensions))
        {
            return Error.New("The file type isn`t permitted or the file`s signature doesn`t match the file`s extension.");
        }

        return memoryStream;
    }

    private static bool IsValidFileExtensionAndSignature(string? fileName, Stream? data, string[]? permittedExtensions)
    {
        if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
        {
            return false;
        }

        var ext = Path.GetExtension(fileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(ext) || (permittedExtensions != null && !permittedExtensions.Contains(ext)))
        {
            return false;
        }

        data.Position = 0;

        using var reader = new BinaryReader(data);
        if (ext.Equals(".txt") || ext.Equals(".csv") || ext.Equals(".prn"))
        {
            if (_allowedChars.Length == 0)
            {
                // Limits characters to ASCII encoding.
                for (var i = 0; i < data.Length; i++)
                {
                    if (reader.ReadByte() > sbyte.MaxValue)
                    {
                        return false;
                    }
                }
            }
            else
            {
                // Limits characters to ASCII encoding and
                // values of the _allowedChars array.
                for (var i = 0; i < data.Length; i++)
                {
                    var b = reader.ReadByte();
                    if (b > sbyte.MaxValue ||
                        !_allowedChars.Contains(b))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Uncomment the following code block if you must permit
        // files whose signature isn`t provided in the _fileSignature
        // dictionary. We recommend that you add file signatures
        // for files (when possible) for all file types you intend
        // to allow on the system and perform the file signature
        // check.
        /*
        if (!_fileSignature.ContainsKey(ext))
        {
            return true;
        }
        */

        // File signature check
        // --------------------
        // With the file signatures provided in the _fileSignature
        // dictionary, the following code tests the input content`s
        // file signature.
        var signatures = _fileSignature[ext];
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature =>
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }
}