namespace DataCollector
{
    public static class CollectionQueries
    {
        public const string BaseInstanceInfoQuery =
            @"SELECT SERVERPROPERTY('MachineName') AS HostName, 
SERVERPROPERTY('InstanceName') AS InstanceName,
SERVERPROPERTY('ProductVersion') AS Version, 
sysinfo.cpu_count AS CPUCount,
memory.total_physical_memory_kb AS RAM
FROM sys.dm_os_sys_info AS sysinfo, 
sys.dm_os_sys_memory AS memory";

        public const string ListOfDatabasesQuery =
            @"SELECT sysdb.name AS DBName, 
sysdb.create_date AS CreationDate,
sysdb.state AS DbState,
sysmast.total_size_mb AS TotalSize 
FROM sys.databases AS sysdb 
JOIN (SELECT CAST(SUM(size) * 8. / 1024 AS int) AS total_size_mb, 
database_id FROM sys.master_files GROUP BY database_id) AS sysmast
on sysdb.database_id=sysmast.database_id";


        public const string ListOfTablesQuery =
            @"SELECT 
tbl.name AS TableName,
count(clmn.name) AS ColumnCount
FROM sys.tables AS tbl
JOIN sys.columns AS clmn
ON tbl.object_id = clmn.object_id
GROUP BY tbl.name";
    }
}