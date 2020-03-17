using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C229_G2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C229_G2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClubController : Controller
    {
        private IClubRepository repository;

        public ClubController(IClubRepository repo)
        {
            repository = repo;
        }

        [AllowAnonymous]
        public IActionResult List() => View(repository.Clubs);

        public IActionResult Edit(int clubID)
        {
            return View(repository.Clubs.FirstOrDefault(c=>c.ClubID==clubID));
        }

        [HttpPost]
        public IActionResult Edit(Club club)
        {
            if (ModelState.IsValid)
            {
                repository.Save(club);
                TempData["message"] = $"{club.Name} has been saved.";
                return RedirectToAction("List");
            }
            else
            {
                return View(club);
            }
        }

        public IActionResult Create() => View("Edit", new Club());

        public IActionResult Delete()
        {

            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Delete(int clubId)
        {
            Club club = repository.Clubs.FirstOrDefault(c=>c.ClubID==clubId);
            try
            {
                club = repository.Delete(club.ClubID);
                TempData["message"] = $"{club.Name} has been deleted";

            }
            catch (Exception)
            {
                TempData["messageError"] = $"{club.Name} cannot be deleted because there are teams associated.";
            }
            return RedirectToAction("List");
        }

        [AllowAnonymous]
        public IActionResult Detail(int clubID)
        {
            return View(repository.Clubs.FirstOrDefault(c => c.ClubID == clubID));
        }

    }
}