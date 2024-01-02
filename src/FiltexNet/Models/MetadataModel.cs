using System;
using System.Text.Json.Serialization;
using FiltexNet.Constants;

namespace FiltexNet.Models
{
    public class Metadata
    {
        [JsonPropertyName("fields")]
        public Field[] Fields { get; set; }


        public FieldType GetFieldType(string str)
        {
            foreach (var item in Fields)
            {
                if (string.Equals(item.Name, str, StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Label, str, StringComparison.InvariantCultureIgnoreCase))
                {
                    return FieldType.ParseFieldType(item.Type);
                }
            }

            return FieldType.FieldTypeUnknown;
        }

        public string GetFieldName(string str)
        {
            foreach (var item in Fields)
            {
                if (string.Equals(item.Name, str, StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Label, str, StringComparison.InvariantCultureIgnoreCase))
                {
                    return item.Name;
                }
            }

            return str;
        }

        public Lookup[] GetFieldValues(string str)
        {
            foreach (var item in Fields)
            {
                if (string.Equals(item.Name, str, StringComparison.InvariantCultureIgnoreCase) ||
                    string.Equals(item.Label, str, StringComparison.InvariantCultureIgnoreCase))
                {
                    return item.Values;
                }
            }

            return new Lookup[] { };
        }
    }
}