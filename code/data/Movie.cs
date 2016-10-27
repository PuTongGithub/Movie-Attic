using SQLite.Net.Attributes;
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
    [DataContract]
    public class Movie
    {
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string Rated { get; set; }
        [DataMember]
        public string Released { get; set; }
        [DataMember]
        public string Runtime { get; set; }
        [DataMember]
        public string Genre { get; set; }
        [DataMember]
        public string Director { get; set; }
        [DataMember]
        public string Writer { get; set; }
        [DataMember]
        public string Actors { get; set; }
        [DataMember]
        public string Plot { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public string Awards { get; set; }
        [DataMember]
        public string Poster { get; set; }
        [DataMember]
        public string Metascore { get; set; }
        [DataMember]
        public string imdbRating { get; set; }
        [DataMember]
        public string imdbVotes { get; set; }
        [DataMember]
        public string imdbID { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Response { get; set; }
    }

    public class CollectedMovie : Movie
    {
        [PrimaryKey, AutoIncrement]
        public int CollectionID { get; set; }

        public string FilmCritic { get; set; }
        public string CollectionDate { get; set; }
        public string Tags { get; set; }
        public string RatingSum { get; set; }
        public string RatingPicture { get; set; }
        public string RatingStory { get; set; }
        public string RatingMusic { get; set; }
        public string RatingPerformance { get; set; }

        public CollectedMovie() { }

        public CollectedMovie(Movie movie)
        {
            Title = movie.Title;
            Year = movie.Year;
            Rated = movie.Rated;
            Released = movie.Released;
            Runtime = movie.Runtime;
            Genre = movie.Genre;
            Director = movie.Director;
            Writer = movie.Writer;
            Actors = movie.Actors;
            Plot = movie.Plot;
            Language = movie.Language;
            Country = movie.Country;
            Awards = movie.Awards;
            Poster = movie.Poster;
            Metascore = movie.Metascore;
            imdbRating = movie.imdbRating;
            imdbVotes = movie.imdbVotes;
            imdbID = movie.imdbID;
            Type = movie.Type;
            Response = movie.Response;            
        }
    }
}
