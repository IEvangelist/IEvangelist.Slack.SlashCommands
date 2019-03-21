using System;
using System.Net;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Configuration;
using IEvangelist.Slack.SlashCommands.Extensions;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IEvangelist.Slack.SlashCommands.Controllers
{
    [ApiController, Route("api/urls")]
    public class UrlsController : ControllerBase
    {
        readonly SlackOptions _slackOptions;

        public UrlsController(IOptions<SlackOptions> slackOptions)
            => _slackOptions = slackOptions?.Value ?? throw new ArgumentNullException(nameof(slackOptions));

        [
            HttpPost("shorten"),
            Consumes("application/x-www-form-urlencoded"),
            Produces("application/json")
        ]
        public async Task<ActionResult> Shorten(
            [FromServices] IUrlService urlService,
            [FromForm] SlackCommandRequest request,
            [FromServices] ILogger<UrlsController> logger)
        {
            if (await request.IsValidAsync(Request, _slackOptions.SigningSecret, logger))
            {
                return new UnauthorizedResult();
            }

            var encodedUrl = WebUtility.UrlEncode(request.Text);
            var shortUrl = await urlService.ShortenUrlAsync(encodedUrl);

            return new JsonResult(new { text = shortUrl });
        }
    }
}