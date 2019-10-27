using System;

namespace Insurance.Policies.Domain.Entities
{
    public class ClientPolicy
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int PoliceId { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PolicyType { get; set; }
        public string RiskType { get; set; }
        public double Coverage { get; set; }
        public double Cost { get; set; }
    }
}

