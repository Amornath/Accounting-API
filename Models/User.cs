using Microsoft.AspNetCore.Identity;

namespace ScopoERP.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        //public byte[] UserImage { get; set; }
    }
}
