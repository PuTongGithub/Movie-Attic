using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
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
        public int budget { get; set; }
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
        public int revenue { get; set; }
        [DataMember]
        public int runtime { get; set; }
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
        public int vote_average { get; set; }
        [DataMember]
        public int vote_count { get; set; }
    }
}

