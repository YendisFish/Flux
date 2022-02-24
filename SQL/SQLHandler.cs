using CSL.SQL;
using Flux.SQL.Types;

namespace UserGrabberReborn.Server.SQL
{
    public class SQLHandler
    {
        public static async Task<SQLDB> GetSql() => await PostgreSQL.Connect("localhost", "flux", "flux", "FIXME", null, CSL.SslMode.Require);
        public static async Task Init()
        {
            PostgreSQL.TrustAllServerCertificates = true;

            using (SQLDB sql = await GetSql())
            {
                sql.BeginTransaction();

                await ServerEntry.CreateDB(sql);

                sql.CommitTransaction();
            }
        }
    }
}