using Newtonsoft.Json;
using System;

namespace EmailService.WebHook.Models
{
    public class WebHookRequest
    {
        [JsonProperty("message-id")]
        public string MessageId { get; set; } = null!;

        [JsonProperty("event")]
        public string Event { get; set; } = null!;

        [JsonProperty("email")]
        public string Email { get; set; } = null!;

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
    }
}
