using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class GetHomePageDataList
    {
        public async static Task GetTopTenList(ObservableCollection<Movie> TopTenList)
        {
            try
            {
                List<string> TopTenID = TopTen();
                foreach (var id in TopTenID)
                {
                    Movie Data;
                    Data = await GetMovie.GetData(id);
                    TopTenList.Add(Data);
                }
            }
            catch (Exception)
            {
                return;
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
