using MediaServer.Services;
using NUnit.Framework;
using System;
using System.Drawing;
using System.Threading.Tasks;
using Utility;

namespace MediaServer.Tests
{
    public class ImageRepositoryTest
    {
        private IImageRepository _imageRepository;

        [SetUp]
        public void Init()
        {
            _imageRepository = new MockImageRepository();
        }

        [Test]
        public async Task AddAndGetImageAsyncTest1()
        {
            await _imageRepository.AddImageAsync(ImageHelper.ToImage(Properties.Resources.TestImage));
            Image image = await _imageRepository.GetImageAsync(ImageHelper.ToImage(Properties.Resources.TestImage).GetHash());
            Assert.IsTrue(image != null);
        }
    }
}
