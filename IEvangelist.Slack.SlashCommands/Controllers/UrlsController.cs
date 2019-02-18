using System.Net;
using System.Threading.Tasks;
using IEvangelist.Slack.SlashCommands.Interfaces;
using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Mvc;

namespace IEvangelist.Slack.SlashCommands.Controllers
{
    [ApiController, Route("api/urls")]
    public class UrlsController : ControllerBase
    {
        [
            HttpPost("shorten"),
            Consumes("application/x-www-form-urlencoded"),
            Produces("application/json")
        ]
        public async Task<ActionResult> Shorten(
            [FromServices] IUrlService urlService,
            [FromForm] SlackCommandRequest request)
        {
            var encodedUrl = WebUtility.UrlEncode(request.Text);
            var shortUrl = await urlService.ShortenUrlAsync(encodedUrl);

            return new JsonResult(new { text = shortUrl });
        }
    }
}