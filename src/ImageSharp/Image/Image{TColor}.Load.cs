// <copyright file="Image{TColor}.Load.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

using System;
using System.Buffers;
using System.IO;
using System.Linq;
using System.Text;
using ImageSharp.Formats;

namespace ImageSharp
{
    /// <summary>
    /// Load images from steams
    /// </summary>
    public partial class Image<TColor>
        where TColor : struct, IPixel<TColor>
    {
        /// <summary>
        /// Loads the image from the given stream.
        /// </summary>
        /// <param name="stream">The stream containing image information.</param>
        /// <param name="options">The options for the decoder.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        public static Image<TColor> FromStream(Stream stream, IDecoderOptions options)
        {
            return FromStream(stream, options, Configuration.Default);
        }
        
        /// <summary>
        /// Loads the image from the given stream.
        /// </summary>
        /// <param name="stream">The stream containing image information.</param>
        /// <param name="options">The options for the decoder.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        public static Image<TColor> FromStream(Stream stream, Configuration config)
        {
            return FromStream(stream, null, config);
        }
        
        /// <summary>
        /// Loads the image from the given stream.
        /// </summary>
        /// <param name="stream">The stream containing image information.</param>
        /// <param name="options">The options for the decoder.</param>
        /// <exception cref="NotSupportedException">
        /// Thrown if the stream is not readable nor seekable.
        /// </exception>
        public static Image<TColor> FromStream(Stream stream, IDecoderOptions options, Configuration config)
        {
              if (!config.ImageFormats.Any())
            {
                throw new InvalidOperationException("No image formats have been configured.");
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException("Cannot read from the stream.");
            }

            if (stream.CanSeek)
            {
                if (Decode(stream, options, config, out Image<TColor> img))
                {
                    return img;
                }
            }
            else
            {
                // We want to be able to load images from things like HttpContext.Request.Body
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    ms.Position = 0;

                    if (Decode(ms, options, config, out Image<TColor> img))
                    {
                        return img;
                    }
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Image cannot be loaded. Available formats:");

            foreach (IImageFormat format in config.ImageFormats)
            {
                stringBuilder.AppendLine("-" + format);
            }

            throw new NotSupportedException(stringBuilder.ToString());
        }


        /// <summary>
        /// Decodes the image stream to the current image.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="options">The options for the decoder.</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool Decode(Stream stream, IDecoderOptions options, Configuration config, out Image<TColor> img)
        {
            img = null;
            int maxHeaderSize = config.MaxHeaderSize;
            if (maxHeaderSize <= 0)
            {
                return false;
            }

            IImageFormat format;
            byte[] header = ArrayPool<byte>.Shared.Rent(maxHeaderSize);
            try
            {
                long startPosition = stream.Position;
                stream.Read(header, 0, maxHeaderSize);
                stream.Position = startPosition;
                format = config.ImageFormats.FirstOrDefault(x => x.IsSupportedFileFormat(header));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(header);
            }

            if (format == null)
            {
                return false;
            }

            img = format.Decoder.Decode<TColor>(stream, options);
            img.CurrentImageFormat = format;
            return true;
        }
    }
}