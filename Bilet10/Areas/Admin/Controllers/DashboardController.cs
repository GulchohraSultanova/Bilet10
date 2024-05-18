using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bilet10.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    [Authorize(Roles = "SuperAdmin")]

    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
