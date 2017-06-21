using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models
{
    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public List<InstanceWithUserInfo> Instances { get; set; }
        [DataMember]
        public int Count { get; set; }

        public SearchResult(List<InstanceWithUserInfo> instances, int count)
        {
            this.Instances = instances;
            this.Count = count;
        }
    }
}
