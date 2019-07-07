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
        public static Dictionary<string, string> GetImageMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"}
            };
        }

        public static string GetImageContentType(string path)
        {
            Dictionary<string, string> imageMimeTypes = GetImageMimeTypes();
            string extension = Path.GetExtension(path).ToLowerInvariant();
            return imageMimeTypes[extension];
        }

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

        public static Image ToImage(this byte[] data)
        {
            Image img = null;

            using (var imageReadStream = new MemoryStream(data))
            {
                img = Image.FromStream(imageReadStream);
            }

            return img;
        }

        public static byte[] ToByteArray(this Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            return new Bitmap(image, width, height);
        }

        public static string GetHash(this Image image)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(image.ToByteArray());
                string checksum = WebEncoders.Base64UrlEncode(hash);
                return checksum;
            }
        }
    }
}
