using Newtonsoft.Json;

namespace Insurance.Policies.Dto
{
    public class ErrorResponseDto
    {
        [JsonProperty("error_code")]
        public string Code { get; set; }
        [JsonProperty("error_message")]
        public string Message { get; set; }
    }
}
