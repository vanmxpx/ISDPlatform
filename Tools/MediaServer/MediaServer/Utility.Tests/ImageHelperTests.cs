using NUnit.Framework;
using System;
using System.Drawing;
using System.Text;

namespace Utility.Tests
{
    public class ImageHelperTests
    {
        private byte[] dataImage;

        [SetUp]
        public void Init()
        {
            string base64image = Properties.Resources.TestImage;
            dataImage = Convert.FromBase64String(base64image);
        }


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
            Assert.IsTrue(ImageHelper.IsImage(dataImage));
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
            Assert.IsTrue(ImageHelper.ToImage(dataImage) != null);
        }

        [Test]
        [TestCase(128, 128)]
        [TestCase(512, 1024)]
        [TestCase(2048, 4)]
        public void ResizeImageTest(int width, int height)
        {
            Image image = ImageHelper.ToImage(dataImage);
            Image resizedImage = ImageHelper.ResizeImage(image, width, height);
            Assert.IsTrue(resizedImage.Width == width && resizedImage.Height == height);
        }

        [Test]
        [TestCase(-128, 128)]
        [TestCase(512, 0)]
        public void ResizeImageExceptionTest1(int width, int height)
        {
            Image image = ImageHelper.ToImage(dataImage);
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
            Image image = ImageHelper.ToImage(dataImage);
            string hash = "JkOrCWhglSAZmJxfNxR7ym54cxrGRCi-twWcQvO8Ir8";
            string testHash = ImageHelper.GetHash(image);
            Assert.IsTrue(ImageHelper.GetHash(image) == hash);
        }
    }
}