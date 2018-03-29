// ***********************************************************************
// Assembly         : Core.News
// Author           : mcarlucci
// Created          : 03-20-2018
//
// Last Modified By : mcarlucci
// Last Modified On : 03-20-2018
// ***********************************************************************
// <copyright file="MicrosecondEpochConverter.cs" company="Core.News">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.News.Converters
{
    /// <summary>
    /// Class EpochConverter.
    /// </summary>
    /// <seealso cref="Newtonsoft.Json.Converters.DateTimeConverterBase" />
    public class EpochConverter : DateTimeConverterBase
    {
        /// <summary>
        /// The epoch
        /// </summary>
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {           
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds + "000");
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (long.TryParse(reader.Value as string ?? "", out long result) == false)
                { return null; }
         
            return _epoch.AddSeconds(result).ToUniversalTime();
        }
    }

}
