using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Models;

namespace WcfServiceLibrary
{
    [ServiceContract()]
    public interface IWcfService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "Instances", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Instance> GetInstanceList();

        [OperationContract]
        [WebInvoke(UriTemplate = "Users", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<User> GetAllUsers();

        [OperationContract()]
        [WebInvoke(UriTemplate = "Instance/{Id}", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        InstanceWithUserInfo GetInstansById(string Id);

        [OperationContract()]
        [WebInvoke(UriTemplate = "UserUpdate", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateUser(User user);

        [OperationContract()]
        [WebInvoke(UriTemplate = "UserDelete/{Id}", Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void DeleteUser(string Id);

        [OperationContract()]
        [WebInvoke(UriTemplate = "UserAdd", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void AddUser(User user);

        [OperationContract()]
        [WebInvoke(UriTemplate = "Search", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SearchResult GetInstansByData(SearchDataIn search);


        [OperationContract()]
        [WebInvoke(UriTemplate = "User/{Id}", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        User GetUserById(string Id);

        [OperationContract()]
        [WebInvoke(UriTemplate = "OptionsUpdate", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void UpdateOptions(Options options);

        [OperationContract()]
        [WebInvoke(UriTemplate = "Options", Method = "GET", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Options GetOption();
    }
}

