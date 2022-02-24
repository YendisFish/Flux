using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSL.SQL;

namespace Flux.SQL.Types
{
    public record ServerEntry(long Id, string Type, string Author, string Content, DateTime Date) : IDBSet
    {
        #region Static Functions
        public static Task<int> CreateDB(SQLDB sql) => sql.ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS \"ServerEntry\" (" +
            "\"Id\" BIGINT NOT NULL, " +
            "\"Type\" TEXT NOT NULL, " +
            "\"Author\" TEXT NOT NULL, " +
            "\"Content\" TEXT NOT NULL, " +
            "\"Date\" TIMESTAMP NOT NULL, " +
            "PRIMARY KEY(\"Id\")" +
            ");");
        public static IEnumerable<ServerEntry> GetRecords(IDataReader dr)
        {
            while(dr.Read())
            {
                long Id =  (long)dr[0];
                string Type =  (string)dr[1];
                string Author =  (string)dr[2];
                string Content =  (string)dr[3];
                DateTime Date =  (DateTime)dr[4];
                yield return new ServerEntry(Id, Type, Author, Content, Date);
            }
            yield break;
        }
        #region Select
        public static async Task<AutoClosingEnumerable<ServerEntry>> Select(SQLDB sql)
        {
            AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"ServerEntry\";");
            return new AutoClosingEnumerable<ServerEntry>(GetRecords(dr),dr);
        }
        public static async Task<AutoClosingEnumerable<ServerEntry>> Select(SQLDB sql, string query, params object[] parameters)
        {
            AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"ServerEntry\" WHERE " + query + " ;", parameters);
            return new AutoClosingEnumerable<ServerEntry>(GetRecords(dr),dr);
        }
        public static async Task<ServerEntry?> SelectBy_Id(SQLDB sql, long Id)
        {
            using(AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"ServerEntry\" WHERE \"Id\" = @0;", Id))
            {
                return GetRecords(dr).FirstOrDefault();
            }
        }
        #endregion
        #region Delete
        public static Task<int> DeleteBy_Id(SQLDB sql, long Id) => sql.ExecuteNonQuery("DELETE FROM \"ServerEntry\" WHERE \"Id\" = @0;", Id);
        #endregion
        #region Table Management
        public static Task Truncate(SQLDB sql, bool cascade = false) => sql.ExecuteNonQuery($"TRUNCATE \"ServerEntry\"{(cascade?" CASCADE":"")};");
        public static Task Drop(SQLDB sql, bool cascade = false) => sql.ExecuteNonQuery($"DROP TABLE IF EXISTS \"ServerEntry\"{(cascade?" CASCADE":"")};");
        #endregion
        #endregion
        #region Instance Functions
        public Task<int> Insert(SQLDB sql) =>
            sql.ExecuteNonQuery("INSERT INTO \"ServerEntry\" (\"Id\", \"Type\", \"Author\", \"Content\", \"Date\") " +
            "VALUES(@0, @1, @2, @3, @4);", ToArray());
        public Task<int> Update(SQLDB sql) =>
            sql.ExecuteNonQuery("UPDATE \"ServerEntry\" " +
            "SET \"Type\" = @1, \"Author\" = @2, \"Content\" = @3, \"Date\" = @4 " +
            "WHERE \"Id\" = @0;", ToArray());
        public Task<int> Upsert(SQLDB sql) =>
            sql.ExecuteNonQuery("INSERT INTO \"ServerEntry\" (\"Id\", \"Type\", \"Author\", \"Content\", \"Date\") " +
            "VALUES(@0, @1, @2, @3, @4) " +
            "ON CONFLICT (\"Id\") DO UPDATE " +
            "SET \"Type\" = @1, \"Author\" = @2, \"Content\" = @3, \"Date\" = @4;", ToArray());
        public object?[] ToArray()
        {
            long _Id = Id;
            string _Type = Type;
            string _Author = Author;
            string _Content = Content;
            DateTime _Date = Date;
            return new object?[] { _Id, _Type, _Author, _Content, _Date };
        }
        #endregion
    }
}