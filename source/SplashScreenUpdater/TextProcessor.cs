using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SplashScreenUpdater
{
    /// <summary>
    /// Allow us to explicitly request a particular character, even from DOS which may have keyboard setup for US etc.
    /// 
    /// </summary>
    class TextProcessor
    {
        private readonly Encoding iso88591;

        internal TextProcessor()
            : this("ISO8859-1")
        {
        }

        internal TextProcessor(string encodingName)
        {
            iso88591 = Encoding.GetEncoding("ISO8859-1");
        }

        internal string ConvertFromNumber(int iNum)
        {
            char[] chars = iso88591.GetChars(new byte[] { (byte)iNum });

            return "" + (chars[0]);
        }

        /// <summary>
        /// Convert from standard ISO8859-1 Entity Number string, as used in HTML.
        /// Example: &#169; = '©'
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        internal string ConvertFromEntityNumberString(string entity)
        {
            string regPat = "&#([0-9]{1,3});";
            Regex reg = new Regex(regPat);
            if(reg.IsMatch(entity))
            {
                GroupCollection groups = reg.Match(entity).Groups;
                string entityNumAsString = groups[groups.Count - 1].Value;
                int iEntityNum = Int32.Parse(entityNumAsString);

                return ConvertFromNumber(iEntityNum);
            }

            return entity;
        }

        /// <summary>
        /// parse the given string, converting ALL ISO8859-1 entity number strings
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal void ConvertAllEntities(ref string text)
        {
            string regPat = "(?:&#[0-9]{1,3};)+";
            Regex reg = new Regex(regPat);
            if (reg.IsMatch(text))
            {
                MatchCollection mc = reg.Matches(text);
                foreach (Match mat in mc)
                {
                    string converted = ConvertFromEntityNumberString(mat.Value);
                    text = text.Replace(mat.Value, converted);
                }
            }

            convertTemplateValues(ref text);
        }

        /// <summary>
        /// converts template values, like ${this}
        /// </summary>
        /// <param name="text"></param>
        private void convertTemplateValues(ref string text)
        {
            //YYYY is the current year:
            string template = buildTemplateString("YYYY");

            string yearString = DateTime.Now.Year.ToString();

            text = text.Replace(template, yearString);
        }

        private string buildTemplateString(string temp)
        {
            return "${" + temp + "}";
        }
    }
}
