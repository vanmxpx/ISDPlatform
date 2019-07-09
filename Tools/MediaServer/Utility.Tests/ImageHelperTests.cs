using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Utility.Tests
{
    public class ImageHelperTests
    {
        [Test]
        [TestCase(@"E:\Images\default.png")]
        [TestCase(@"E:\Images\default.jpg")]
        [TestCase(@"E:\Images\default.jpeg")]
        public void GetImageContentTypeTest1(string path)
        {
            string imageContentType = ImageHelper.GetImageContentType(path);
            Dictionary<string, string> imageMimeTypes = ImageHelper.GetImageMimeTypes();
            string extension = Path.GetExtension(path).ToLowerInvariant();
            Assert.IsTrue(imageContentType == imageMimeTypes[extension]);
        }

        [Test]
        [TestCase(@"E:\Images\default.gif")]
        [TestCase(@"sdjfsjdfjsdjf")]
        [TestCase(@"E:\Images\....////\\default.gif")]
        public void GetImageContentTypeTest2(string path)
        {
            string imageContentType = ImageHelper.GetImageContentType(path);
            Assert.IsTrue(string.IsNullOrEmpty(imageContentType));
        }

        [Test]
        [TestCase(null)]
        [TestCase(new byte[] { 1, 3, 3, 7 })]
        public void IsImageTest1(byte[] data)
        {
            Assert.IsFalse(ImageHelper.IsImage(data));
        }

        [Test]
        public void IsImageTest2()
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
        public void ResizeImageTest1(int width, int height)
        {
            byte[] data = Properties.Resources.TestImage;
            Image image = ImageHelper.ToImage(data);
            Image resizedImage = ImageHelper.ResizeImage(image, width, height);
            Assert.IsTrue(resizedImage.Width == width && resizedImage.Height == height);
        }

        [Test]
        [TestCase(-128, 128)]
        [TestCase(512, 0)]
        public void ResizeImageTest2(int width, int height)
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
        public void ResizeImageTest3(int width, int height)
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
        public void GetHashCodeTest1()
        {
            byte[] data = Properties.Resources.TestImage;
            Image image = ImageHelper.ToImage(data);
            Assert.IsTrue(ImageHelper.GetHash(image) == ImageHelper.GetHash(image));
        }
    }
}
