using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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

        public static Image ToImage(byte[] data)
        {
            Image img = null;

            using (var imageReadStream = new MemoryStream(data))
            {
                img = Image.FromStream(imageReadStream);
            }

            return img;
        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}
