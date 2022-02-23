using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;

namespace Flux.Main.Handlers.Commands
{
    public class Ticket
    {
        [Command("ticket")]
        public static async Task TicketHandler(CommandContext ctx)
        {
            DiscordChannel ticket = await ctx.Guild.CreateChannelAsync("ticket-" + ctx.Message.Author.Username, ChannelType.Text);
        }
    }
}