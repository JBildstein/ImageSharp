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
        private readonly Mock<IFileSystem> fileSystem;
        //private readonly Mock<IImageFormat> format;
        //private readonly Mock<IImageFormat> formatNotRegistered;
        //private readonly Mock<IImageDecoder> decoder;
        //private readonly Mock<IImageDecoder> decoderNotInFormat;
        private readonly IDecoderOptions decoderOptions;
        private Image<Color> returnImage;
        private Mock<IImageDecoder> localDecoder;
        private Mock<IImageFormat> localFormat;

        //public Configuration Configuration { get; private set; }
        //public IDisposable UnitOrWork { get; private set; }
        public Configuration LocalConfiguration { get; private set; }
        public byte[] Marker { get; private set; }
        public MemoryStream DataStream { get; private set; }

        //public byte[] imageData;

        public ImageLoadTests()
        {
            //this.imageData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            this.returnImage = new Image<Color>(1, 1);
            //this.decoder = new Mock<IImageDecoder>();
            //this.format = new Mock<IImageFormat>();
            //this.format.Setup(x => x.Decoder).Returns(this.decoder.Object);
            //this.format.Setup(x => x.Encoder).Returns(new Mock<IImageEncoder>().Object);
            //this.format.Setup(x => x.MimeType).Returns("img/test");
            //this.format.Setup(x => x.Extension).Returns("png1");
            //this.format.Setup(x => x.HeaderSize).Returns(this.imageData.Length);
            //this.format.Setup(x => x.IsSupportedFileFormat(this.imageData)).Returns(true);
            //this.format.Setup(x => x.SupportedExtensions).Returns(new string[] { "png1", "jpg1" });
            //this.decoder.Setup(x => x.Decode<Color>(It.IsAny<Stream>(), It.IsAny<IDecoderOptions>())).Returns(this.returnImage);

            //this.decoderNotInFormat = new Mock<IImageDecoder>();
            //this.formatNotRegistered = new Mock<IImageFormat>();
            //this.formatNotRegistered.Setup(x => x.Decoder).Returns(this.decoderNotInFormat.Object);
            //this.formatNotRegistered.Setup(x => x.Encoder).Returns(new Mock<IImageEncoder>().Object);
            //this.formatNotRegistered.Setup(x => x.MimeType).Returns("img/test");
            //this.formatNotRegistered.Setup(x => x.Extension).Returns("png1");
            //this.formatNotRegistered.Setup(x => x.HeaderSize).Returns(1);
            //this.formatNotRegistered.Setup(x => x.IsSupportedFileFormat(It.IsAny<byte[]>())).Returns(true);
            //this.formatNotRegistered.Setup(x => x.SupportedExtensions).Returns(new string[] { "png1", "jpg1" });
            //this.decoderNotInFormat.Setup(x => x.Decode<Color>(It.IsAny<Stream>(), It.IsAny<IDecoderOptions>())).Returns(this.returnImage);

            this.localDecoder = new Mock<IImageDecoder>();
            this.localFormat = new Mock<IImageFormat>();
            this.localFormat.Setup(x => x.Decoder).Returns(this.localDecoder.Object);
            this.localFormat.Setup(x => x.Encoder).Returns(new Mock<IImageEncoder>().Object);
            this.localFormat.Setup(x => x.MimeType).Returns("img/test");
            this.localFormat.Setup(x => x.Extension).Returns("png");
            this.localFormat.Setup(x => x.HeaderSize).Returns(1);
            this.localFormat.Setup(x => x.IsSupportedFileFormat(It.IsAny<byte[]>())).Returns(true);
            this.localFormat.Setup(x => x.SupportedExtensions).Returns(new string[] { "png", "jpg" });
            this.localDecoder.Setup(x => x.Decode<Color>(It.IsAny<Stream>(), It.IsAny<IDecoderOptions>())).Returns(this.returnImage);

            this.fileSystem = new Mock<IFileSystem>();

            //this.Configuration = new Configuration(this.format.Object)
            //{
            //    FileSystem = this.fileSystem.Object
            //};
            this.LocalConfiguration = new Configuration(this.localFormat.Object)
            {
                FileSystem = this.fileSystem.Object
            };
            TestFormat.RegisterGloablTestFormat();
            this.Marker = Guid.NewGuid().ToByteArray();
            this.DataStream = TestFormat.GlobalTestFormat.CreateStream(this.Marker);
            this.decoderOptions = new Mock<IDecoderOptions>().Object;
        }

        [Fact]
        public void LoadFromStream()
        {
            Image img = Image.Load(this.DataStream);

            Assert.NotNull(img);
            Assert.Equal(TestFormat.GlobalTestFormat, img.CurrentImageFormat);

            TestFormat.GlobalTestFormat.VerifyDecodeCall(this.Marker, null);
        }

        [Fact]
        public void LoadFromStreamWithType()
        {
            Image<Color> img = Image.Load<Color>(this.DataStream);

            Assert.NotNull(img);
            Assert.Equal(TestFormat.GlobalTestFormat.Sample<Color>(), img);
            Assert.Equal(TestFormat.GlobalTestFormat, img.CurrentImageFormat);
            TestFormat.GlobalTestFormat.VerifyDecodeCall(this.Marker, null);
        }

        [Fact]
        public void LoadFromStreamWithOptions()
        {
            Image img = Image.Load(this.DataStream, this.decoderOptions);

            Assert.NotNull(img);
            Assert.Equal(TestFormat.GlobalTestFormat, img.CurrentImageFormat);
            TestFormat.GlobalTestFormat.VerifyDecodeCall(this.Marker, this.decoderOptions);
        }

        [Fact]
        public void LoadFromStreamWithTypeAndOptions()
        {
            Image<Color> img = Image.Load<Color>(this.DataStream, this.decoderOptions);

            Assert.NotNull(img);
            Assert.Equal(TestFormat.GlobalTestFormat.Sample<Color>(), img);
            Assert.Equal(TestFormat.GlobalTestFormat, img.CurrentImageFormat);
            TestFormat.GlobalTestFormat.VerifyDecodeCall(this.Marker, this.decoderOptions);
        }

        [Fact]
        public void LoadFromStreamWithConfig()
        {
            Stream stream = new MemoryStream();
            Image img = Image.Load(stream, this.LocalConfiguration);

            Assert.NotNull(img);
            Assert.Equal(this.localFormat.Object, img.CurrentImageFormat);
            this.localDecoder.Verify(x => x.Decode<Color>(stream, null));
        }

        [Fact]
        public void LoadFromStreamWithTypeAndConfig()
        {
            Stream stream = new MemoryStream();
            Image<Color> img = Image.Load<Color>(stream, this.LocalConfiguration);

            Assert.NotNull(img);
            Assert.Equal(this.returnImage, img);
            Assert.Equal(this.localFormat.Object, img.CurrentImageFormat);
            this.localDecoder.Verify(x => x.Decode<Color>(stream, null));
        }

        [Fact]
        public void LoadFromStreamWithConfigAndOptions()
        {
            Stream stream = new MemoryStream();
            Image img = Image.Load(stream, this.decoderOptions, this.LocalConfiguration);

            Assert.NotNull(img);
            Assert.Equal(this.localFormat.Object, img.CurrentImageFormat);
            this.localDecoder.Verify(x => x.Decode<Color>(stream, this.decoderOptions));
        }

        [Fact]
        public void LoadFromStreamWithTypeAndConfigAndOptions()
        {
            Stream stream = new MemoryStream();
            Image<Color> img = Image.Load<Color>(stream, this.decoderOptions, this.LocalConfiguration);

            Assert.NotNull(img);
            Assert.Equal(this.returnImage, img);
            Assert.Equal(this.localFormat.Object, img.CurrentImageFormat);
            this.localDecoder.Verify(x => x.Decode<Color>(stream, this.decoderOptions));
        }

        public void Dispose()
        {
            // clean up the global object;
            this.returnImage?.Dispose();
        }
    }
}
