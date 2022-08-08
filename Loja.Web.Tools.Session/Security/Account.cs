namespace Loja.Web.Tools.Session.Security
{
    public class Account
    {
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public string? Role { get; set; }
    }
}
