using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Mvc;

namespace IEvangelist.Slack.SlashCommands.Controllers
{
    [ApiController, Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService) => _weatherService = weatherService;

        [
            HttpPost,
            Consumes("application/x-www-form-urlencoded"),
            Produces("application/json")
        ]
        public async Task<ActionResult> Random([FromForm] SlackCommandRequest request)
        {
            var response = await _weatherService.GetWeatherAsync(request.Text);
            return new JsonResult(new
            {
                text = response.ToString()
            });
        }
    }
}