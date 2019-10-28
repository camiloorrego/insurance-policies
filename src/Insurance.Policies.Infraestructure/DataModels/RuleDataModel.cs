using System;
using System.Collections.Generic;
using System.Text;

namespace Insurance.Policies.Infraestructure.DataModels
{
  public  class RuleDataModel
    {
        public int RuleId { get; set; }

        public int RiskTypeId { get; set; }

        public string Type { get; set; }

        public decimal? Value { get; set; }
    }
}
