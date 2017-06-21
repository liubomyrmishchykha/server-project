using System;
using System.Reflection;
using Repository.QueryExecutor;
using System.Collections.Generic;

namespace Repository.DataMapper
{
    public class TypeInitializer
    {
        /// <summary>
        /// method create instance and set fields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="typeMetaData"></param>
        /// <param name="queryResult"></param>
        /// <returns>instance</returns>
        public static T Initialize<T>(IEnumerable<TypePropery> typeMetaData, QueryResult queryResult) where T : class, new()
        {

            T typeInstance = new T();
            Type type = typeInstance.GetType();
            PropertyInfo prop;

            if (queryResult.HasResult() && typeMetaData != null)
            {
                foreach (TypePropery typeProperty in typeMetaData)
                {
                    prop = type.GetProperty(typeProperty.PropertyName);       //get our property from object
                    var propertyValue = queryResult.GetItem(typeProperty.SqlName);  //get value from DB
                    if (propertyValue != DBNull.Value && propertyValue != null)
                    {
                        prop.SetValue(typeInstance, propertyValue, null);
                    }
                }
                return typeInstance;
            }
            else
                return null;
        }
    }
}
