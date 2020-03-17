using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class EFTeamRepository : ITeamRepository
    {
        private ApplicationDbContext context;

        public EFTeamRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Team> Teams => context
            .Teams
            .Include(t=>t.Players).ThenInclude(t=>t.Player)
            .Include(t=>t.Club);

        public Team Delete(int teamId)
        {
            Team team = context.Teams.FirstOrDefault(c => c.TeamID == teamId);
            if (team != null)
            {
                context.Teams.Remove(team);
                context.SaveChanges();
            }
            return team;
        }

        public void Save(Team team)
        {
            if (team.TeamID==0)
            {
                context.Teams.Add(team);
            }
            else
            {
                Team teamDb = context.Teams.FirstOrDefault(c => c.TeamID == team.TeamID);
                teamDb.Name = team.Name;
                teamDb.Club = team.Club;
            }
            context.SaveChanges();
        }
    }
}
