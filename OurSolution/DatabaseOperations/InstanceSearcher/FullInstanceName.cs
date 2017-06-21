namespace InstanceSearcher
{
    class FullInstanceName
    {
        public FullInstanceName(string hostname, string instancename)
        {
            HostName = hostname;
            InstanceName = instancename;
        }
        public string HostName { get; set; }
        public string InstanceName { get; set; }
    }
}
