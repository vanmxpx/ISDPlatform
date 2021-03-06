﻿using MediaServer.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Utility;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MediaServer.Tests
{
    public class ImageRepositoryTest
    {
        private IImageRepository _imageRepository;
        private byte[] dataImage;

        [SetUp]
        public void Init()
        {
            _imageRepository = new MockImageRepository();
            string base64image = Properties.Resources.TestImage;
            dataImage = Convert.FromBase64String(base64image);
        }

        [Test]
        public async Task AddAndGetImageAsyncTest1()
        {
            await _imageRepository.AddImageAsync(ImageHelper.ToImage(dataImage));
            Image<Rgb24> image = await _imageRepository.GetImageAsync(ImageHelper.ToImage(dataImage).GetHash());
            Assert.IsTrue(image != null);
        }
    }
}
