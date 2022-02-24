using System.Threading.Channels;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using Flux.Main.Handlers.Commands;
using Flux.Main.Handlers.Events;

namespace Flux.Main.Handlers
{
    public class DCHandler
    {
        public static async Task RunAsync()
        {
            DiscordClient client = new DiscordClient(new DiscordConfiguration()
            {
                Token = "OTQ2MjE5OTc0ODE5NzM3NjMw.YhbiBQ.j0PgHbSFItLnX4er66E0MYQeGOw",
                TokenType = TokenType.Bot,
            });

            CommandsNextConfiguration cconfig = new CommandsNextConfiguration()
            {
                EnableDms = false,
                StringPrefixes = new string[] { "fx!" },
                DmHelp = false
            };

            CommandsNextExtension cex = client.UseCommandsNext(cconfig);
            
            //cex.RegisterCommands<Basic>();
            cex.RegisterCommands<Ticket>(); 
            
            client.MessageCreated += Events.MessageCreated.Process;
            client.MessageReactionAdded += ReactionCreated.Process;

            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}