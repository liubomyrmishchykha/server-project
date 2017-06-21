using System;
using Repository.QueryExecutor;
using System.Collections.Generic;

namespace Repository.DataMapper
{
    class DataMapper
    {
        public static T Map<T>(QueryResult queryResult) where T : class, new()
        {
            if (queryResult == null)
            {
                throw new ArgumentNullException("queryResult");
            }

            IEnumerable<TypePropery> typeMetaData = TypeMetadataProvider.GetPropertiesSqlName<T>();   //get type data
            T instance = TypeInitializer.Initialize<T>(typeMetaData, queryResult);

            return instance;
        }

        public static List<T> MapList<T>(QueryResult queryResult) where T : class, new()
        {
            if (queryResult == null)
            {
                throw new ArgumentNullException("queryResult");
            }

            List<T> instanceList = new List<T>();

            IEnumerable<TypePropery> typeMetaData = TypeMetadataProvider.GetPropertiesSqlName<T>();   //get type data

            if (queryResult.HasResult())
            {
                do
                {
                    T instance = TypeInitializer.Initialize<T>(typeMetaData, queryResult);
                    instanceList.Add(instance);
                }
                while (queryResult.Read());
            }
            return instanceList;
        }
    }
}
