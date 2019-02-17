using System.Net.Http;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Newtonsoft.Json;

namespace IEvangelist.Slack.SlashCommands.Services
{
    public class JokeService : IJokeService
    {
        readonly IHttpClientFactory _httpClientFactory;

        public JokeService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        public async Task<JokeResponse> GetJokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var json = await client.GetStringAsync("https://api.icndb.com/jokes/random?limitTo=[nerdy]");

            return JsonConvert.DeserializeObject<JokeResponse>(json);
        }
    }
}