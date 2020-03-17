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
    public class MatchPlayerController : Controller
    {
        private IMatchRepository matchRepository;
        private ITeamRepository teamRepository;

        public MatchPlayerController(IMatchRepository matchRepository, ITeamRepository teamRepository)
        {
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
        }

        public IActionResult Edit(int matchID, int matchPlayerID)
        {
            MatchPlayer matchPlayer = matchRepository.Matches.FirstOrDefault(m => m.MatchID == matchID)
                    .MatchPlayers.FirstOrDefault(mp=>mp.MatchPlayerID==matchPlayerID);
            Team team = teamRepository.Teams.FirstOrDefault(t => t.TeamID == matchPlayer.TeamPlayer.Team.TeamID);
            MatchPlayerViewModel modelView = new MatchPlayerViewModel
            {
                MatchPlayer = matchPlayer,
                TeamPlayers = team.Players.ToList(),
                TeamID = team.TeamID,
                SelectedTeamPlayerID = matchPlayer.TeamPlayer.TeamPlayerID
            };
            return View(modelView);
        }

        [HttpPost]
        public IActionResult Edit(MatchPlayerViewModel model, int matchId, int teamId)
        {
            Match match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchId);
            Team team = teamRepository.Teams.FirstOrDefault(t => t.TeamID == teamId);
            if (ModelState.IsValid)
            {
                model.MatchPlayer.Match = match;
                model.MatchPlayer.TeamPlayer = team.Players.FirstOrDefault(tp => tp.TeamPlayerID==model.SelectedTeamPlayerID);

                if (model.MatchPlayer.MatchPlayerID == 0) 
                {
                    match.MatchPlayers.Add(model.MatchPlayer);
                }
                else
                {
                    MatchPlayer matchPlayerDb = match.MatchPlayers.First(mp => mp.MatchPlayerID == model.MatchPlayer.MatchPlayerID);
                    matchPlayerDb.TeamPlayer = model.MatchPlayer.TeamPlayer;
                    matchPlayerDb.Goals = model.MatchPlayer.Goals;
                    matchPlayerDb.YellowCards = model.MatchPlayer.YellowCards;
                    matchPlayerDb.RedCards = model.MatchPlayer.RedCards;
                }

                calculateGoals(match);

                matchRepository.Save(match);
                TempData["message"] = $"An event has been created to {model.MatchPlayer.TeamPlayer.Player.FullName} for the match.";
                return RedirectToAction("Detail", "Match", new { matchId });
            }
            else
            {
                MatchPlayerViewModel modelView = new MatchPlayerViewModel
                {
                    MatchPlayer = new MatchPlayer { Match = match },
                    TeamPlayers = team.Players.ToList(),
                    TeamID = team.TeamID
                };
                return View(modelView);
            }
        }

        private void calculateGoals(Match match)
        {
            match.HomeGoals = match.MatchPlayers.OfType<MatchPlayer>()
                                                .Where(mp => mp.TeamPlayer.Team == match.HomeTeam)
                                                .Sum(mp => mp.Goals);
            match.AwayGoals = match.MatchPlayers.OfType<MatchPlayer>()
                                                .Where(mp => mp.TeamPlayer.Team == match.AwayTeam)
                                                .Sum(mp => mp.Goals);
        }

        public IActionResult Delete(MatchPlayerViewModel model, int matchId, int matchPlayerId)
        {
            Match match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchId);
            if (ModelState.IsValid)
            {
                MatchPlayer matchPlayer = match.MatchPlayers.FirstOrDefault(mp => mp.MatchPlayerID == matchPlayerId);
                match.MatchPlayers.Remove(matchPlayer);
                calculateGoals(match);
                matchRepository.Save(match);
                TempData["message"] = $"An event has been removed for {matchPlayer.TeamPlayer.Player.FullName}.";
            }
            return RedirectToAction("Detail", "Match", new { matchId });
        }

        public IActionResult Create(int matchId, int teamId)
        {
            Match match = matchRepository.Matches.FirstOrDefault(c => c.MatchID == matchId);
            Team team = teamRepository.Teams.FirstOrDefault(t => t.TeamID == teamId);

            MatchPlayerViewModel model = new MatchPlayerViewModel
            {
                MatchPlayer = new MatchPlayer { Match = match },
                TeamPlayers = team.Players.ToList(),
                TeamID = team.TeamID
            };
            return View("Edit", model);
        }

    }
}