namespace Insurance.Policies.Domain.Entities
{
    public class ClientPolicy
    {
        public int Id { get; set; }

        public string Client { get; set; }
        public int ClientId { get; set; }
        public string PolicyName { get; set; }
        public int PoliceId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

    }
}
