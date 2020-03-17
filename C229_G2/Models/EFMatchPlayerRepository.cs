using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class EFMatchPlayerRepository : IMatchPlayerRepository
    {
        private ApplicationDbContext context;

        public EFMatchPlayerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<MatchPlayer> MatchPlayers => context.MatchPlayers;

        public MatchPlayer Delete(int matchPlayerId)
        {
            MatchPlayer matchPlayer = context.MatchPlayers.FirstOrDefault(c => c.MatchPlayerID == matchPlayerId);
            if (matchPlayer != null)
            {
                context.MatchPlayers.Remove(matchPlayer);
                context.SaveChanges();
            }
            return matchPlayer;
        }

        public void Save(MatchPlayer matchPlayer)
        {
            if (matchPlayer.MatchPlayerID==0)
            {
                context.MatchPlayers.Add(matchPlayer);
            }
            else
            {
                MatchPlayer matchPlayerDb = context.MatchPlayers.FirstOrDefault(c => c.MatchPlayerID == matchPlayer.MatchPlayerID);
                matchPlayerDb.TeamPlayer = matchPlayer.TeamPlayer;
                matchPlayerDb.RedCards = matchPlayer.RedCards;
                matchPlayerDb.YellowCards = matchPlayer.YellowCards;
                matchPlayerDb.Goals = matchPlayer.Goals;
            }
            context.SaveChanges();
        }
    }
}
