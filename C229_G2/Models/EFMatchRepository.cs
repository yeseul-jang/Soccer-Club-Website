using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class EFMatchRepository : IMatchRepository
    {
        private ApplicationDbContext context;

        public EFMatchRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Match> Matches => context
            .Matches
            .Include(m => m.HomeTeam)
            .Include(m => m.AwayTeam)
            .Include(m => m.MatchPlayers)
                .ThenInclude(mp => mp.TeamPlayer)
                    .ThenInclude(p => p.Player);

        public Match Delete(int matchId)
        {
            Match match = context.Matches.FirstOrDefault(c => c.MatchID == matchId);
            if (match != null)
            {
                context.Matches.Remove(match);
                context.SaveChanges();
            }
            return match;
        }

        public void Save(Match match)
        {
            if (match.MatchID==0)
            {
                context.Matches.Add(match);
            }
            else
            {
                Match matchDb = context.Matches.FirstOrDefault(c => c.MatchID == match.MatchID);
                matchDb.Date = match.Date;
                matchDb.Location = match.Location;
                matchDb.Referee = match.Referee;
                matchDb.HomeTeam = match.HomeTeam;
                matchDb.AwayTeam = match.AwayTeam;

                matchDb.HomeGoals = match.HomeGoals;
                matchDb.HomeFouls = match.HomeFouls;
                matchDb.HomeCorners = match.HomeCorners;
                matchDb.HomeShotsOnTarget = match.HomeShotsOnTarget;

                matchDb.AwayGoals = match.AwayGoals;
                matchDb.AwayFouls = match.AwayFouls;
                matchDb.AwayCorners = match.AwayCorners;
                matchDb.AwayShotsOnTarget = match.AwayShotsOnTarget;
            }
            context.SaveChanges();
        }
    }
}
