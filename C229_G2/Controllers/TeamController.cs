using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C229_G2.Models;
using C229_G2.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C229_G2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private ITeamRepository teamRepository;
        private IClubRepository clubRepository;

        public TeamController(ITeamRepository teamRepository, IClubRepository clubRepository)
        {
            this.teamRepository = teamRepository;
            this.clubRepository = clubRepository;
        }

        [AllowAnonymous]
        public IActionResult List() => View(teamRepository.Teams);

        public IActionResult Edit(int teamID)
        {
            Team team = teamRepository.Teams.FirstOrDefault(c => c.TeamID == teamID);

            return View("Edit",
                 new TeamViewModel
                 {
                     Team = team,
                     Clubs = clubRepository.Clubs,
                     SelectedClubID = team.Club.ClubID
                 });
        }

        [HttpPost]
        public IActionResult Edit(TeamViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Team.Club = clubRepository.Clubs.FirstOrDefault(c => c.ClubID==model.SelectedClubID);
                teamRepository.Save(model.Team);
                TempData["message"] = $"{model.Team.Name} has been saved.";
                return RedirectToAction("List");
            }
            else
            {
                model.Clubs = clubRepository.Clubs;
                return View(model);
            }
        }

        public IActionResult Create() => 
            View("Edit", 
                 new TeamViewModel
                 {
                     Team = new Team(),
                     Clubs = clubRepository.Clubs
                 });
        public IActionResult Delete()
        {
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int teamId)
        {
            Team team = teamRepository.Delete(teamId);
            if (team != null)
            {
                TempData["message"] = $"{team.Name} has been deleted";
            }
            return RedirectToAction("List");
        }

        [AllowAnonymous]
        public IActionResult Detail(int teamID)
        {
            return View(new TeamViewModel()
            {
                Team = teamRepository.Teams.FirstOrDefault(c => c.TeamID == teamID),
                Clubs = clubRepository.Clubs.ToList()
            });
        }

    }
}