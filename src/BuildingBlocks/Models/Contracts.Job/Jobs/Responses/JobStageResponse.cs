using Domain.Seedwork.Enums;
using System.Text.Json.Serialization;

namespace Contracts.Job.Jobs.Responses
{
    public class JobStageResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public JobStage JobStage { get; set; }
    }
}
