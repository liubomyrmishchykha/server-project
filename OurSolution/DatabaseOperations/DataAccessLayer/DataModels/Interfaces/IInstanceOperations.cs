using System.Collections.Generic;

namespace DataAccessLayer.DataModels.Interfaces
{
    public interface IInstanceOperations : IOperations<Instance>
    {
        int Count();
        bool Exists(string hostName, string instnaceName);
        List<Instance> SearchByInstanceName(string searchString, int pageNumber, int instancesPerPage, string orderBy);
        List<InstanceWithUserInfo> GetAllWithUserInfo();
        List<InstanceWithUserInfo> GetWithUserInfo(SearchDataOut searchData);
    }
}
