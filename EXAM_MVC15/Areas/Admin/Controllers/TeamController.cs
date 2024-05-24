using Business.CustomExceptions;
using Business.Service.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EXAM_MVC15.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            var team=_teamService.GetAllTeams();
            return View(team);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (!ModelState.IsValid)  return View();

            try
            {
                _teamService.AddTeam(team);
            }catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(PhotoLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(PhotoFileException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }



            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _teamService.Delete(id);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Update(int id)
        {
            var team = _teamService.GetTeam(x => x.Id == id);
            if(team == null)
            {

                return View("Error");
            }
            return View(team);
        }
        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _teamService.Update(team.Id, team);
            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(PhotoLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
