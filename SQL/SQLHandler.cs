using CSL.SQL;
using Flux.SQL.Types;

namespace Flux.SQL
{
    public class SQLHandler
    {
        public static async Task<SQLDB> GetSql() => await PostgreSQL.Connect("localhost", "flux", "flux", "password", null, CSL.SslMode.Require);
        public static async Task Init()
        {
            PostgreSQL.TrustAllServerCertificates = true;

            using (SQLDB sql = await GetSql())
            {
                sql.BeginTransaction();

                await ServerEntry.CreateDB(sql);
                await TicketType.CreateDB(sql);

                sql.CommitTransaction();
            }
        }
    }
}