namespace Insurance.Policies.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string CreateToken()
        {
            return "";
        }
    }
}
