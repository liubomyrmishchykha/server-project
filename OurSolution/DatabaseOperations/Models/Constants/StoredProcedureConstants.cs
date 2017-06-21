namespace Models.Constants
{
    public class StoredProcedureConstants
    {
        public const string ProcedureNamePattern = "usp{0}{1}";
        public const string ParameterNamePattern = "@{0}";

        public const string GetAll = "GetAll";
        public const string SelectById = "SelectById";
        public const string Add = "Add";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string Exists = "Exists";
        public const string Count = "Count";
        public const string Search = "Search";
        public const string SelectByInstanceId = "SelectByInstanceId";
        public const string SelectByDatabaseIds = "SelectByDatabaseIds";
    }
}
