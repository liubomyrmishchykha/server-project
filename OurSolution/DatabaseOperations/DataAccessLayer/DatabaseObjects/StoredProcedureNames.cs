namespace DataAccessLayer.DatabaseObjects
{
    public static class StoredProcedureNames
    {
        public const string AddUser = "dbo.uspAddUsers";
        public const string UpdateUser = "dbo.uspUpdateUsers";
        public const string GetAllUsers = "dbo.uspGetAllUsers";
        public const string DeleteUser = "dbo.uspDeleteUsers";
        public const string GetUserById = "dbo.uspSelectByIdUsers";
        public const string AddInstance = "dbo.uspAddInstances";
        public const string UpdateInstance = "dbo.uspUpdateInstances";
        public const string DeleteInstance = "dbo.uspDeleteInstances";
        public const string GetAllInstances = "dbo.uspGetAllInstances";
        public const string GetInstanceById = "dbo.uspSelectByIdInstances";
        public const string InstancesCount = "dbo.uspCountInstances";
        public const string InstanceExists = "dbo.uspExistsInstances";
        public const string SearchInstancesByInstanceName = "dbo.uspSearchByInstanceNameInstances";
        public const string GetAllInstancesWithUserInfo = "dbo.uspGetAllInstancesWithUserInfo";
        public const string GetInstancesWithUserInfoPaginationAndSearch = "dbo.uspGetInstancesWithUserInfoPaginationAndSearch";

    }
}