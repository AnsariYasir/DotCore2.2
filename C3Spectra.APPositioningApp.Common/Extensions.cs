using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace C3Spectra.APPositioningApp.Common
{
    public static class Extensions
    {
        
        public static DataTable ToDataTable<T>(this IList<T> data, string tableName)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                //table.Columns.Add(prop.Name, prop.PropertyType);
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(values);
            }
            table.TableName = string.IsNullOrEmpty(tableName) ? "Table1" : tableName;
            return table;
        }

        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                T tempT = new T();
                var tType = tempT.GetType();
                List<T> list = new List<T>();
                foreach (var row in table.Rows.Cast<DataRow>())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        var propertyInfo = tType.GetProperty(prop.Name);

                        // Make best effort - if column not found in table, skip it.
                        if (table.Columns.Contains(prop.Name))
                        {
                            var rowValue = row[prop.Name];
                            var t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                            try
                            {
                                object safeValue = (rowValue == null || DBNull.Value.Equals(rowValue)) ? null : Convert.ChangeType(rowValue, t);
                                propertyInfo.SetValue(obj, safeValue, null);

                            }
                            catch
                            {//this write exception to my logger
                             //_logger.Error(ex.Message);
                            }
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
                throw new ArgumentNullException("target");
            if (source == null)
                throw new ArgumentNullException("source");
            foreach (var element in source)
                target.Add(element);
        }

        public static void ToCSV<T>(this IList<T> list, string strFilePath)
        {
            DataTable dtDataTable = list.ToDataTable("Table1");
            dtDataTable.ToCSV(strFilePath);
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            using (StreamWriter sw = new StreamWriter(strFilePath, false))
            {
                //headers  
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    sw.Write(dtDataTable.Columns[i]);
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
                foreach (DataRow dr in dtDataTable.Rows)
                {
                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            if (value.Contains(','))
                            {
                                value = String.Format("\"{0}\"", value);
                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(dr[i].ToString());
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
        }

        #region - Validations -

        public static bool HasSomething(this string str, string considerEmptyIf = null)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            if (str.Trim() == string.Empty)
            {
                return false;
            }
            if (considerEmptyIf != null && str.Trim().Equals(considerEmptyIf, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static bool IsNothing(this string str, string considerNothingIf = null)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                return true;
            }
            if (str.Trim() == string.Empty)
            {
                return true;
            }
            if (considerNothingIf != null && str.Trim().Equals(considerNothingIf, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public static bool ValidateNotNullOrEmpty(this string str, string nameOfParam)
        {
            if (str.IsNothing())
            {
                throw new ArgumentNullException(nameOfParam, $"{nameOfParam} cannot be null or empty.");
            }
            return true;
        }

        public static bool ValidateNotZero(this int intVal, string nameOfParam)
        {
            if (intVal == 0)
            {
                throw new ArgumentNullException(nameOfParam, $"{nameOfParam} cannot be zero.");
            }
            return true;
        }

        public static bool ValidateNotNullOrDefault(this Guid obj, string nameOfParam)
        {
            if (obj == null || obj == default(Guid))
            {
                throw new ArgumentNullException(nameOfParam, $"{nameOfParam} cannot be zero.");
            }
            return true;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties

            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in Props)

            {

                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);

            }

            foreach (T item in items)

            {

                var values = new object[Props.Length];

                for (int i = 0; i < Props.Length; i++)

                {

                    //inserting property values to datatable rows

                    values[i] = Props[i].GetValue(item, null);

                }

                dataTable.Rows.Add(values);

            }

            //put a breakpoint here and check datatable

            return dataTable;

        }

        public static DataTable ClassToDatatable<T>() where T : class
        {
            Type classType = typeof(T);
            DataTable result = new DataTable(classType.UnderlyingSystemType.Name);

            foreach (PropertyInfo property in classType.GetProperties())
            {
                DataColumn column = new DataColumn();
                column.ColumnName = property.Name;
                column.DataType = property.PropertyType;

                if (IsNullableType(column.DataType) && column.DataType.IsGenericType)
                {   // If Nullable<>, this is how we get the underlying Type...
                    column.DataType = column.DataType.GenericTypeArguments.FirstOrDefault();
                }
                else
                {   // True by default, so set it false
                    column.AllowDBNull = false;
                }

                // Add column
                result.Columns.Add(column);
            }
            return result;
        }


        public static DataTable ConvertClassToDataTable<T>(T entity) where T : class
        {
            var properties = typeof(T).GetProperties();
            var table = new DataTable();

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            table.Rows.Add(properties.Select(p => p.GetValue(entity, null)).ToArray());
            return table;
        }

        public static bool IsNullableType(Type Input)
        {
            if (!Input.IsValueType) return true; // Reference Type
            if (Nullable.GetUnderlyingType(Input) != null) return true; // Nullable<T>
            return false;   // Value Type
        }
        #endregion - Validations -

        #region - DateTime -

        public static DateTime ToUtcIgnoreDst(this DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                throw new ArgumentException("DateTime object already in UTC");
            }
            var dt = dateTime + TimeZoneInfo.Local.BaseUtcOffset;
            DateTime.SpecifyKind(dt, DateTimeKind.Utc);
            return dt;
        }

        public static DateTime ToLocalIgnoreDst(this DateTime dateTime)
        {
            if (dateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("DateTime object is not in UTC");
            }
            var dt = dateTime + TimeZoneInfo.Local.BaseUtcOffset;
            DateTime.SpecifyKind(dt, DateTimeKind.Local);
            return dt;
        }

        public static string ConvertToISO8601Format(this DateTimeOffset dateTime)
        {
            return dateTime.ToString("yyyymmddThhmmss+|-hhmm");
        }

        #endregion - DateTime -

        #region - Serialization -

        /// <summary>
        /// Serializes the given object to to Xml. Ignores the xml start tag and namespaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeToXml<T>(this object obj) where T : class
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            string xml = null;
            using (var sw = new StringWriter())
            {
                XmlWriter writer = XmlWriter.Create(sw, settings);

                XmlSerializerNamespaces names = new XmlSerializerNamespaces();
                names.Add("", "");

                XmlSerializer cs = new XmlSerializer(typeof(T));
                cs.Serialize(writer, obj, names);
                xml = sw.ToString();
            }
            return xml;
        }

        /// <summary>
        /// Deserailizes a string to the object of given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static T Deserialize<T>(this string xml) where T : class
        {
            T obj = null;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringReader reader = new StringReader(xml))
            {
                obj = (T)serializer.Deserialize(reader);
            }

            return obj;
        }

        #endregion - Serialization -

        #region - JToken -

        //public static List<JToken> FindTokens(this JToken containerToken, string name)
        //{
        //    List<JToken> matches = new List<JToken>();
        //    //FindTokens(containerToken, name, matches);//commented by Suhel
        //    return matches;
        //}

        #region commented by Suhel
        //private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        //{
        //    if (containerToken.Type == JTokenType.Object)
        //    {
        //        foreach (JProperty child in containerToken.Children<JProperty>())
        //        {
        //            if (child.Name == name)
        //            {
        //                matches.Add(child.Value);
        //            }
        //            FindTokens(child.Value, name, matches);
        //        }
        //    }
        //    else if (containerToken.Type == JTokenType.Array)
        //    {
        //        foreach (JToken child in containerToken.Children())
        //        {
        //            FindTokens(child, name, matches);
        //        }
        //    }
        //}

        #endregion

        #endregion - JToken -

        #region - Dictionary -

        public static int GetIndexOfDictionaryKey<T, K>(this Dictionary<T, K> dictionary, dynamic key)
        {
            for (int index = 0; index < dictionary.Count; index++)
            {
                if (dictionary.Skip(index).First().Key == key)
                    return index;
            }

            return -1;
        }
      


        /// <summary>
        /// Adds an item to the dictionary without throwing the duplicate key error; replaces the value if key already exists.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>Returns true if the item was added as new and false if the item was found and value was replaced with new one.</returns>
        public static bool SafeAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
                return false;
            }
            else
            {
                dictionary.Add(key, value);
                return true;
            }
        }

        #endregion - Dictionary -

        #region - Enums -

        public static T ToEnum<T>(this string value) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result;
            return System.Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }

        #endregion - Enums -

        #region - String -

        public static T ConvertTo<T>(this string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        #endregion - String -
    }
    public static class SessionExtensions
    {
        public static T GetComplexData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        
           
        public static void SetComplexData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
