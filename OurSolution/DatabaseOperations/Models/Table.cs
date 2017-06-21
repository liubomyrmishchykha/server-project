using Models.Attributes;

namespace Models
{
    [DbTable(SqlName = "Tables")]
    public class Table
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "Name")]
        public string Name { get; set; }

        [DbColumn(SqlName = "ColumnCount")]
        public int ColumnCount { get; set; }

        [DbColumn(SqlName = "DatabaseId")]
        public int DatabaseId { get; set; }
    }
}
