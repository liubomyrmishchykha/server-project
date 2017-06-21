using System;

namespace DataAccessLayer.DataModels
{
    public class InstanceWithUserInfo
    {
        public int Id { get; set; }
        public string HostName { get; set; }
        public string InstanceName { get; set; }
        public int Status { get; set; }
        public string Version { get; set; }
        public string Added { get; set; }
        public string Modified { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int AuthentificationMode { get; set; }
        public string Password { get; set; }

        public InstanceWithUserInfo() { }

        public InstanceWithUserInfo(int id, 
                                    string hostName, 
                                    string instanceName, 
                                    int status, 
                                    string version, 
                                    string added, 
                                    string modified, 
                                    int? userId, 
                                    string userName = null, 
                                    int authMode = 0,
                                    string userPass = null)
        {
            Id = id;
            HostName = hostName;
            InstanceName = instanceName;
            Status = status;
            Version = version;
            Added = added;
            Modified = modified;
            UserId = userId;
            UserName = userName;
            AuthentificationMode = authMode;
            Password = userPass;
        }
    }
}
