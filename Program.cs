using DSharpPlus.Entities;
using Flux.Main.Handlers;

namespace Flux.Main
{
    class EntryPoint
    {
        public static async Task Main()
        {
            Console.WriteLine("Starting Flux...");
            await DCHandler.RunAsync();
        }
    }
}