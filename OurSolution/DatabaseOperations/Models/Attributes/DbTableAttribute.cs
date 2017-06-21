using System;

namespace Models.Attributes
{
    public class DbTableAttribute : Attribute
    {
        public string SqlName { get; set; }
    }
}
