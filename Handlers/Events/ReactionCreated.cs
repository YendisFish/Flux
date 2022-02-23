using DSharpPlus;
using DSharpPlus.EventArgs;
using Flux.Main.Types;
using Newtonsoft.Json;

namespace Flux.Main.Handlers.Events
{
    public class ReactionCreated
    {
        public static async Task Process(DiscordClient cli, MessageReactionAddEventArgs e)
        {
            List<TicketType>? tickets = JsonConvert.DeserializeObject<List<TicketType>>(File.ReadAllText("./opentickets.json"));
            List<TicketType> n = new();
            foreach (TicketType ticket in tickets ?? new List<TicketType>())
            {
                if (ticket.Channel.Id == e.Message.Channel.Id)
                {
                    if (e.Message.Id == ticket.Message.Id)
                    {
                        Console.WriteLine("Skipping current");
                    }
                }
                else
                {
                    n.Add(ticket);
                }
            }

            await File.WriteAllTextAsync("./opentickets.json", JsonConvert.SerializeObject(n));
        }
    }
}