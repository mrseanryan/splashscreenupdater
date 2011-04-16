using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SplashScreenUpdater
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                ArgsProcessor argProc = new ArgsProcessor(args);

                if (!argProc.Validate())
                {
                    Console.WriteLine(argProc.GetUsage());
                    return 1;
                }

                Font font = new Font(argProc.GetArg(ArgsProcessor.ARG_FONT), argProc.GetArgAsInt(ArgsProcessor.ARG_FONT_SIZE), FontStyle.Regular);

                string text, bmpFilePath;
                bmpFilePath = argProc.GetArg(ArgsProcessor.ARG_BMP_PATH);
                text = argProc.GetArg(ArgsProcessor.ARG_TEXT);
                log("Writing text " + text + " on bitmap file at " + bmpFilePath);

                int iRed = argProc.GetArgAsInt(ArgsProcessor.ARG_COLOR_RED);
                int iGreen = argProc.GetArgAsInt(ArgsProcessor.ARG_COLOR_GREEN);
                int iBlue = argProc.GetArgAsInt(ArgsProcessor.ARG_COLOR_BLUE);
                Color color = Color.FromArgb(iRed, iGreen, iBlue);

                BmpTextWriter.WriteTextOnBmp(bmpFilePath, text, font,
                    argProc.GetArgAsInt(ArgsProcessor.ARG_FONT_SIZE),
                    new Point(argProc.GetArgAsInt(ArgsProcessor.ARG_POS_X), argProc.GetArgAsInt(ArgsProcessor.ARG_POS_Y)),
                    color
                    );
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace + "\n" + e.Message);
                return 2;
            }

            return 0;
        }

        private static void log(string text)
        {
            Console.WriteLine(text);
        }
    }
}
