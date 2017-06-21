using System;
namespace WcfServiceLibrary.DataModel
{

    public static class ConvertToNulable
    {
        /// <summary>
        /// Converts string to int with null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ToNullableInt(string s)
        {
            int result;
            if (string.IsNullOrWhiteSpace(s))
                return null;
            if (!int.TryParse(s, out result))
                throw new ArgumentException("Passed string value isn't a number");
            return result;
        }

        /// <summary>
        /// Converts string to DateTime with null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime? ToNullableDateTime(string s)
        {
            DateTime result;
            if (string.IsNullOrWhiteSpace(s))
                return null;
            if (!DateTime.TryParse(s, out result))
                throw new ArgumentException("Passed string has wrong format of date");
            return result;
        }
    }
}