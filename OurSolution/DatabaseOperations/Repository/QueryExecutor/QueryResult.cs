using System.Data.SqlClient;

namespace Repository.QueryExecutor
{
    public class QueryResult
    {
        SqlDataReader _reader;

        public QueryResult(SqlDataReader reader)
        {
            _reader = reader;
        }
        
        public bool Read()
        {
            return _reader.Read();
        }

        public bool NextResult()
        {
            if (_reader.HasRows)
            {
                return (_reader.NextResult());
            }
            else
                return false;
        }

        //Getting value of current column
        public object GetItem(string columnName)
        {
            if (HasColumn(columnName))
            {
                return _reader[columnName];
            }
            else
            {
                return null;
            }
        }
        
        private bool HasColumn(string columnName)
        {
            for (int i = 0; i < _reader.FieldCount; i++)
            {
                if (_reader.GetName(i) == columnName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasResult()
        {
            return _reader.HasRows;
        }

        public int RowCount()
        {
            int count;
            if (_reader.HasRows)
            {
                string rowCount = _reader["RowsCount"].ToString();
                return int.TryParse(rowCount, out count) ? count : 0;
            }
            else
            {
                return 0;
            }           
        }

        public int GetId()
        {
            int id;
            return int.TryParse(_reader["Id"].ToString(), out id) ? id : 0;
        }
    }
}


