using System.Runtime.InteropServices.ComTypes;
using CSL.SQL;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Flux.Main.Handlers.Commands;
using Flux.SQL.Types;

namespace Flux.Main.Handlers.Events
{
    public class ReactionCreated
    {
        public static async Task Process(DiscordClient cli, MessageReactionAddEventArgs e)
        {
            using (SQLDB sql = await SQL.SQLHandler.GetSql())
            {
                AutoClosingEnumerable<TicketType> ticketsraw = await TicketType.Select(sql);
                List<TicketType> tickets = new();
                tickets.AddRange(ticketsraw);

                foreach (TicketType ticket in tickets)
                {
                    try
                    {
                        if (ticket.Id == e.Message.Id.ToLong())
                        {
                            await e.Channel.DeleteAsync();
                            //Maybe truncate table?
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        Console.WriteLine("Failed on reaction event handler");
                    }
                }
            }
        }
    }
}