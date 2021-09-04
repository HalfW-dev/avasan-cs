using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace avasan_cs.Modules
{
    public class osu : ModuleBase
    {
        [Command("osu")]
        public async Task Osu(string User, string Mode = "std") //string User, int Number
        {
            dynamic source = JObject.Parse(File.ReadAllText(@"C:\Users\Tung Hoang\source\repos\avasan-cs\Services\Secret.json"));
            string api = source.api_key;

            string displayMode = "";

            switch (Mode)
            {
                case "std":
                    Mode = "0";
                    displayMode = "Standard";
                    break;
                case "taiko":
                    Mode = "1";
                    displayMode = "Taiko";
                    break;
                case "ctb":
                    Mode = "2";
                    displayMode = "CTB";
                    break;
                case "mania":
                    Mode = "3";
                    displayMode = "Mania";
                    break;
                default:
                    Mode = "0";
                    displayMode = "Standard";
                    break;
            }

            var client = new HttpClient();//I know this ain't the best way, I will learn factory after I get this running
            string url = $"https://osu.ppy.sh/api/get_user?&k={api}&u={User}&m={Mode}"; 

            var result = await client.GetStringAsync(url);
            dynamic arr = JsonConvert.DeserializeObject(result);

            string url_ava = $"http://s.ppy.sh/a/{arr[0].user_id}";
            var acc = Decimal.Round(Convert.ToDecimal(arr[0].accuracy), 2);
            var flag_code = arr[0].country.ToString().ToLower();

            var builder = new EmbedBuilder()
                .WithThumbnailUrl(url_ava)
                .WithTitle($":flag_{flag_code}: osu! {displayMode} Profile for {arr[0].username.ToString()}")
                .WithDescription($"Rank: #{arr[0].pp_rank} ({arr[0].country}#{arr[0].pp_country_rank})" + "\n" + $"PP: {arr[0].pp_raw}" + "\n" + $"Acc: {acc}%")
                .WithFooter($"Joined at {arr[0].join_date} (Time in UTC)");

            var embed = builder.Build();
            await Context.Channel.SendMessageAsync(null, false, embed);

        }

    }
        
    
}
