
using Bussiness.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bilet10.Controllers
{
	public class HomeController : Controller
	{
		IOurTeamService _ourTeam;

        public HomeController(IOurTeamService ourTeam)
        {
            _ourTeam = ourTeam;
        }

        public IActionResult Index()
		{
			var teams=_ourTeam.GetAllTeam();
			return View(teams);
		}

		
	}
}
