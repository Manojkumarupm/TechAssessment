using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;


namespace GeneralKnowledge.Test.App.Tests
{
    /// <summary>
    /// Image rescaling
    /// </summary>
    public class RescaleImageTest : ITest
    {
        public void Run()
        {
            // TODO
            // Grab an image from a public URL and write a function that rescales the image to a desired format
            // The use of 3rd party plugins is permitted
            // For example: 100x80 (thumbnail) and 1200x1600 (preview)
            Image I;
            int thumb_height = 100;
            int thumb_width = 80;
            string FilePath = ConfigurationManager.AppSettings["FilePath"];
            string FileName = "Image";
            try
            {
                string url = ConfigurationManager.AppSettings["URL"];
                RescaleImage(url, Path.Combine(FilePath,FileName), thumb_width, thumb_height, false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Image rescaling based on the aspect ratio and perform croping on the image if it passed as true
        /// </summary>
        /// <param name="imageUrl">Image URL</param>
        /// <param name="name">FileName with path</param>
        /// <param name="thumb_width">Width</param>
        /// <param name="thumb_height">Height</param>
        /// <param name="Crop">true/false</param>
        private void RescaleImage(string imageUrl, string name, int thumb_width, int thumb_height, bool Crop)
        {
            WebClient webClient = new WebClient();
            Stream stream = webClient.OpenRead(imageUrl);
            Image I = Image.FromStream(stream);
            I.Save(name + "_original.bmp");
            int imageWidth = I.Width;
            int imageHeight = I.Height;
            int originalWidth;
            int originalHeight;

            Bitmap O;
            Bitmap Out = new Bitmap(thumb_width, thumb_height);

            float aspect_ratio;
            aspect_ratio = (float)thumb_height / (float)imageHeight;
            originalHeight = thumb_height;
            originalWidth = (int)(Math.Floor(imageWidth * aspect_ratio));

            if (originalWidth < thumb_width)
            {
                aspect_ratio = (float)thumb_width / (float)originalWidth;
                originalWidth = thumb_width;
                originalHeight = (int)(Math.Floor(originalHeight * aspect_ratio));
            }
            O = new Bitmap(I, originalWidth, originalHeight);
            if (Crop)
            {
                int start_x = (originalWidth - thumb_width) / 2;
                int start_y = (originalHeight - thumb_height) / 2;
                if (start_x + thumb_width > originalWidth)
                    start_x = 0;
                if (start_y + thumb_height > originalHeight)
                    start_y = 0;
                Out = O.Clone(new Rectangle(start_x, start_y, thumb_width, thumb_height), Out.PixelFormat);
            }
            else
                Out = O;

            Out.Save(name + "_" + Out.Width + "x" + Out.Height + ".bmp");
        }
    }
}
