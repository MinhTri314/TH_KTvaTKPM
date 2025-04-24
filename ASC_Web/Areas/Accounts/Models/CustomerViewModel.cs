using Microsoft.AspNetCore.Identity;

namespace ASC_Web.Areas.Accounts.Models
{
    public class CustomerViewModel
    {
        public List<IdentityUser> Customers { get; set; }
        public CustomerRegistrationViewModel Registration { get; set; }
    }
}
