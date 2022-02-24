using System.Threading.Channels;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.EventArgs;
using Flux.Main.Types;
using Newtonsoft.Json;

namespace Flux.Main.Handlers.Commands
{
    public class Ticket : BaseCommandModule
    {
        [Command("ticket")]
        public static async Task<TicketType> TicketHandler(CommandContext ctx)
        {
            DiscordMember member = await ctx.Guild.GetMemberAsync(ctx.Message.Author.Id);
            DiscordChannel ticket = await ctx.Guild.CreateChannelAsync("ticket-" + ctx.Message.Author.Username, ChannelType.Text);
            
            await ticket.AddOverwriteAsync(member, Permissions.SendMessages);
            await ticket.AddOverwriteAsync(member, Permissions.ReadMessageHistory);
            await ticket.AddOverwriteAsync(member, Permissions.AttachFiles);
            await ticket.AddOverwriteAsync(member, Permissions.AddReactions);
            await ticket.AddOverwriteAsync(member, Permissions.UseExternalEmojis);
            await ticket.AddOverwriteAsync(member, Permissions.UseExternalStickers);

            DiscordMessage msg = await ticket.SendMessageAsync($"<@!{member.Id.ToString()}> <@!{null}>");

            TicketType ret = new TicketType(ticket, msg);

            if (File.Exists("./opentickets.json"))
            {
                List<TicketType>? tickets = JsonConvert.DeserializeObject<List<TicketType>>(File.ReadAllText("./opentickets.json")) ?? new List<TicketType>() { ret };
                tickets?.Add(ret);
                await File.WriteAllTextAsync("./opentickets.json", JsonConvert.SerializeObject(tickets));
            }
            else
            {
                List<TicketType> tickets = new();
                tickets?.Add(ret);
                await File.WriteAllTextAsync("./opentickets.json", JsonConvert.SerializeObject(tickets));
            }

            return ret;
        }
    }
}