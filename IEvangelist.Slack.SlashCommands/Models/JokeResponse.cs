namespace IEvangelist.Slack.SlashCommands.Models
{
    public class JokeResponse
    {
        public string Type { get; set; }

        public JokeModel Value { get; set; }
    }
}