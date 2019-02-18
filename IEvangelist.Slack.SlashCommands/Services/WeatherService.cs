using System;
using System.Net.Http;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace IEvangelist.Slack.SlashCommands.Services
{
    public class WeatherService : IWeatherService
    {
        readonly HttpClient _client;
        readonly OpenWeatherMapOptions _openWeatherMapOptions;

        public WeatherService(HttpClient client, IOptions<OpenWeatherMapOptions> options)
        {
            _client = client;
            _openWeatherMapOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<WeatherResponse> GetWeatherAsync(string zipCode)
        {
            var url = $"?zip={zipCode},us&appid={_openWeatherMapOptions.Key}&units=imperial";
            var json = await _client.GetStringAsync(url);

            return JsonConvert.DeserializeObject<WeatherResponse>(json);
        }
    }
}