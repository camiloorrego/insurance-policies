namespace Insurance.Policies.Domain.Entities
{
    public class Rule
    {
        public int RuleId { get; set; }

        public int RiskTypeId { get; set; }

        public string Type { get; set; }

        public decimal Value { get; set; }

    }
}
