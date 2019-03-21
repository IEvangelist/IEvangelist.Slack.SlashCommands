using System;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Extensions;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IEvangelist.Slack.SlashCommands.Controllers
{
    [ApiController, Route("api/weather")]
    public class WeatherController : ControllerBase
    {
        readonly IWeatherService _weatherService;
        readonly SlackOptions _slackOptions;

        public WeatherController(IWeatherService weatherService, IOptions<SlackOptions> slackOptions)
        {
            _weatherService = weatherService;
            _slackOptions = slackOptions?.Value ?? throw new ArgumentNullException(nameof(slackOptions));
        }

        [
            HttpPost,
            Consumes("application/x-www-form-urlencoded"),
            Produces("application/json")
        ]
        public async Task<ActionResult> Weather([FromForm] SlackCommandRequest request)
        {
            //if (await request.IsValidAsync(Request, _slackOptions.SigningSecret))
            //{
            //    return new UnauthorizedResult();
            //}

            var response = await _weatherService.GetWeatherAsync(request.Text);
            return new JsonResult(new
            {
                text = response.ToString()
            });
        }
    }
}