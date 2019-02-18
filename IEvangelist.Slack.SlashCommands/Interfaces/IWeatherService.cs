using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Models;

namespace IEvangelist.Slack.SlashCommands.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string zipCode);
    }
}