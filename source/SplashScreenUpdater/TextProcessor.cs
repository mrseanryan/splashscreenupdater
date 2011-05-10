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
        internal string ConvertFromISO8859_1_number(int iNum)
        {
            char[] chars = System.Text.ASCIIEncoding.ASCII.GetChars(new byte[]{(byte)iNum});

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
                string entityNumAsString = reg.Match(entity).Groups[0].Value;
                int iEntityNum = Int32.Parse(entityNumAsString);

                return ConvertFromISO8859_1_number(iEntityNum);
            }

            return entity;
        }

        /// <summary>
        /// parse the given string, converting ALL ISO8859-1 entity number strings
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        internal string ConvertAllEntities(string text)
        {
            string regPat = "(?:#[0-9]{1,3};)+";
            Regex reg = new Regex(regPat);
            if (reg.IsMatch(text))
            {
                MatchCollection mc = reg.Matches(text);
                foreach (Match mat in mc)
                {
                    string converted = ConvertFromEntityNumberString(mat.Value);
                    text.Replace(mat.Value, converted);
                }
            }

            return text;
        }
    }
}
