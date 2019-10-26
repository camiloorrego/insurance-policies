using Newtonsoft.Json;
using System;

namespace Insurance.Policies.Utilities
{
    public static class Serializer
    {
        public static string Serialize<T>(T o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
