using ApplicationCore.Entities;

namespace Infrastructure.Options
{
    public class SuperAdminDefaultOptions
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
