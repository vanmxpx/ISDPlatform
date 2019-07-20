﻿using NUnit.Framework;
using System;
using System.Drawing;

namespace Utility.Tests
{
    public class ImageHelperTests
    {
        [Test]
        [TestCase(@"..\..\..\Resources\default.png", "image/png")]
        [TestCase(@"..\..\..\Resources\default.jpg", "image/jpeg")]
        [TestCase(@"..\..\..\Resources\default.jpeg", "image/jpeg")]
        public void GetImageContentTypeTest1(string path, string expected)
        {
            string imageContentType = ImageHelper.GetImageContentType(path);
            Assert.IsTrue(imageContentType == expected);
        }

        [Test]
        [TestCase(@"..\..\..\Resources\default.gif")]
        [TestCase(@"sdjfsjdfjsdjf")]
        [TestCase(@"321\Images\....////\\default.gif")]
        public void GetImageContentTypeTest2(string path)
        {
            string imageContentType = ImageHelper.GetImageContentType(path);
            Assert.IsTrue(string.IsNullOrEmpty(imageContentType));
        }

        [Test]
        [TestCase(null)]
        [TestCase(new byte[] { 1, 3, 3, 7 })]
        public void IsNotImageTest(byte[] data)
        {
            Assert.IsFalse(ImageHelper.IsImage(data));
        }

        [Test]
        public void IsImageTest()
        {
            byte[] data = Properties.Resources.TestImage;
            Assert.IsTrue(ImageHelper.IsImage(data));
        }

        [Test]
        [TestCase(null)]
        public void ToImageTest1(byte[] data)
        {
            Assert.IsTrue(ImageHelper.ToImage(data) == null);
        }

        [Test]
        public void ToImageTest2()
        {
            byte[] data = Properties.Resources.TestImage;
            Assert.IsTrue(ImageHelper.ToImage(data) != null);
        }

        [Test]
        [TestCase(128, 128)]
        [TestCase(512, 1024)]
        [TestCase(2048, 4)]
        public void ResizeImageTest(int width, int height)
        {
            byte[] data = Properties.Resources.TestImage;
            Image image = ImageHelper.ToImage(data);
            Image resizedImage = ImageHelper.ResizeImage(image, width, height);
            Assert.IsTrue(resizedImage.Width == width && resizedImage.Height == height);
        }

        [Test]
        [TestCase(-128, 128)]
        [TestCase(512, 0)]
        public void ResizeImageExceptionTest1(int width, int height)
        {
            byte[] data = Properties.Resources.TestImage;
            Image image = ImageHelper.ToImage(data);
            Assert.Throws<ArgumentException>(() =>
            {
                Image resizedImage = ImageHelper.ResizeImage(image, width, height);
            });
        }

        [Test]
        [TestCase(128, 128)]
        [TestCase(512, 0)]
        public void ResizeImageExceptionTest2(int width, int height)
        {
            Image resizedImage = null;

            try
            {
                resizedImage = ImageHelper.ResizeImage(null, width, height);
            }
            catch { }

            Assert.IsTrue(resizedImage == null);
        }

        [Test]
        public void GetHashCodeTest()
        {
            byte[] data = Properties.Resources.TestImage;
            Image image = ImageHelper.ToImage(data);
            string hash = "JkOrCWhglSAZmJxfNxR7ym54cxrGRCi-twWcQvO8Ir8";
            Assert.IsTrue(ImageHelper.GetHash(image) == hash);
        }
    }
}