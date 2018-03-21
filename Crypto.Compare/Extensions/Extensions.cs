using System;
using System.Collections.Generic;
using System.Text;

namespace Crypto.Compare.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Froms the unix time.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime FromUnixTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
        /// <summary>
        /// Froms the unix time.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime FromUnixTime(this long unixTimeStamp)
        {
            return FromUnixTime((double)unixTimeStamp);
        }

        /// <summary>
        /// Froms the unix time.
        /// </summary>
        /// <param name="unixTimeStamp">The unix time stamp.</param>
        /// <returns>DateTime.</returns>
        public static DateTime FromUnixTime(this string unixTimeStamp)
        {
            return FromUnixTime(int.Parse(unixTimeStamp));
        }
        /// <summary>
        /// To the unix time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>System.Int32.</returns>
        public static int ToUnixTime(this DateTime date)
        {
            return (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// To the ASCII.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string Ascii(this string value)
        {
            //string inputString = "Räksmörgås";
            return Encoding.ASCII.GetString(
                Encoding.Convert(
                    Encoding.UTF8,
                    Encoding.GetEncoding(
                        Encoding.ASCII.EncodingName,
                        new EncoderReplacementFallback(" "),
                        new DecoderExceptionFallback()
                        ),
                    Encoding.UTF8.GetBytes(value)
                )
            );
            // return System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(value));
        }
    }
}
