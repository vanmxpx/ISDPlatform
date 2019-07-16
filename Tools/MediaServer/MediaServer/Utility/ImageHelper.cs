using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

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
                    Image.FromStream(imageReadStream);
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
        public static Image ToImage(this byte[] data)
        {
            Image img;

            try
            {
                using (var imageReadStream = new MemoryStream(data))
                {
                    img = Image.FromStream(imageReadStream);
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
        public static byte[] ToByteArray(this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }

        /// <summary> Resize image. </summary>
        /// <returns> Returns a new resizing image. </returns>
        /// <exception cref="System.ArgumentException"> If the image is null and the width or height is less than 1. </exception>
        /// <exception cref="System.ArgumentNullException"> If the image is null. </exception>
        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            return new Bitmap(image, width, height);
        }

        /// <summary> Returns the image hash using sha256 and base64 encoding. </summary>
        public static string GetHash(this Image image)
        {
            byte[] hash = sha.ComputeHash(image.ToByteArray());
            string checksum = WebEncoders.Base64UrlEncode(hash);
            return checksum;
        }
    }
}
