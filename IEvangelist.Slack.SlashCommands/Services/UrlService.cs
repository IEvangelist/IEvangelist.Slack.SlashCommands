using System;
using System.Net.Http;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Interfaces;
using Microsoft.Extensions.Options;

namespace IEvangelist.Slack.SlashCommands.Services
{
    public class UrlService : IUrlService
    {
        readonly HttpClient _client;
        readonly BitlyOptions _options;

        public UrlService(HttpClient client, IOptions<BitlyOptions> options)
        {
            _client = client;
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> ShortenUrlAsync(string longUrl)
        {
            var shortUrl = 
                await _client.GetStringAsync(
                    $"shorten?login={_options.Login}&apiKey={_options.ApiKey}&longUrl={longUrl}&format=txt");

            return shortUrl?.TrimEnd('\r', '\n', '\\', '/');
        }
    }
}