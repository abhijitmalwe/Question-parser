using System;
using System.IO;

namespace Xengine.Admin.Core
{
    public static class ImageConverter
    {
        public static string Base64String = null;

        public static string ImageToBase64(Stream stream)
        {
            byte[] imageBytes = stream.ToByteArray();
            Base64String = Convert.ToBase64String(imageBytes);
            return Base64String;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length;)
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }

        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}