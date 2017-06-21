using Models.Attributes;

namespace Models
{
    [DbTable(SqlName = "Users")]
    public class User
    {
        [DbColumn(SqlName = "Id")]
        public int Id { get; set; }

        [DbColumn(SqlName = "Name")]
        public string Name { get; set; }

        [DbColumn(SqlName = "Password")]
        public string Password { get; set; }
    }
}
