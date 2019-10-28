using System;

namespace Insurance.Policies.Infraestructure.DataModels
{
    public class ClientPolicyDataModel
    {
        public int Id { get; set; }

        public string Client { get; set; }
        public int ClientId { get; set; }
        public string PoliceName { get; set; }
        public string PoliceId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

    }
}
