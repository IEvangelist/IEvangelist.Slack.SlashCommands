using IEvangelist.Slack.SlashCommands.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IEvangelist.Slack.SlashCommands.Extensions
{
    static class SlackCommandRequestExtensions
    {
        internal static async Task<bool> IsValidAsync(
            this SlackCommandRequest request,
            HttpRequest httpRequest,
            string signingSecret)
        {
            if (request is null ||
                httpRequest is null ||
                signingSecret is null)
            {
                return await Task.FromResult(false);
            }

            // https://api.slack.com/docs/verifying-requests-from-slack
            var timestamp = httpRequest.Headers["X-Slack-Request-Timestamp"];
            var signature = httpRequest.Headers["X-Slack-Signature"].ToString();

            var body = await GetRequestBody(httpRequest);
            var payload = Encoding.UTF8.GetBytes($"v0:{timestamp}:{body}");

            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(signingSecret)))
            {
                var hash = hmac.ComputeHash(payload);
                var computedSignature = $"v0={Encoding.UTF8.GetString(hash)}";

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
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                {
                    _ = ex.Message;
                    Debugger.Break();
                }

                throw;
            }
        }
    }
}