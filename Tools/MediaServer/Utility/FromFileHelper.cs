using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;

namespace Utility
{
    public static class FromFileHelper
    {
        /// <summary> Returns the byte array corresponding to the file. </summary>
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

        /// <summary> Checks if a file is an image. </summary>
        public static bool IsImage(this IFormFile file)
        {
            return file.GetBytes().IsImage();
        }

        /// <summary> Converts a file to an image. </summary>
        public static Image ToImage(this IFormFile file)
        {
            return file.GetBytes().ToImage();
        }
    }
}
