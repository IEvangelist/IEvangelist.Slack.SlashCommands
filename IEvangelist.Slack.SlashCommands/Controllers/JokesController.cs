using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Extensions;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IEvangelist.Slack.SlashCommands.Controllers
{
    [ApiController, Route("api/jokes")]
    public class JokesController : ControllerBase
    {
        readonly List<string> _positiveEmoji = new List<string>
        {
            ":smile:",
            ":smirk:",
            ":clap:",
            ":joy:",
            ":grin:",
            ":yum:",
            ":sweat_smile:",
            ":laughing:",
            ":smiley:",
            ":rolling_on_the_floor_laughing:"
        };

        readonly IJokeService _jokeService;
        readonly SlackOptions _slackOptions;

        public JokesController(IJokeService jokeService, IOptions<SlackOptions> slackOptions)
        {
            _jokeService = jokeService;
            _slackOptions = slackOptions?.Value ?? throw new ArgumentNullException(nameof(slackOptions));
        }

        [
            HttpPost("random"),
            Consumes("application/x-www-form-urlencoded"),
            Produces("application/json")
        ]
        public async Task<ActionResult> Random([FromForm] SlackCommandRequest request)
        {
            var response = await _jokeService.GetJokeAsync();
            return new JsonResult(new
            {
                response_type = request.Text == "share" ? "in_channel" : "ephemeral",
                text = response.Value.Joke,
                attachments = new[]
                {
                    new { text = _positiveEmoji.RandomElement() }
                }
            });
        }
    }
}