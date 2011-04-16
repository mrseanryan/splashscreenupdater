using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace SplashScreenUpdater
{
    class ArgsProcessor
    {
        //TODO - replace these strings with Enum
        internal const string ARG_FONT = "FONT";
        internal const string ARG_FONT_SIZE = "FONT_SIZE";
        internal const string ARG_BMP_PATH = "BMP_PATH";
        internal const string ARG_TEXT = "ARG_TEXT";
        internal const string ARG_POS_X = "POS_X";
        internal const string ARG_POS_Y = "POS_Y";
        internal const string ARG_COLOR_RED = "COLOR_RED";
        internal const string ARG_COLOR_GREEN = "ARG_COLOR_GREEN";
        internal const string ARG_COLOR_BLUE = "ARG_COLOR_BLUE";

        Dictionary<string, int> dictArgsToPos = new Dictionary<string, int>();

        string[] args;

        internal ArgsProcessor(string[] args)
        {
            this.args = args;

            int iPos = 0;
            dictArgsToPos[ARG_FONT] = iPos++;
            dictArgsToPos[ARG_FONT_SIZE] = iPos++;
            dictArgsToPos[ARG_BMP_PATH] = iPos++;
            dictArgsToPos[ARG_TEXT] = iPos++;
            dictArgsToPos[ARG_POS_X] = iPos++;
            dictArgsToPos[ARG_POS_Y] = iPos++;
            dictArgsToPos[ARG_COLOR_RED] = iPos++;
            dictArgsToPos[ARG_COLOR_GREEN] = iPos++;
            dictArgsToPos[ARG_COLOR_BLUE] = iPos++;
        }

        internal string GetArg(string argName)
        {
            return args[dictArgsToPos[argName]];
        }

        internal int GetArgAsInt(string arg)
        {
            return Int32.Parse(GetArg(arg));
        }

        internal string GetUsage()
        {
            StringBuilder sb = new StringBuilder();

            AssemblyName asmName = this.GetType().Assembly.GetName();
            sb.Append(asmName.Name + " " + asmName.Version.ToString() + "\n" );

            sb.Append("Usage: ");

            foreach (string arg in dictArgsToPos.Keys)
            {
                sb.Append("<" + arg + "> ");
            }

            return sb.ToString();
        }

        internal bool Validate()
        {
            return args.Length == dictArgsToPos.Keys.Count;
        }
    }
}
