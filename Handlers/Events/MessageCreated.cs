using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Flux.Main.Handlers.Events
{
    public class MessageCreated
    {
        public static async Task Process(DiscordClient cli, MessageCreateEventArgs e)
        {
            Console.WriteLine("Not yet implemented fully");
            Console.WriteLine("Raw Message: " + e.Message);
            Console.WriteLine("Message Author: " + e.Message.Author.Id);
        }
    }
}