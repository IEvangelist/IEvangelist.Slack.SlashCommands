using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Models;

namespace IEvangelist.Slack.SlashCommands.Interfaces
{
    public interface IJokeService
    {
        Task<JokeResponse> GetJokeAsync();

        string GetRandomJokeEmoji();
    }
}