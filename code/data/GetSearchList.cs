using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class GetSearchList
    {
        private static string ImageBaseUri = "https://image.tmdb.org/t/p/original";

        public async static Task GetData(ObservableCollection<Search> SearchList, string SearchString, int page = 0)
        {
            try
            {
                SearchResult Result;
                if (page == 0)
                {
                    Result = await GetSearchResultAsync(SearchString);
                }
                else
                {
                    Result = await GetSearchResultAsync(SearchString, page);
                }

                foreach (var item in Result.results)
                {
                    item.poster_path = ImageBaseUri + item.poster_path;
                    if (item.backdrop_path != null)
                    {
                        item.backdrop_path = ImageBaseUri + item.backdrop_path;
                    }
                    else
                    {
                        item.backdrop_path = item.poster_path;
                    }
                    SearchList.Add(item);
                }
            }
            catch(Exception)
            {
                return;
            }
        }

        /*
        public enum ResultState
        {
            Succeed,
            NotFind,
            Failed
        }
        */

        private async static Task<SearchResult> GetSearchResultAsync(string SearchString, int page = 0)

        //async表示此函数异步，当有异步操作时需要加上此关键字
        //Task<.>为异步获取的数据所使用的类型

        {
            var http = new HttpClient();

            //要使用HttpClient类需要通过如下步骤
            //在项目中打开 工具-Nut包管理器-管理解决方案的Nut包-浏览-搜索HttpClient
            //安装Microsoft.Net.Http

            string uri;
            if (page == 0)
            {
                uri = String.Format("https://api.themoviedb.org/3/search/movie?api_key=341386d68f259dc87b0f767d00f06bb2&query={0}", SearchString);
            }
            else
            {
                uri = String.Format("https://api.themoviedb.org/3/search/movie?api_key=341386d68f259dc87b0f767d00f06bb2&page={0}&query={1}", page, SearchString);
            }
                
            var response = await http.GetAsync(uri);

            //当使用异步方法的函数（名字中含有Async）时，使用关键词await

            var jsonMessage = await response.Content.ReadAsStringAsync();

            //同上一句

            var serializer = new DataContractJsonSerializer(typeof(SearchResult));

            //反序列化JSON序列
            //用Root类型向反序列类指明工作目标类

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            //反序列化JSON所需的内存流（MemoryStream）对象，其使用的编码格式为UTF8

            var data = (SearchResult)serializer.ReadObject(ms);

            //从流对象中取出数据，放入类实例中

            return data;
        }
    }
}
