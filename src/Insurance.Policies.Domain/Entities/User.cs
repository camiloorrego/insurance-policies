using Insurance.Policies.Utilities;
using System;
using System.Collections.Generic;

namespace Insurance.Policies.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CreateToken()
        {
            Dictionary<string, object> points = new Dictionary<string, object>
            {
                { "username", Username },
                { "expired",  DateTime.Now.AddMinutes(30).ToString("dd/MM/yyyy HH:mm")}
            };

            //var  a = DateTime.Parse(points["Expired"].ToString());

            var json = Serializer.Serialize(points);
            return Decoder.Encode(json);
        }


    }
}
