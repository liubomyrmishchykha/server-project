using System;
using Models.Attributes;

namespace Models
{
    /// <summary>
    ///  Data model for  Search criteria which is used in stored procedure
    /// </summary>
    public class SearchDataOut
    {
        [DbColumn(SqlName = "PageNumber")]
        public int CurrentPage { get; set; }

        [DbColumn(SqlName = "RecordsPerPage")]
        public int ItemsPerPage { get; set; }

        [DbColumn(SqlName = "IdSearch")]
        public int? Id { get; set; }

        [DbColumn(SqlName = "InstanceNameSearch")]
        public string InstanceName { get; set; }

        [DbColumn(SqlName = "HostNameSearch")]
        public string HostName { get; set; }

        [DbColumn(SqlName = "AddedFromSearch")]
        public DateTime? AddedFrom { get; set; }

        [DbColumn(SqlName = "AddedToSearch")]
        public DateTime? AddedTo { get; set; }

        [DbColumn(SqlName = "ModifiedFromSearch")]
        public DateTime? ModifiedFrom { get; set; }

        [DbColumn(SqlName = "ModifiedToSearch")]
        public DateTime? ModifiedTo { get; set; }

        [DbColumn(SqlName = "StatusSearch")]
        public int? Status { get; set; }

        [DbColumn(SqlName = "VersionSearch")]
        public string Version { get; set; }

        [DbColumn(SqlName = "RAMSearch")]
        public int? RAM { get; set; }

        [DbColumn(SqlName = "CPUCountSearch")]
        public int? CPUCount { get; set; }

        [DbColumn(SqlName = "UserNameSearch")]
        public string UserName { get; set; }

        [DbColumn(SqlName = "OrderByField")]
        public string OrderbyField { get; set; }

        [DbColumn(SqlName = "OrderBy")]
        public bool? OrderBy { get; set; }
    }
}
