using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace SplashScreenUpdater
{
    class BmpTextWriter
    {
        internal static void WriteTextOnBmp(
            string pathToBmp, 
            string text, 
            Font font, 
            int iFontSize, 
            Point pos, 
            Color color
            )
        {
            //backup the image:
            string backupPath = pathToBmp + ".bak";
            File.Copy(pathToBmp, backupPath, true);

            string tempPath = Path.GetTempFileName();

            //Load the Image to be written on.
            using (Bitmap bitMapImage = new Bitmap(pathToBmp))
            {
                using (Graphics graphicImage = Graphics.FromImage(bitMapImage))
                {
                    //Smooth graphics is nice.
                    graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

                    //Write your text.
                    SolidBrush br = new SolidBrush(color);

                    graphicImage.DrawString(text, font, br, pos);
                    //graphicImage.DrawString(text, font, SystemBrushes.WindowText, pos);

                    //save the image
                    bitMapImage.Save(tempPath);
                }
            }

            File.Copy(tempPath, pathToBmp, true);
        }
    }
}
