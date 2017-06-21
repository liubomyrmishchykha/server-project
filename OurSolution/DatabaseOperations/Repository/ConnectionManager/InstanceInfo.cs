namespace Repository.ConnectionManager
{
    public class InstanceInfo
    {
        public int Id { get; set; }
        public string HostName { get; set; }
        public string InstanceName { get; set; }        
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
