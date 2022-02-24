using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace Flux.Main.Handlers.Events
{
    public class MessageCreated
    {
        public static async Task Process(DiscordClient cli, MessageCreateEventArgs e)
        {
            Console.WriteLine("Not yet implemented fully");
            Console.WriteLine("Raw Message: " + e.Message);
            Console.WriteLine("Message Author: " + e.Message.Author.Id);

            if (File.Exists("./messages.json"))
            {
                List<DiscordMessage> messages = JsonConvert.DeserializeObject<List<DiscordMessage>>(File.ReadAllText("./messages.json"));
                messages?.Add(e.Message);
                await File.WriteAllTextAsync("./messages.json", JsonConvert.SerializeObject(messages));
            }
            else
            {
                List<DiscordMessage> messages = new() { e.Message };
                await File.WriteAllTextAsync("./messages.json", JsonConvert.SerializeObject(messages));
            }
        }
    }
}