using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Insurance.Policies.Dto
{
    public class CredentialsResponseDto
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}
