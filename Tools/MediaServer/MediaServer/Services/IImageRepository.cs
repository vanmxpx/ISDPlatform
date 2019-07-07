﻿using System.Drawing;
using System.Threading.Tasks;

namespace MediaServer.Services
{
    public interface IImageRepository
    {
        Task<Image> GetImageAsync(string path);
        Task<string> AddImageAsync(Image image);
    }
}
