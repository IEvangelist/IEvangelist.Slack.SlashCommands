using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace IEvangelist.Slack.SlashCommands.Extensions
{
    static class SlackCommandRequestExtensions
    {
        internal static async Task<bool> IsValidAsync(
            this SlackCommandRequest request,
            HttpRequest httpRequest,
            string signingSecret,
            ILogger logger)
        {
            if (request is null ||
                httpRequest is null ||
                signingSecret is null)
            {
                logger.LogInformation("Early exit.");
                return false;
            }

            // https://api.slack.com/docs/verifying-requests-from-slack
            var timestamp = httpRequest.Headers["X-Slack-Request-Timestamp"];
            if (double.TryParse(timestamp.ToString(), out var unix) &&
                DateTime.Now.Subtract(unix.FromUnixTime()).TotalMinutes > 5)
            {
                logger.LogInformation($"Date {unix.FromUnixTime()}");
                return false;
            }

            var signature = httpRequest.Headers["X-Slack-Signature"].ToString();

            var body = await GetRequestBody(httpRequest);
            var payload = Encoding.UTF8.GetBytes($"v0:{timestamp}:{body}");

            logger.LogInformation($"Payload {payload}");

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(signingSecret)))
            {
                var hash = hmac.ComputeHash(payload);
                var computedSignature = $"v0={Encoding.UTF8.GetString(hash)}";

                logger.LogInformation($"cs:{computedSignature} vs s:{signature}");

                return string.Equals(signature, computedSignature);
            }
        }

        static async Task<string> GetRequestBody(HttpRequest httpRequest)
        {
            try
            {
                httpRequest.EnableRewind();
                using (var reader = new StreamReader(httpRequest.Body))
                {
                    return await reader.ReadToEndAsync();
                }
            }
            catch (Exception ex) when (Debugger.IsAttached)
            {
                _ = ex;
                Debugger.Break();

                throw;
            }
        }
    }
}