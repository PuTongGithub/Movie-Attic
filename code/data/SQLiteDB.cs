using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollection.data
{
    public class SQLiteDB
    {
        private static String DB_NAME = "UserData.db";
        private static String TABLE_NAME = "MovieTable";
        private static String TABLE_DATA_TAPE = " (Title TEXT,"
            + " Year TEXT," + " Rated TEXT," + " Released TEXT," + " Runtime TEXT,"
            + " Genre TEXT," + " Director TEXT," + " Writer TEXT," + " Actors TEXT,"
            + " Plot TEXT," + " Language TEXT," + " Country TEXT," + " Awards TEXT,"
            + " Poster BLOB," + " MetaScore TEXT," + " imdbRating TEXT," + " imdbVotes TEXT,"
            + " imdbID TEXT," + " CollectionID INTEGER," + " CollectionDate TEXT," + " Tags TEXT,"
            + " RatingSum REAL," + " RatingPicture REAL," + " RatingStory REAL," + " RatingMusic REAL,"
            + " RatingPerformance REAL)";

        private static String SQL_CREATE_TABLE = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + TABLE_DATA_TAPE;
        private static String SQL_INSERT = "INSERT INTO " + TABLE_NAME + " VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";
        private static String SQL_QUERY_VALUE = "SELECT Value FROM " + TABLE_NAME + " WHERE (?) = (?);";
        private static String SQL_UPDATE = "UPDATE " + TABLE_NAME + " SET Value = ? WHERE Key = ?";
        private static String SQL_DELETE = "DELETE FROM " + TABLE_NAME + " WHERE Key = ?";

        private static SQLiteConnection _connection;

        SQLiteDB()
        {
            _connection = new SQLiteConnection(DB_NAME);
            CreateTable();
        }

        private static void CreateTable()
        {
            using (var statement = _connection.Prepare(SQL_CREATE_TABLE))
            {
                statement.Step();
            }
        }

        private static void InsertData()
        {
            using (var statement = _connection.Prepare(SQL_INSERT))
            {
                statement.Bind(1, key);
                statement.Bind(2, value);
                statement.Step();
            }
        }

    }
}
