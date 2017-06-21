namespace PrototypeAppforScheduling.Models
{
    static public class CollectionQueries
    {

        public const string BaseInstanceInfoQuery =
            @"SELECT SERVERPROPERTY('MachineName') as HostName, SERVERPROPERTY('InstanceName') as InstanceName,
SERVERPROPERTY('ProductVersion') as Version, sysinfo.cpu_count as CPUCount, memory.total_physical_memory_kb as RAM
FROM sys.dm_os_sys_info as sysinfo, sys.dm_os_sys_memory as memory";

        public const string ListOfDatabasesQuery =
            "SELECT name AS DBName, create_date AS CreationDate FROM sys.databases where state_desc='ONLINE'";

        public const string ListOfTablesQuery = "SELECT name AS 'TableName' FROM sys.objects WHERE Type = 'U'";
    }
}