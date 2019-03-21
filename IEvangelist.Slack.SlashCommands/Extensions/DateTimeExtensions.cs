using System;

namespace IEvangelist.Slack.SlashCommands.Extensions
{
    public static class DateTimeExtensions
    {
        static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTime(this double unixTimeStamp) 
            => _epoch.AddSeconds(unixTimeStamp).ToLocalTime();
    }
}