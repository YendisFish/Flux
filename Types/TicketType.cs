using DSharpPlus.Entities;

namespace Flux.Main.Types
{
    public class TicketType
    {
        public DiscordChannel Channel { get; set; }
        public DiscordMessage Message { get; set; }

        public TicketType(DiscordChannel channel, DiscordMessage message)
        {
            this.Channel = channel;
            this.Message = message;
        }
    }
}