using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Policies.Domain.Settings
{
   public class AppSettings
    {
        public DataBaseSettings DataBaseSettings { get; set; }
        public AuthSettings AuthSettings { get; set; }
        public int UserId { get; set; }
    }
}
