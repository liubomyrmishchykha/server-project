using System;
using Models.Attributes;
using System.Collections.Generic;

namespace Models
{
    public class InstanceWithUserInfo
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "InstanceName")]
        public string InstanceName { get; set; }

        [DbColumn(SqlName = "HostName")]
        public string HostName { get; set; }

        [DbColumn(SqlName = "Added")]
        public DateTime Added { get; set; }

        [DbColumn(SqlName = "Modified")]
        public DateTime? Modified { get; set; }

        [DbColumn(SqlName = "Status")]
        public int Status { get; set; }

        [DbColumn(SqlName = "Version")]
        public string Version { get; set; }

        [DbColumn(SqlName = "RAM")]
        public int? RAM { get; set; }

        [DbColumn(SqlName = "CPUCount")]
        public int? CPUCount { get; set; }

        [DbColumn(SqlName = "UserId")]
        public int? UserId { get; set; }

        [DbColumn(SqlName = "Name")]
        public string UserName { get; set; }

        [DbColumn(SqlName = "Database")]
        public List<Database> Databases { get; set; }
    }
}
