using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MovieCollection.data
{
    public class SQLiteOperation
    {
        private static string DB_NAME = "UserDB.db";
        private static SQLiteConnection conn;
        public enum OrderType
        {
            Title,
            CollectionDate,
            RatingSum,
            Year,
            Director
        }

        public static void GetDbConnection()
        {           
            string DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME);
            // 连接数据库，如果数据库文件不存在则创建一个空数据库。
            conn = new SQLiteConnection(new SQLitePlatformWinRT(), DbPath);
            // 创建 Person 模型对应的表，如果已存在，则忽略该操作。
            conn.CreateTable<CollectedMovie>();
        }

        public static CollectedMovie QueryData(string imdbID)
        {
            var movie = conn.Table<CollectedMovie>().Where(v => v.imdbID == imdbID);
            if (movie.Count() == 0)
            {
                return null;
            }
            else
            {
                return movie.First();
            }
        }

        public static List<CollectedMovie> QueryDataAll()
        {
            var movies = conn.Table<CollectedMovie>();
            return movies.ToList();
        }

        public static List<CollectedMovie> QueryDataByType(OrderType type)
        {
            TableQuery<CollectedMovie> movies;
            switch (type)
            {
                case OrderType.Title:
                    movies = conn.Table<CollectedMovie>().OrderBy(v => v.Title);
                    break;
                case OrderType.CollectionDate:
                    movies = conn.Table<CollectedMovie>().OrderBy(v => v.CollectionDate);
                    break;
                case OrderType.RatingSum:
                    movies = conn.Table<CollectedMovie>().OrderBy(v => v.RatingSum);
                    break;
                case OrderType.Year:
                    movies = conn.Table<CollectedMovie>().OrderBy(v => v.Year);
                    break;
                case OrderType.Director:
                    movies = conn.Table<CollectedMovie>().OrderBy(v => v.Director);
                    break;
                default:
                    movies = conn.Table<CollectedMovie>();
                    break;
            }
            return movies.ToList();
        }

        public static List<CollectedMovie> QueryDataByTag(string tag)
        {
            var movies = conn.Table<CollectedMovie>().Where(v => v.Tags.Contains(";" + tag + ";"));
            return movies.ToList();
        }

        public static void InsertData(CollectedMovie movie)
        {
            conn.Insert(movie);
        }
        
        public static void DeleteData(CollectedMovie movie)
        {
            conn.Delete(movie);
        }

        public static void UpdateData(CollectedMovie movie)
        {
            conn.Update(movie);
        }
    }
}
