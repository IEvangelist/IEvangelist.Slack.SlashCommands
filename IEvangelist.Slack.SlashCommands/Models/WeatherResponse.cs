using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace IEvangelist.Slack.SlashCommands.Models
{
    public class WeatherResponse
    {
        readonly Dictionary<string, string> _iconMap = new Dictionary<string, string>
        {
            ["01d"] = ":sun_with_face:",
            ["01n"] = ":full_moon:",
            ["02d"] = ":sun_small_cloud:",
            ["02n"] = ":sun_small_cloud:",
            ["03d"] = ":cloud:",
            ["03n"] = ":cloud:",
            ["04d"] = ":cloud:",
            ["04n"] = ":cloud:",
            ["09d"] = ":rain_cloud:",
            ["09n"] = ":rain_cloud:",
            ["10d"] = ":partly_sunny_rain:",
            ["10n"] = ":partly_sunny_rain:",
            ["11d"] = ":thunder_cloud_and_rain:",
            ["11n"] = ":thunder_cloud_and_rain:",
            ["13d"] = ":snow_cloud:",
            ["13n"] = ":snow_cloud:",
            ["50d"] = ":wind_blowing_face:",
            ["50n"] = ":wind_blowing_face:"
        };

        [JsonProperty("main")]
        public Main Main { get; set; }

        [JsonProperty("weather")]
        public IList<Weather> Weather { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        public override string ToString()
        {
            var temp = Main.Temp;
            var low = Main.TempMin;
            var high = Main.TempMax;

            var (description, icon) = TryGetDetails();

            return $"{icon} It's currently {temp:#}° right now in {Name}, with {description}. Expect a low of {low:#}° and a high of {high:#}°.";
        }

        (string description, string icon) TryGetDetails()
        {
            if (Weather?.Any() ?? false)
            {
                var weather = Weather[0];
                var hasIcon = _iconMap.TryGetValue(weather.Icon, out var icon);
                return (weather.Description, hasIcon ? icon : "");
            }

            return (null, null);
        }
    }

    public struct Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }
    }
}