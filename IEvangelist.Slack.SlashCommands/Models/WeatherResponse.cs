using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace IEvangelist.Slack.SlashCommands.Models
{
    public class WeatherResponse
    {
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

            var description =
                Weather?.Any() ?? false
                    ? Weather[0].Description
                    : "";

            return $"It's currently {temp:#}° right now in {Name}, with {description}. Expect a low of {low:#}° and a high of {high:#}°.";
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