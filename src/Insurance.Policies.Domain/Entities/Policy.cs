using System;

namespace Insurance.Policies.Domain.Entities
{
    public class Policy
    {
        public int PoliceId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PolicyTypeId { get; set; }
        public string PolicyType { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int Terms { get; set; }

        public decimal Cost { get; set; }
        public decimal Coverage { get; set; }

        public int RiskTypeId { get; set; }
        public string RiskType { get; set; }

        public int UserId { get; set; }

    }
}
