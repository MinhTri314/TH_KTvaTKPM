using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASC_Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}