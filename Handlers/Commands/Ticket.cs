using System.Threading.Channels;
using CSL.SQL;
using DSharpPlus.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.EventArgs;
using DSharpPlus.Net.Models;
using Flux.SQL.Types;
using Newtonsoft.Json;

namespace Flux.Main.Handlers.Commands
{
    public class Ticket : BaseCommandModule
    {
        [Command("ticket")]
        public async Task TicketHandler(CommandContext ctx)
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

            TicketType ret = new TicketType(ctx.Message.Channel.Id.ToLong(), ctx.Message.Id.ToLong());

            using (SQLDB sql = await SQL.SQLHandler.GetSql())
            {
                await ret.Upsert(sql);
            }
        }
    }
}