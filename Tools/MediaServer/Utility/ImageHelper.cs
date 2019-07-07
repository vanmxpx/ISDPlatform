using System.Collections.Generic;
using System.Drawing;
using System.IO;

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

        public static bool IsImage(byte[] data)
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
    }
}
