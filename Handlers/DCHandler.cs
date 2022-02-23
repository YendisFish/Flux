using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using Flux.Main.Handlers.Commands;
using Flux.Main.Types;

namespace Flux.Main.Handlers
{
    public class DCHandler
    {
        public static async Task RunAsync()
        {
            DiscordClient client = new DiscordClient(new DiscordConfiguration()
            {
                Token = "",
                TokenType = TokenType.Bot,
            });

            CommandsNextConfiguration cconfig = new CommandsNextConfiguration()
            {
                EnableDms = false,
                StringPrefixes = new string[] { "fx!" },
                DmHelp = false
            };

            CommandsNextExtension cex = client.UseCommandsNext(cconfig);
            
            cex.RegisterCommands<Basic>();
            cex.RegisterCommands<Ticket>();

            client.MessageCreated += Events.MessageCreated.Process;
        }
    }
}