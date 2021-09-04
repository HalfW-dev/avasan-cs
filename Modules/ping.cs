using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avasan_cs.Modules
{
    public class ping : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PingAsync()
        {
            await ReplyAsync("Pong!");
            await Context.Channel.SendMessageAsync("Pong(1)!");
            await Context.Channel.SendFileAsync("");
        }

        [Command("info")]
        public async Task InfoAsync(SocketGuildUser socketGuildUser = null)
        {
            if (socketGuildUser == null)
            {
                socketGuildUser = Context.User as SocketGuildUser;
            }

            //    await ReplyAsync($"ID: {socketGuildUser.id}\n + " +
            //        $"Name: {socketGuildUser.Username}#{socketGuildUser.Discriminator}\n" +
            //        $"Created at : {socketGuildUser.CreatedAt}");

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(socketGuildUser.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl())
                .WithDescription($"Info of {socketGuildUser.Username}#{socketGuildUser.Discriminator}")
                .WithColor(new Color(0, 221, 192))
                .AddField("User ID", socketGuildUser.Id, true)
                .AddField("Discriminator", socketGuildUser.Discriminator, true)
                .AddField("Created at", socketGuildUser.CreatedAt.ToString("dd/MM/yyyy"))
                .AddField("Joined at", socketGuildUser.JoinedAt.Value.ToString("dd/MM/yyyy"))
                .WithCurrentTimestamp();
            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);
        }
    }
}
