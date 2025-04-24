using Microsoft.AspNetCore.Identity;

namespace ASC_Web.Areas.Accounts.Models
{
    public class ServiceEngineerViewModel
    {
        public List<IdentityUser>? ServiceEngineers {  get; set; }
        public ServiceEngineerRegistrationViewModel Registration { get; set; }
    }
}
