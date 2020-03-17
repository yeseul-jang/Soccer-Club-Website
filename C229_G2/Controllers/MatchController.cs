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
    public class MatchController : Controller
    {
        private IMatchRepository matchRepository;
        private ITeamRepository teamRepository;

        public MatchController(IMatchRepository matchRepository, ITeamRepository teamRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
        }


        [AllowAnonymous]
        public IActionResult List() => View(new MatchViewModel { Matches = matchRepository.Matches });

        [HttpPost]
        [AllowAnonymous]
        public IActionResult List(DateTime Date)
        {
            ViewBag.start = Date;
            var filteredMatches = matchRepository.Matches.Where(x => x.Date.Month == Date.Month &&
                                                     x.Date.Day == Date.Day &&
                                                     x.Date.Year == Date.Year)
                                                     .OrderByDescending(x => x.MatchID).ToList();
            return View(new MatchViewModel { Matches = filteredMatches });
        }


        public IActionResult Edit(int matchID)
        {
            Match match = matchRepository.Matches.FirstOrDefault(m => m.MatchID == matchID);

            return View("Edit",
                 new MatchViewModel
                 {
                     Match = match,
                     Teams = teamRepository.Teams,
                     SelectedHomeTeamID = match.HomeTeam.TeamID,
                     SelectedAwayTeamID = match.AwayTeam.TeamID,
                     Date = match.Date
                 });
        }
         

        [HttpPost]
        public IActionResult Edit(MatchViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Match.HomeTeam = teamRepository.Teams.FirstOrDefault(t => t.TeamID == model.SelectedHomeTeamID);
                model.Match.AwayTeam = teamRepository.Teams.FirstOrDefault(t => t.TeamID == model.SelectedAwayTeamID);
                model.Date = model.Match.Date;
                matchRepository.Save(model.Match);
                TempData["message"] = $"Match ID {model.Match.MatchID} has been saved.";
                return RedirectToAction("List");
            }
            else
            {
                model.Teams = teamRepository.Teams;
                return View(model);
            }
        }
        public IActionResult Create() =>
            View("Edit",
                 new MatchViewModel
                 {
                     Match = new Match(),
                     Teams = teamRepository.Teams
                 });

       
        public IActionResult Delete()
        {
           
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Delete(int matchId)
        {
            Match match = matchRepository.Matches.FirstOrDefault(m => m.MatchID == matchId);
            try
            {
                match = matchRepository.Delete(match.MatchID);
                TempData["message"] = $"Match ID {match.MatchID} has been deleted";
            }
            catch (Exception)
            {
                TempData["messageError"] = $"Match ID {match.MatchID} cannot be deleted because there is information associated.";
            }
            return RedirectToAction("List");
        }

        [AllowAnonymous]
        public IActionResult Event(int matchID)
        {
            Match match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchID);
            return View(new MatchViewModel() {
                Match = match,
                Teams = teamRepository.Teams.ToList(),
                HomeEvents = match.MatchPlayers.Where(mp => mp.TeamPlayer.Team == match.HomeTeam).ToList(),
                AwayEvents = match.MatchPlayers.Where(mp => mp.TeamPlayer.Team == match.AwayTeam).ToList()
            });
        }
        [AllowAnonymous]
        public IActionResult Detail(int matchID)
        {
            return View(new MatchViewModel()
            {
                Match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchID),
                Teams = teamRepository.Teams.ToList()
            });
        }

        public IActionResult EditST(int matchID)
        {
            Match match = matchRepository.Matches.FirstOrDefault(m => m.MatchID == matchID);

            return View("EditST",
                 new MatchViewModel
                 {
                     Match = match
                 });
        }

        [HttpPost]
        public IActionResult EditST(MatchViewModel model)
        {
            Match match = matchRepository.Matches.FirstOrDefault(m => m.MatchID == model.Match.MatchID);
            match.HomeFouls = model.Match.HomeFouls;
            match.HomeCorners = model.Match.HomeCorners;
            match.HomeShotsOnTarget = model.Match.HomeShotsOnTarget;

            match.AwayFouls = model.Match.AwayFouls;
            match.AwayCorners = model.Match.AwayCorners;
            match.AwayShotsOnTarget = model.Match.AwayShotsOnTarget;



            if (ModelState.IsValid)
            {
                matchRepository.Save(match);
                TempData["message"] = $"Match ID {model.Match.MatchID} has been saved.";
                return View("Detail", new MatchViewModel
                                       {
                                           Match = match
                                      
                                       });
            }
            else
            {
                return View(model);
            }
        }
        
        [AllowAnonymous]
        public IActionResult LineUp(int matchID)
        {
            return View(new MatchViewModel()
            {
                Match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchID),
                Teams = teamRepository.Teams.ToList()
            });
        }
    }
}