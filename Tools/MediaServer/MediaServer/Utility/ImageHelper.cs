using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace Utility
{
    public static class ImageHelper
    {
        private static readonly SHA256 sha;

        static ImageHelper()
        {
            sha = SHA256.Create();
        }

        /// <summary> Returns image types. </summary>
        public static Dictionary<string, string> GetImageMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"}
            };
        }

        /// <summary> Returns the content type. </summary>
        /// <returns> Returns the content type of the image, if type is not the content type of the image, then returns null. </returns>
        public static string GetImageContentType(string path)
        {
            Dictionary<string, string> imageMimeTypes = GetImageMimeTypes();
            string extension = Path.GetExtension(path).ToLowerInvariant();
            return imageMimeTypes.ContainsKey(extension) ? imageMimeTypes[extension] : null;
        }

        /// <summary> Checks if an byte array is a image. </summary>
        /// <returns> Returns true if the byte array is image, otherwise returns false. </returns>
        public static bool IsImage(this byte[] data)
        {
            bool isImage;

            try
            {
                using (var imageReadStream = new MemoryStream(data))
                {
                    Image.Load<Rgb24>(imageReadStream);
                }
                isImage = true;
            }
            catch
            {
                isImage = false;
            }

            return isImage;
        }

        /// <summary> Converts a byte array to an image. </summary>
        /// <returns> Returns an image if the specified byte array is an image, otherwise returns null. </returns>
        public static Image<Rgb24> ToImage(this byte[] data)
        {
            Image<Rgb24> img;

            try
            {
                using (var imageReadStream = new MemoryStream(data))
                {
                    img = Image.Load<Rgb24>(imageReadStream);
                }
            }
            catch
            {
                img = null;
            }

            return img;
        }

        /// <summary> Converts an image into an array of bytes. </summary>
        /// <returns> An array of bytes corresponding to the image. </returns>
        public static byte[] ToByteArray(this Image<Rgb24> image)
        {
            byte[] imageBytes = new byte[0];

            using (var ms = new MemoryStream())
            {
                image.Save(ms, new JpegEncoder());
                imageBytes = ms.ToArray();
            }

            return imageBytes;
        }


        /// <summary> Resize image. </summary>
        /// <returns> Returns a new resizing image. </returns>
        /// <exception cref="System.ArgumentException"> If the image is null and the width or height is less than 1. </exception>
        /// <exception cref="System.ArgumentNullException"> If the image is null. </exception>
        public static void ResizeImage(this Image<Rgb24> image, int width, int height)
        {
            image.Mutate(ctx => ctx.Resize(width, height));
        }

        /// <summary> Returns the image hash using sha256 and base64 encoding. </summary>
        public static string GetHash(this Image<Rgb24> image)
        {
            byte[] dataImage = image.ToByteArray();
            byte[] hash = sha.ComputeHash(dataImage);
            string checksum = WebEncoders.Base64UrlEncode(hash);
            return checksum;
        }
    }
}
