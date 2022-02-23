using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;

namespace Flux.Main.Handlers.Commands
{
    public class Basic : BaseCommandModule
    {
        [Command("help")]
        public static async Task Help(CommandContext ctx)
        {
            await ctx.RespondAsync("Help message not yet implemented!");
        }
    }
}