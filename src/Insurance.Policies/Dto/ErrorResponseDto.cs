using Newtonsoft.Json;
using System;

namespace Insurance.Policies.Dto
{
    [Serializable]
    public class ErrorResponseDto
    {
        [JsonProperty("error_code")]
        public string Code { get; set; }
        [JsonProperty("error_message")]
        public string Message { get; set; }
    }
}
