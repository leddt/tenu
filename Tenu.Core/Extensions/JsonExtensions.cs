using System;
using Newtonsoft.Json.Linq;

namespace Tenu.Core.Extensions
{
    internal static class JsonExtensions
    {
        public static string GetRequiredString(this JObject jObject, string propertyName)
        {
            var prop = jObject.GetRequiredProperty(propertyName);
            return prop.GetStringValue();
        }

        public static JObject GetRequiredObject(this JObject jObject, string propertyName)
        {
            var prop = jObject.GetRequiredProperty(propertyName);
            return prop.GetObjectValue();
        }

        public static JProperty GetRequiredProperty(this JObject jObject, string propertyName)
        {
            if (!jObject.ContainsKey(propertyName))
                throw new Exception($"Required property {propertyName} is missing.");

            var prop = jObject.Property(propertyName);

            return prop;
        }

        public static string GetOptionalString(this JObject jObject, string propertyName)
        {
            if (!jObject.ContainsKey(propertyName))
                return null;

            var prop = jObject.Property(propertyName);
            if (prop.Type == JTokenType.Null)
                return null;

            return prop.GetStringValue();
        }

        public static bool GetOptionalBoolean(this JObject jObject, string propertyName, bool defaultValue = false)
        {
            if (!jObject.ContainsKey(propertyName))
                return defaultValue;

            var prop = jObject.Property(propertyName);
            if (prop.Type == JTokenType.Null)
                return defaultValue;

            return prop.GetBooleanValue();
        }

        public static string GetStringValue(this JProperty prop)
        {
            prop.EnsureType(JTokenType.String);
            return prop.Value.Value<string>();
        }

        public static bool GetBooleanValue(this JProperty prop)
        {
            prop.EnsureType(JTokenType.Boolean);
            return prop.Value.Value<bool>();
        }

        public static JObject GetObjectValue(this JProperty prop)
        {
            prop.EnsureType(JTokenType.Object);
            return prop.Value.Value<JObject>();
        }

        public static void EnsureType(this JProperty prop, JTokenType expectedType)
        {
            if (prop.Value.Type != expectedType)
                throw new Exception($"Property {prop.Name} expected {expectedType} but got {prop.Value.Type}.");
        }
    }
}