using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C229_G2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C229_G2.Controllers
{
    [Authorize(Roles = "Admin,General")]
    public class PlayerController : Controller
    {
        private IClubRepository repository;

        public PlayerController(IClubRepository repo)
        {
            repository = repo;
        }

        public IActionResult Edit(Player player, int clubId)
        {
            if (ModelState.IsValid)
            {
                Club club = repository.Clubs.FirstOrDefault(c => c.ClubID == clubId);
                player.Club = club;
                club.Players.Add(player);
                repository.Save(club);
                TempData["message"] = $"{player.FirstName} has been added.";
                return RedirectToAction("Detail", "Club", new { clubId });
            }
            else
            {
                return View(player);
            }
        }

        public IActionResult Create(int clubId)
        {
            Club club = repository.Clubs.FirstOrDefault(c => c.ClubID == clubId);
            return View("Edit", new Player { Club = club });
        }
    }
}