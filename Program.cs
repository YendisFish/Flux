using DSharpPlus.Entities;
using Flux.Main.Handlers;

namespace Flux.Main
{
    class EntryPoint
    {
        public static async Task Main()
        {
            //CSL dependency injection
            CSL.DependencyInjection.NpgsqlConnectionConstructor = (x) => new Npgsql.NpgsqlConnection(x);
            CSL.DependencyInjection.NpgsqlConnectionStringConstructor = () => new Npgsql.NpgsqlConnectionStringBuilder();
            CSL.DependencyInjection.SslModeConverter = (x) => (Npgsql.SslMode)x;

            //Application startup
            Console.WriteLine("Starting Flux...");
            await DCHandler.RunAsync();
            Console.WriteLine("Stopping Flux...");
        }
    }
}