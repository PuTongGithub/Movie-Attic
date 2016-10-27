using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class HomePageData
    {
        public string DataTitle { get; set; }
        public ObservableCollection<Movie> MovieList { get; set; }
    }

    public class GetHomePageDataList
    {
        public static void SetDataList(ObservableCollection<HomePageData> DataList)
        {
            var TopTenDatas = new HomePageData();
            TopTenDatas.DataTitle = "IMDB Top 10";
            TopTenDatas.MovieList = new ObservableCollection<Movie>();
            DataList.Add(TopTenDatas);

            var NowPlayingDatas = new HomePageData();
            NowPlayingDatas.DataTitle = "Now Playing";
            NowPlayingDatas.MovieList = new ObservableCollection<Movie>();
            DataList.Add(NowPlayingDatas);

            var PopularDatas = new HomePageData();
            PopularDatas.DataTitle = "Popular";
            PopularDatas.MovieList = new ObservableCollection<Movie>();
            DataList.Add(PopularDatas);

            var UpcomingDatas = new HomePageData();
            UpcomingDatas.DataTitle = "Upcoming";
            UpcomingDatas.MovieList = new ObservableCollection<Movie>();
            DataList.Add(UpcomingDatas);
        }

        public static async void GetDataList(ObservableCollection<HomePageData> DataList)
        {
            try
            {
                GetTopTenList(DataList[0].MovieList);

                var NowPlayingResult = await TMDBNowPlaying.GetData();
                GetNowPlayingDatas(DataList[1].MovieList, NowPlayingResult);

                var PopularResult = await TMDBPopular.GetData();
                GetPopularDatas(DataList[2].MovieList, PopularResult);

                var UpcomingResult = await TMDBUpcoming.GetData();
                GetUpcomingDatas(DataList[3].MovieList, UpcomingResult);
            }
            catch { }
        }

        public static async void GetNowPlayingDatas(ObservableCollection<Movie> MovieList,TMDBNowPlaying.NowPlaying Results)
        {
            for (int i = 0, j = 0; i < 10; j++)
            {
                try
                {
                    var Tmovie = await TMDBdata.GetData(Results.results[j].id.ToString());
                    var Omovie = await GetMovie.GetData(Tmovie.imdb_id);
                    if (Omovie != null)
                    {
                        i++;
                        MovieList.Add(Omovie);
                    }

                }
                catch { }
            }
        }

        public static async void GetPopularDatas(ObservableCollection<Movie> MovieList, TMDBPopular.Popular Results)
        {
            for (int i = 0, j = 0; i < 10; j++)
            {
                try
                {
                    var Tmovie = await TMDBdata.GetData(Results.results[j].id.ToString());
                    var Omovie = await GetMovie.GetData(Tmovie.imdb_id);
                    if (Omovie != null)
                    {
                        i++;
                        MovieList.Add(Omovie);
                    }

                }
                catch { }
            }
        }

        public static async void GetUpcomingDatas(ObservableCollection<Movie> MovieList, TMDBUpcoming.Upcoming Results)
        {
            for (int i = 0, j = 0; i < 10; j++)
            {
                try
                {
                    var Tmovie = await TMDBdata.GetData(Results.results[j].id.ToString());
                    var Omovie = await GetMovie.GetData(Tmovie.imdb_id);
                    if (Omovie != null)
                    {
                        i++;
                        MovieList.Add(Omovie);
                    }

                }
                catch { }
            }
        }

        public static async void GetTopTenList(ObservableCollection<Movie> TopTenList)
        {
            List<string> TopTenID = TopTen();
            foreach (var id in TopTenID)
            {
                try
                {
                    Movie Data;
                    Data = await GetMovie.GetData(id);
                    TopTenList.Add(Data);
                }
                catch { }
            }
        }

        private static List<string> TopTen()
        {
            var TopTen = new List<string>();

            TopTen.Add("tt0111161");
            TopTen.Add("tt0068646");
            TopTen.Add("tt0071562");
            TopTen.Add("tt0468569");
            TopTen.Add("tt0108052");

            TopTen.Add("tt0110912");
            TopTen.Add("tt0167260");
            TopTen.Add("tt0060196");
            TopTen.Add("tt0137523");
            TopTen.Add("tt0050083");

            return TopTen;
        }
    }
}
