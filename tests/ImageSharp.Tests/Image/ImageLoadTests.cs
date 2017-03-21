// <copyright file="PixelAccessorTests.cs" company="James Jackson-South">
// Copyright (c) James Jackson-South and contributors.
// Licensed under the Apache License, Version 2.0.
// </copyright>

namespace ImageSharp.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using ImageSharp.Formats;
    using ImageSharp.IO;
    using Moq;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Image"/> class.
    /// </summary>
    public class ImageLoadTests : IDisposable
    {
        private readonly Image Image;
        private readonly Mock<IFileSystem> fileSystem;
        private readonly Mock<IImageFormat> format;
        private readonly Mock<IImageFormat> formatNotRegistered;
        private readonly Mock<IImageDecoder> decoder;
        private readonly Mock<IImageDecoder> decoderNotInFormat;
        private readonly IDecoderOptions decoderOptions;

        public Configuration Configuration { get; private set; }

        public ImageLoadTests()
        {
            this.decoder = new Mock<IImageDecoder>();
            this.format = new Mock<IImageFormat>();
            this.format.Setup(x => x.Decoder).Returns(this.decoder.Object);
            this.format.Setup(x => x.Encoder).Returns(new Mock<IImageEncoder>().Object);
            this.format.Setup(x => x.MimeType).Returns("img/test");
            this.format.Setup(x => x.Extension).Returns("png");
            this.format.Setup(x => x.SupportedExtensions).Returns(new string[] { "png", "jpg" });


            this.decoderNotInFormat = new Mock<IImageDecoder>();
            this.formatNotRegistered = new Mock<IImageFormat>();
            this.formatNotRegistered.Setup(x => x.Decoder).Returns(this.decoderNotInFormat.Object);
            this.formatNotRegistered.Setup(x => x.Encoder).Returns(new Mock<IImageEncoder>().Object);
            this.formatNotRegistered.Setup(x => x.MimeType).Returns("img/test");
            this.formatNotRegistered.Setup(x => x.Extension).Returns("png");
            this.formatNotRegistered.Setup(x => x.SupportedExtensions).Returns(new string[] { "png", "jpg" });

            this.fileSystem = new Mock<IFileSystem>();
            this.decoderOptions = new Mock<IDecoderOptions>().Object;
            this.Configuration =new Configuration(this.format.Object) {
                FileSystem = this.fileSystem.Object
            };

        }

        [Fact]
        public void LoadFromStream()
        {
            Stream stream = new MemoryStream();
            Image.Load(stream, )
            this.fileSystem.Setup(x => x.Create("path.png")).Returns(stream);
            this.Image.Save("path.png");

            this.encoder.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SavePathWithOptions()
        {
            Stream stream = new MemoryStream();
            this.fileSystem.Setup(x => x.Create("path.jpg")).Returns(stream);

            this.Image.Save("path.jpg", this.encoderOptions);
            
            this.encoder.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }

        [Fact]
        public void SavePathWithEncoder()
        {
            Stream stream = new MemoryStream();
            this.fileSystem.Setup(x => x.Create("path.jpg")).Returns(stream);

            this.Image.Save("path.jpg", this.encoderNotInFormat.Object);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SavePathWithEncoderAndOptions()
        {
            Stream stream = new MemoryStream();
            this.fileSystem.Setup(x => x.Create("path.jpg")).Returns(stream);

            this.Image.Save("path.jpg", this.encoderNotInFormat.Object, this.encoderOptions);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }



        [Fact]
        public void SavePathWithFormat()
        {
            Stream stream = new MemoryStream();
            this.fileSystem.Setup(x => x.Create("path.jpg")).Returns(stream);

            this.Image.Save("path.jpg", this.encoderNotInFormat.Object);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SavePathWithFormatAndOptions()
        {
            Stream stream = new MemoryStream();
            this.fileSystem.Setup(x => x.Create("path.jpg")).Returns(stream);

            this.Image.Save("path.jpg", this.encoderNotInFormat.Object, this.encoderOptions);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }

        [Fact]
        public void SaveStream()
        {
            Stream stream = new MemoryStream();
            this.Image.Save(stream);
            
            this.encoder.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SaveStreamWithOptions()
        {
            Stream stream = new MemoryStream();

            this.Image.Save(stream, this.encoderOptions);

            this.encoder.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }

        [Fact]
        public void SaveStreamWithEncoder()
        {
            Stream stream = new MemoryStream();

            this.Image.Save(stream, this.encoderNotInFormat.Object);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SaveStreamWithEncoderAndOptions()
        {
            Stream stream = new MemoryStream();

            this.Image.Save(stream, this.encoderNotInFormat.Object, this.encoderOptions);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }

        [Fact]
        public void SaveStreamWithFormat()
        {
            Stream stream = new MemoryStream();

            this.Image.Save(stream, this.formatNotRegistered.Object);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, null));
        }

        [Fact]
        public void SaveStreamWithFormatAndOptions()
        {
            Stream stream = new MemoryStream();

            this.Image.Save(stream, this.formatNotRegistered.Object, this.encoderOptions);

            this.encoderNotInFormat.Verify(x => x.Encode<Color>(this.Image, stream, this.encoderOptions));
        }

        public void Dispose()
        {
            this.Image.Dispose();
        }
    }
}
