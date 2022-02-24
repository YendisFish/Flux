using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CSL.SQL;

namespace Flux.SQL.Types
{
    public record TicketType(long Id, long MessageId) : IDBSet
    {
        #region Static Functions
        public static Task<int> CreateDB(SQLDB sql) => sql.ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS \"TicketType\" (" +
            "\"Id\" BIGINT NOT NULL, " +
            "\"MessageId\" BIGINT NOT NULL, " +
            "PRIMARY KEY(\"Id\")" +
            ");");
        public static IEnumerable<TicketType> GetRecords(IDataReader dr)
        {
            while(dr.Read())
            {
                long Id =  (long)dr[0];
                long MessageId =  (long)dr[1];
                yield return new TicketType(Id, MessageId);
            }
            yield break;
        }
        #region Select
        public static async Task<AutoClosingEnumerable<TicketType>> Select(SQLDB sql)
        {
            AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"TicketType\";");
            return new AutoClosingEnumerable<TicketType>(GetRecords(dr),dr);
        }
        public static async Task<AutoClosingEnumerable<TicketType>> Select(SQLDB sql, string query, params object[] parameters)
        {
            AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"TicketType\" WHERE " + query + " ;", parameters);
            return new AutoClosingEnumerable<TicketType>(GetRecords(dr),dr);
        }
        public static async Task<TicketType?> SelectBy_Id(SQLDB sql, long Id)
        {
            using(AutoClosingDataReader dr = await sql.ExecuteReader("SELECT * FROM \"TicketType\" WHERE \"Id\" = @0;", Id))
            {
                return GetRecords(dr).FirstOrDefault();
            }
        }
        #endregion
        #region Delete
        public static Task<int> DeleteBy_Id(SQLDB sql, long Id) => sql.ExecuteNonQuery("DELETE FROM \"TicketType\" WHERE \"Id\" = @0;", Id);
        #endregion
        #region Table Management
        public static Task Truncate(SQLDB sql, bool cascade = false) => sql.ExecuteNonQuery($"TRUNCATE \"TicketType\"{(cascade?" CASCADE":"")};");
        public static Task Drop(SQLDB sql, bool cascade = false) => sql.ExecuteNonQuery($"DROP TABLE IF EXISTS \"TicketType\"{(cascade?" CASCADE":"")};");
        #endregion
        #endregion
        #region Instance Functions
        public Task<int> Insert(SQLDB sql) =>
            sql.ExecuteNonQuery("INSERT INTO \"TicketType\" (\"Id\", \"MessageId\") " +
            "VALUES(@0, @1);", ToArray());
        public Task<int> Update(SQLDB sql) =>
            sql.ExecuteNonQuery("UPDATE \"TicketType\" " +
            "SET \"MessageId\" = @1 " +
            "WHERE \"Id\" = @0;", ToArray());
        public Task<int> Upsert(SQLDB sql) =>
            sql.ExecuteNonQuery("INSERT INTO \"TicketType\" (\"Id\", \"MessageId\") " +
            "VALUES(@0, @1) " +
            "ON CONFLICT (\"Id\") DO UPDATE " +
            "SET \"MessageId\" = @1;", ToArray());
        public object?[] ToArray()
        {
            long _Id = Id;
            long _MessageId = MessageId;
            return new object?[] { _Id, _MessageId };
        }
        #endregion
    }
}