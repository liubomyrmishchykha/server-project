using System;
using Models.Attributes;

namespace Models
{
    public class BriefInstance
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "Modified")]
        public DateTime Modified { get; set; }

        [DbColumn(SqlName = "Status")]
        public int Status { get; set; }

        [DbColumn(SqlName = "UserID")]
        public int? UserID { get; set; }
    }
}