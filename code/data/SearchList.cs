using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    [DataContract]
    public class Search
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string imdbID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Poster { get; set; }
    }

    [DataContract]
    public class SearchResult
    {
        [DataMember]
        public List<Search> Search { get; set; }
        [DataMember]
        public string totalResults { get; set; }
        [DataMember]
        public string Response { get; set; }
    }
}
