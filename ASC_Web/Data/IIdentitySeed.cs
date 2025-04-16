using ASC_Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
namespace ASC_Web.Data
{
    public interface IIdentitySeed
    {
        Task Seed(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<ApplicationSetting> settings);
    }
}
