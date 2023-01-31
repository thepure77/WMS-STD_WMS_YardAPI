using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Business.Commons
{
    public static class ModelCondition
    {
        public static bool StringLike(string source, string data)
        {
            return (!string.IsNullOrEmpty(data)) ? (source ?? string.Empty).Contains(data) : true;
        }

        public static bool StringEqual(string source, string data)
        {
            return (!string.IsNullOrEmpty(data)) ? (source ?? string.Empty).Equals(data) : true;
        }

        public static bool DateTimeBetween(DateTime? source, DateTime? data, DateTime? data2, bool withoutTime = false)
        {
            if (data.HasValue && source.HasValue)
            {
                if (withoutTime)
                {
                    data = DateTime.Parse(data.Value.ToShortTimeString());
                    source = DateTime.Parse(source.Value.ToShortTimeString());
                }

                if (data2.HasValue)
                {
                    if (withoutTime)
                    {
                        data2 = DateTime.Parse(data2.Value.ToShortTimeString());
                    }

                    return source >= data && source <= data2;
                }
                else
                {
                    return source.Equals(data);
                }
            }
            else
            {
                return true;
            }
        }

        public static bool NullableEqual<T>(this T? source, T? data) where T : struct
        {
            return data.HasValue ? source.Equals(data) : true;
        }
    }
}
