using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class GetMovie
    {                
        public async static Task<Movie> GetData(string imdbID)

        //async表示此函数异步，当有异步操作时需要加上此关键字
        //Task<.>为异步获取的数据所使用的类型

        {
            var http = new HttpClient();

            //要使用HttpClient类需要通过如下步骤
            //在项目中打开 工具-Nut包管理器-管理解决方案的Nut包-浏览-搜索HttpClient
            //安装Microsoft.Net.Http

            var uri = String.Format("http://www.omdbapi.com/?i={0}&plot=full&r=json", imdbID);
            var response = await http.GetAsync(uri);

            //当使用异步方法的函数（名字中含有Async）时，使用关键词await

            var jsonMessage = await response.Content.ReadAsStringAsync();

            //同上一句

            var serializer = new DataContractJsonSerializer(typeof(Movie));

            //反序列化JSON序列
            //用Root类型向反序列类指明工作目标类

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            //反序列化JSON所需的内存流（MemoryStream）对象，其使用的编码格式为UTF8

            var data = (Movie)serializer.ReadObject(ms);

            //从流对象中取出数据，放入类实例中

            uri = String.Format("http://img.omdbapi.com/?i={0}&apikey=c0150b20&h=1000", imdbID);

            data.Poster = uri;

            return data;
        }        
    }    
}
