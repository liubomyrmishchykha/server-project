using Models.Attributes;
using System;

namespace DataCollector
{
    public class BriefDatabase
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "Name")]
        public string Name { get; set; }

        [DbColumn(SqlName = "CreateTime")]
        public DateTime CreateTime { get; set; }

        [DbColumn(SqlName = "TotalSize")]
        public int TotalSize { get; set; }

        [DbColumn(SqlName = "DbState")]
        public int DbState { get; set; }

        [DbColumn(SqlName = "InstanceId")]
        public int InstanceId { get; set; }
    }
}
