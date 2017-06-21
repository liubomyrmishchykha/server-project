using Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Repository.DataMapper
{
    public class TypeMetadataProvider
    {
        /// <summary>
        /// Return collection of TypePropery (witch describe type)
        /// </summary>
        /// <typeparam name="T">Type witch discovery</typeparam>
        /// <returns>Ienumerable Type properties</returns>
        public static IEnumerable<TypePropery> GetPropertiesSqlName<T>()
        {
            Type type = typeof(T);
            return type.GetProperties()
                 .Select(x => new TypePropery
                 {
                     SqlName = Attribute.IsDefined(x, typeof(DbColumnAttribute))
                         ? x.GetCustomAttribute<DbColumnAttribute>().SqlName
                         : x.Name,
                     PropertyName = x.Name
                 });
        }

        /// <summary>
        /// Look for SqlName
        /// </summary>
        /// <typeparam name="T">Type of class with parse</typeparam>
        /// <returns> DbTableAttribute.SqlName or Class Name </returns>
        public static string GetTypeSqlName<T>()
        {
            DbTableAttribute instanceAttribute = GetTypeAttribute<T, DbTableAttribute>();
            if (instanceAttribute != null)
            {
                return instanceAttribute.SqlName;
            }
            else
            {
                return typeof(T).Name;
            }
        }

        /// <summary>
        /// look for Attribute TAtribute in type T
        /// </summary>
        /// <typeparam name="T">Type of class with parse</typeparam>
        /// <typeparam name="TAtribute"> Attribute witch try to find </typeparam>
        /// <returns> instance of type or null </returns>
        private static TAtribute GetTypeAttribute<T, TAtribute>() where TAtribute : Attribute
        {
            TAtribute instanceAttribute = typeof(T)
                .GetCustomAttributes(
                    typeof(TAtribute),
                    true
                ).FirstOrDefault() as TAtribute;
            return instanceAttribute;
        }
    }
}
