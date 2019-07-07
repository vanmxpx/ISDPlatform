using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;

namespace Utility
{
    public static class FromFileHelper
    {
        public static byte[] GetBytes(this IFormFile file)
        {
            byte[] fileBytes = new byte[0];

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            return fileBytes;
        }

        public static bool IsImage(this IFormFile file)
        {
            return ImageHelper.IsImage(file.GetBytes());
        }

        public static Image ToImage(this IFormFile file)
        {
            return ImageHelper.ToImage(file.GetBytes());
        }
    }
}
