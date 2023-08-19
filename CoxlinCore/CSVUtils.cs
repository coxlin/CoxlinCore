/**********************************************************                                                                                
CoxlinCore - Copyright (c) 2023 Lindsay Cox / MIT License                                                                                                         
**********************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace CoxlinCore
{
    public  static class CSVUtils
    {
        /// <summary>
        /// Not for use on update loops etc as uses
        /// Reflection underneath for value T so will not be the fastest possible solution
        /// but is quite flexible
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static T[] ParseCSVFile<T>(string csvFilePath)
        {
            var lines = File.ReadAllLines(csvFilePath);
            var lineCount = lines.Length;
            var properties = typeof(T).GetProperties();
            var propertyCount = properties.Length;
            var result = new List<T>();

            for (int i = 1; i < lineCount; ++i) //skip the first one
            {
                var line = lines[i];
                var values = line.Split(',');
                if (values.Length != properties.Length)
                {
                    throw new InvalidOperationException("CSV columns do not match object properties");
                }

                var instance = Activator.CreateInstance<T>();
                for (int j = 0; j < propertyCount; ++j)
                {
                    var property = properties[j];
                    var value = Convert.ChangeType(values[j], property.PropertyType, CultureInfo.InvariantCulture);
                    property.SetValue(instance, value);
                }

                result.Add(instance);
            }

            return result.ToArray();
        }
        
        public static void WriteCsvFile<T>(T[] data, string filePath, Action onSuccess, Action<string> onError)
        {
            try
            {
                var dataCount = data.Length;
                using var writer = new StreamWriter(filePath);
                var properties = typeof(T).GetProperties();
                var propertyCount = properties.Length;
                var header = JoinWithComma(GetPropertyNames(properties));
                writer.WriteLine(header);

                for (int i = 0; i < dataCount; ++i)
                {
                    var item = data[i];
                    var values = new string[propertyCount];
                
                    for (var j = 0; j < properties.Length; ++j)
                    {
                        var property = properties[j];
                        var value = property.GetValue(item);
                        values[j] = value.ToString();
                    }

                    var line = JoinWithComma(values);
                    writer.WriteLine(line);
                }
                onSuccess();
            }
            catch (Exception e)
            {
                onError(e.ToString());
                throw;
            }
        }
        
        private static string JoinWithComma(string[] values)
        {
            var sb = new StringBuilder();
            var isFirst = true;

            for (var i = 0; i < values.Length; ++i)
            {
                var value = values[i];
                if (!isFirst)
                {
                    sb.Append(',');
                }

                sb.Append(value);
                isFirst = false;
            }

            return sb.ToString();
        }
        
        private static string[] GetPropertyNames(PropertyInfo[] properties)
        {
            var propertyNames = new string[properties.Length];

            for (int i = 0; i < properties.Length; ++i)
            {
                propertyNames[i] = properties[i].Name;
            }

            return propertyNames;
        }
    }
}