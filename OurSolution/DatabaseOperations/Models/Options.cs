using Models.Attributes;

namespace Models
{
    [DbTable(SqlName = "Options")]
    public class Options
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "Interval")]
        public int Interval { get; set; }
      
    }
}