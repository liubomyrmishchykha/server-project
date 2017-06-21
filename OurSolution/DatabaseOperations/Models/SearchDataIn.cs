using System.Runtime.Serialization;

namespace Models
{
    /// <summary>
    /// Data model for input Search criteria 
    /// </summary>
    [DataContract]
    public class SearchDataIn
    {
        [DataMember(Name = "currentPage")]
        public int CurrentPage { get; set; }

        [DataMember(Name = "pageItems")]
        public int ItemsPerPage { get; set; }

        [DataMember(Name = "filterByFields")]
        public FilterByFields FilterByFields { get; set; }

        [DataMember(Name = "orderbyField")]
        public string OrderbyField { get; set; }

        [DataMember(Name = "orderByReverse")]
        public bool OrderByReverse { get; set; }
    }
}

