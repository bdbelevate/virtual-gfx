using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace FormUrlEncoded
{
    public static class Serializer
    {
        public static async Task<string> SerializeAsync(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var keyValues = obj.GetPropertiesAsDictionary();

            var formUrlEncodedContent = new FormUrlEncodedContent(keyValues);

            return await formUrlEncodedContent.ReadAsStringAsync();
        }

        public static T Deserialize<T>(string formUrlEncodedText) where T : new()
        {
            if (string.IsNullOrEmpty(formUrlEncodedText))
                throw new ArgumentException("Form URL Encoded text was null or empty.", nameof(formUrlEncodedText));

            var pairs = formUrlEncodedText.Split('&');

            var obj = new T();
            foreach (var pair in pairs)
            {
                var nameValue = pair.Split('=');

                if (HasValue(nameValue))
                {
                    obj.SetProperty(nameValue[0], UnityWebRequest.UnEscapeURL(nameValue[1]));
                }
            }

            return obj;
        }

        private static bool HasValue(string[] nameValue)
        {
            return nameValue.Length == 2 && nameValue[1] != string.Empty;
        }
    }

    public static class ObjectExtensions
    {
        public static void SetProperty(this object source, string propertyName, object propertyValue)
        {
            var pi = source.GetProperty(propertyName);

            if (pi != null && pi.CanWrite)
            {
                pi.SetValue(source, propertyValue, null);
            }
        }

        public static PropertyInfo GetProperty(this object source, string propertyName)
        {
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            return source.GetType().GetProperty(propertyName, bindingFlags);
        }

        public static IDictionary<string, string> GetPropertiesAsDictionary(this object source)
        {
            var dict = new Dictionary<string, string>();

            if (source == null)
                return dict;

            var properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var property in properties)
            {
                var value = property.GetValue(source);

                if (value == null)
                    continue;

                dict.Add(property.Name, value.ToString());
            }

            return dict;
        }
    }
}