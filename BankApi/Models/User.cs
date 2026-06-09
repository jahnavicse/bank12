using Microsoft.AspNetCore.Identity;

namespace BankApi.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}