using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bilet10.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurTeamController : Controller
    {
        IOurTeamService _ourTeamService;

        public OurTeamController(IOurTeamService ourTeamService)
        {
            _ourTeamService = ourTeamService;
        }
       
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Index()
        {
            var teams = _ourTeamService.GetAllTeam();
            return View(teams);
        }

        [Authorize(Roles = "SuperAdmin")]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OurTeam ourTeam)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ourTeamService.Create(ourTeam);
            }
            catch (NotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            catch(FileContentTypeException ex)
            {
                ModelState.AddModelError("PhotoFile", ex.Message);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "SuperAdmin")]

        public IActionResult Delete(int id)
        {
            _ourTeamService.Delete(id);
            return RedirectToAction("Index");   
        }
        [Authorize(Roles = "SuperAdmin")]

        public IActionResult Update(int id)
        {
            var team=_ourTeamService.GetTeam(x=>x.Id == id);

            return View(team);
        }
        [HttpPost]
        public IActionResult Update(OurTeam team)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _ourTeamService.Update(team.Id,team);
            }
            catch (NotFoundException ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            catch (FileContentTypeException ex)
            {
                ModelState.AddModelError("PhotoFile", ex.Message);
            }
            return RedirectToAction("Index");

        }
    }
}
