using System;

namespace Models.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public string SqlName { get; set; }
    }
}
