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
    public class TMDBdata
    {
        private static string PosterImageBaseUri = "https://image.tmdb.org/t/p/original";
        public async static Task<TmdbMovieDetails> GetData(string ID)
        {
            var http = new HttpClient();
            var uri = String.Format("https://api.themoviedb.org/3/movie/{0}?api_key=341386d68f259dc87b0f767d00f06bb2", ID);
            var response = await http.GetAsync(uri);

            var jsonMessage = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(TmdbMovieDetails));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));
            var data = (TmdbMovieDetails)serializer.ReadObject(ms);

            data.poster_path = PosterImageBaseUri + data.poster_path;
            return data;
        }

        [DataContract]
        public class BelongsToCollection
        {
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public string poster_path { get; set; }
            [DataMember]
            public string backdrop_path { get; set; }
        }

        [DataContract]
        public class ProductionCompany
        {
            [DataMember]
            public string name { get; set; }
            [DataMember]
            public int id { get; set; }
        }

        [DataContract]
        public class Genre
        {
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string name { get; set; }
        }

        [DataContract]
        public class ProductionCountry
        {
            [DataMember]
            public string iso_3166_1 { get; set; }
            [DataMember]
            public string name { get; set; }
        }

        [DataContract]
        public class SpokenLanguage
        {
            [DataMember]
            public string iso_639_1 { get; set; }
            [DataMember]
            public string name { get; set; }
        }

        [DataContract]
        public class TmdbMovieDetails
        {
            [DataMember]
            public bool adult { get; set; }
            [DataMember]
            public string backdrop_path { get; set; }
            [DataMember]
            public BelongsToCollection belongs_to_collection { get; set; }
            [DataMember]
            public string budget { get; set; }
            [DataMember]
            public List<Genre> genres { get; set; }
            [DataMember]
            public string homepage { get; set; }
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public string imdb_id { get; set; }
            [DataMember]
            public string original_language { get; set; }
            [DataMember]
            public string original_title { get; set; }
            [DataMember]
            public string overview { get; set; }
            [DataMember]
            public double popularity { get; set; }
            [DataMember]
            public string poster_path { get; set; }
            [DataMember]
            public List<ProductionCompany> production_companies { get; set; }
            [DataMember]
            public List<ProductionCountry> production_countries { get; set; }
            [DataMember]
            public string release_date { get; set; }
            [DataMember]
            public string revenue { get; set; }
            [DataMember]
            public string runtime { get; set; }
            [DataMember]
            public List<SpokenLanguage> spoken_languages { get; set; }
            [DataMember]
            public string status { get; set; }
            [DataMember]
            public string tagline { get; set; }
            [DataMember]
            public string title { get; set; }
            [DataMember]
            public bool video { get; set; }
            [DataMember]
            public string vote_average { get; set; }
            [DataMember]
            public string vote_count { get; set; }
        }
    }
}

