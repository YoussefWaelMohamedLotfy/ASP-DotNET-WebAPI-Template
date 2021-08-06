using Microsoft.AspNetCore.Identity;

namespace ASP_DotNET_WebAPI_Template.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}