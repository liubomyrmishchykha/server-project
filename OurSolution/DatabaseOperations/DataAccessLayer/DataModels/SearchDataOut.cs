using System;

namespace DataAccessLayer.DataModels
{
    public class SearchDataOut
    {
        /// <summary>
        ///  DAta model for  Search criteria wich is used in stored procedyre
        /// </summary>
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public string SearchBy { get; set; }
        public SearchParameterByFields SearchParameterByFields = new SearchParameterByFields();
        public string OrderbyField { get; set; }
        public bool OrderByReverse { get; set; }
    }
    public class SearchParameterByFields
    {
        public int? Id { get; set; }
        public string InstanceName { get; set; }
        public string HostName { get; set; }
        public DateTime? Added { get; set; }
        public DateTime? Modified { get; set; }
        public int? Status { get; set; }
        public int? AuthMode { get; set; }
        public string UserName { get; set; }
        public string Version { get; set; }
    }
}
