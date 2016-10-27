using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class TMDBNowPlaying
    {
        public async static Task<NowPlaying> GetData()
        {
            var http = new HttpClient();
            var uri = "https://api.themoviedb.org/3/movie/now_playing?api_key=341386d68f259dc87b0f767d00f06bb2&language=en-US";
            var response = await http.GetAsync(uri);
            var jsonMessage = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(NowPlaying));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));
            var data = (NowPlaying)serializer.ReadObject(ms);
            return data;
        }

        [DataContract]
        public class Result
        {
            [DataMember]
            public string poster_path { get; set; }
            [DataMember]
            public bool adult { get; set; }
            [DataMember]
            public string overview { get; set; }
            [DataMember]
            public string release_date { get; set; }
            [DataMember]
            public List<string> genre_ids { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string original_title { get; set; }
            [DataMember]
            public string original_language { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public string backdrop_path { get; set; }
            [DataMember]
            public double popularity { get; set; }
            [DataMember]
            public string vote_count { get; set; }
            [DataMember]
            public bool video { get; set; }
            [DataMember]
            public double vote_average { get; set; }
        }

        [DataContract]
        public class Dates
        {
            [DataMember]
            public string maximum { get; set; }
            [DataMember]
            public string minimum { get; set; }
        }

        [DataContract]
        public class NowPlaying
        {
            [DataMember]
            public int page { get; set; }
            [DataMember]
            public List<Result> results { get; set; }
            [DataMember]
            public Dates dates { get; set; }
            [DataMember]
            public int total_pages { get; set; }
            [DataMember]
            public int total_results { get; set; }
        }
    }
}
