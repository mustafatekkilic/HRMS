using Microsoft.AspNetCore.Identity;

namespace HRMS.Entities
{
    public class Users : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
