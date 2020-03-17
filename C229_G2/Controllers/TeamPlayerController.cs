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
    [Authorize(Roles = "Admin,General")]
    public class TeamPlayerController : Controller
    {
        private ITeamRepository teamRepository;
        private IPlayerRepository playerRepository;

        public TeamPlayerController(ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            this.teamRepository = teamRepository;
            this.playerRepository = playerRepository;
        }

        public IActionResult Edit(TeamPlayerViewModel model, int teamId)
        {
            Team team = teamRepository.Teams.FirstOrDefault(c => c.TeamID == teamId);
            if (ModelState.IsValid)
            {
                model.TeamPlayer.Team = team;
                model.TeamPlayer.Player = playerRepository.Players.FirstOrDefault(p => p.PlayerID==model.SelectedTeamPlayerID);
                team.Players.Add(model.TeamPlayer);
                teamRepository.Save(team);
                TempData["message"] = $"{model.TeamPlayer.Player.FirstName} has been assigned to the team.";
                return RedirectToAction("Detail", "Team", new { teamId });
            }
            else
            {
                TeamPlayerViewModel modelView = new TeamPlayerViewModel
                {
                    TeamPlayer = new TeamPlayer { Team = team },
                    Players = playerRepository.Players.Where(p => p.Club == team.Club).ToList()
                };
                return View(modelView);
            }
        }

        public IActionResult Create(int teamId)
        {
            Team team = teamRepository.Teams.FirstOrDefault(c => c.TeamID == teamId);
            TeamPlayerViewModel model = new TeamPlayerViewModel
            {
                TeamPlayer = new TeamPlayer { Team = team },
                Players = playerRepository.Players.Where(p => p.Club == team.Club).ToList()
            };
            return View("Edit", model);
        }

    }
}