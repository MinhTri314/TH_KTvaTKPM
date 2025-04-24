using ASC_Web.Configuration;
using ASC_Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASC_Web.Areas.ServiceRequests.Controllers
{
    [Area("ServiceRequests")]
    public class DashboardController : BaseController
    {
        private IOptions<ApplicationSetting> _settings;

        public DashboardController(IOptions<ApplicationSetting> settings)
        {
            _settings = settings;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
    }
}