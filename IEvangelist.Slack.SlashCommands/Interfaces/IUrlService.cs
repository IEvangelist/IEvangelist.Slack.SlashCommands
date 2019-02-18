using System.Threading.Tasks;

namespace IEvangelist.Slack.SlashCommands.Interfaces
{
    public interface IUrlService
    {
        Task<string> ShortenUrlAsync(string longUrl);
    }
}