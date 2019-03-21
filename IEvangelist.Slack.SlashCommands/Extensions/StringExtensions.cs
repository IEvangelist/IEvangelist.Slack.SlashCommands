using Newtonsoft.Json;

namespace IEvangelist.Slack.SlashCommands.Extensions
{
    public static class StringExtensions
    {
        public static T To<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
    }
}