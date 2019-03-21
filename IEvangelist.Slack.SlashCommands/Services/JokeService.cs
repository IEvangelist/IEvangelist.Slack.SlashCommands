using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Extensions;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;

namespace IEvangelist.Slack.SlashCommands.Services
{
    public class JokeService : IJokeService
    {
        readonly IList<string> _positiveEmoji = new List<string>
        {
            ":smile:",
            ":smirk:",
            ":clap:",
            ":joy:",
            ":grin:",
            ":yum:",
            ":sweat_smile:",
            ":laughing:",
            ":smiley:",
            ":rolling_on_the_floor_laughing:"
        };

        readonly IHttpClientFactory _httpClientFactory;

        public JokeService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        public async Task<JokeResponse> GetJokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync("https://api.icndb.com/jokes/random?limitTo=[nerdy]");

            return json.To<JokeResponse>();
        }

        public string GetRandomJokeEmoji()
            => _positiveEmoji.RandomElement();
    }
}