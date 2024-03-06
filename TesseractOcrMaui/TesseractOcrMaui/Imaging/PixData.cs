﻿// Code copied from https://github.com/charlesw/tesseract
#if IOS
using TesseractOcrMaui.IOS;
#else
using TesseractOcrMaui.ImportApis;
#endif
namespace TesseractOcrMaui.Imaging;

/// <summary>
/// Store information about Pix image.
/// </summary>
public unsafe class PixData
{
    /// <summary>
    /// Pix corresponding to data.
    /// </summary>
    public Pix Pix { get; private set; }

    internal PixData(Pix pix)
    {
        Pix = pix;
        Data = LeptonicaApi.PixGetData(Pix.Handle);
        WordsPerLine = LeptonicaApi.PixGetWpl(Pix.Handle);
    }

    /// <summary>
    /// Pointer to the data.
    /// </summary>
    public IntPtr Data { get; private set; }

    /// <summary>
    /// Number of 32-bit words per line. 
    /// </summary>
    public int WordsPerLine { get; private set; }

    /// <summary>
    /// Swaps the bytes on little-endian platforms within a word; bytes 0 and 3 swapped, and bytes `1 and 2 are swapped.
    /// </summary>
    /// <remarks>
    /// This is required for little-endians in situations where we convert from a serialized byte order that is in raster order, 
    /// as one typically has in file formats, to one with MSB-to-the-left in each 32-bit word, or v.v. See <seealso href="http://www.leptonica.com/byte-addressing.html"/>
    /// </remarks>
    public void EndianByteSwap() => LeptonicaApi.PixEndianByteSwap(Pix.Handle);

    /// <summary>
    /// Encode image pixel to RGBA.
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <returns>uint representing pixel.</returns>
    public static uint EncodeAsRGBA(byte red, byte green, byte blue, byte alpha)
    {
        return (uint)(red << 24 |
            green << 16 |
            blue << 8 |
            alpha);
    }

    /// <summary>
    /// Gets the pixel value for a 1bpp image.
    /// </summary>
    public static uint GetDataBit(uint* data, int index)
    {
        return *(data + (index >> 5)) >> 31 - (index & 31) & 1;
    }


    /// <summary>
    /// Sets the pixel value for a 1bpp image.
    /// </summary>
    public static void SetDataBit(uint* data, int index, uint value)
    {
        uint* wordPtr = data + (index >> 5);
        *wordPtr &= ~(0x80000000 >> (index & 31));
        *wordPtr |= value << 31 - (index & 31);
    }


    /// <summary>
    /// Gets the pixel value for a 2bpp image.
    /// </summary>
    public static uint GetDataDIBit(uint* data, int index)
    {
        return *(data + (index >> 4)) >> 2 * (15 - (index & 15)) & 3;
    }



    /// <summary>
    /// Sets the pixel value for a 2bpp image.
    /// </summary>
    public static void SetDataDIBit(uint* data, int index, uint value)
    {
        uint* wordPtr = data + (index >> 4);
        *wordPtr &= ~(0xc0000000 >> 2 * (index & 15));
        *wordPtr |= (value & 3) << 30 - 2 * (index & 15);
    }


    /// <summary>
    /// Gets the pixel value for a 4bpp image.
    /// </summary>
    public static uint GetDataQBit(uint* data, int index)
    {
        return *(data + (index >> 3)) >> 4 * (7 - (index & 7)) & 0xf;
    }


    /// <summary>
    /// Sets the pixel value for a 4bpp image.
    /// </summary>
    public static void SetDataQBit(uint* data, int index, uint value)
    {
        uint* wordPtr = data + (index >> 3);
        *wordPtr &= ~(0xf0000000 >> 4 * (index & 7));
        *wordPtr |= (value & 15) << 28 - 4 * (index & 7);
    }


    /// <summary>
    /// Gets the pixel value for a 8bpp image.
    /// </summary>
    public static uint GetDataByte(uint* data, int index)
    {
        // Must do direct size comparison to detect x64 process, since in this will be jited out and results in a lot faster code (e.g. 6x faster for image conversion)
        if (IntPtr.Size is 8)
        {
            return *(byte*)((ulong)((byte*)data + index) ^ 3);
        }
        return *(byte*)((uint)((byte*)data + index) ^ 3);
        // Architecture types that are NOT little edian are not currently supported
        //return *((byte*)data + index);  
    }


    /// <summary>
    /// Sets the pixel value for a 8bpp image.
    /// </summary>
    public static void SetDataByte(uint* data, int index, uint value)
    {
        // Must do direct size comparison to detect x64 process, since in this will be jited out and results in a lot faster code (e.g. 6x faster for image conversion)
        if (IntPtr.Size is 8)
        {
            *(byte*)((ulong)((byte*)data + index) ^ 3) = (byte)value;
        }
        *(byte*)((uint)((byte*)data + index) ^ 3) = (byte)value;

        // Architecture types that are NOT little edian are not currently supported
        // *((byte*)data + index) =  (byte)value;
    }


    /// <summary>
    /// Gets the pixel value for a 16bpp image.
    /// </summary>
    public static uint GetDataTwoByte(uint* data, int index)
    {
        // Must do direct size comparison to detect x64 process, since in this will be jited out and results in a lot faster code (e.g. 6x faster for image conversion)
        if (IntPtr.Size is 8)
        {
            return *(ushort*)((ulong)((ushort*)data + index) ^ 2);
        }
        return *(ushort*)((uint)((ushort*)data + index) ^ 2);
        // Architecture types that are NOT little edian are not currently supported
        // return *((ushort*)data + index);
    }


    /// <summary>
    /// Sets the pixel value for a 16bpp image.
    /// </summary>
    public static void SetDataTwoByte(uint* data, int index, uint value)
    {
        // Must do direct size comparison to detect x64 process, since in this will be jited out and results in a lot faster code (e.g. 6x faster for image conversion)
        if (IntPtr.Size is 8)
        {
            *(ushort*)((ulong)((ushort*)data + index) ^ 2) = (ushort)value;
        }
        *(ushort*)((uint)((ushort*)data + index) ^ 2) = (ushort)value;
        // Architecture types that are NOT little edian are not currently supported
        //*((ushort*)data + index) = (ushort)value;
    }


    /// <summary>
    /// Gets the pixel value for a 32bpp image.
    /// </summary>
    public static uint GetDataFourByte(uint* data, int index) => *(data + index);


    /// <summary>
    /// Sets the pixel value for a 32bpp image.
    /// </summary>
    public static void SetDataFourByte(uint* data, int index, uint value) => *(data + index) = value;
}