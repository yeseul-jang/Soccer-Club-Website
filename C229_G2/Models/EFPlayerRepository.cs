using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class EFPlayerRepository : IPlayerRepository
    {
        private ApplicationDbContext context;

        public EFPlayerRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Player> Players => context.Players;

        public Player Delete(int playerId)
        {
            Player player = context.Players.FirstOrDefault(c => c.PlayerID == playerId);
            if (player != null)
            {
                context.Players.Remove(player);
                context.SaveChanges();
            }
            return player;
        }

        public void Save(Player player)
        {
            if (player.PlayerID==0)
            {
                context.Players.Add(player);
            }
            else
            {
                Player playerDb = context.Players.FirstOrDefault(c => c.PlayerID == player.PlayerID);
                playerDb.FirstName = player.FirstName;
                playerDb.LastName = player.LastName;
                playerDb.Age = player.Age;
                playerDb.Phone = player.Phone;
                playerDb.Email = player.Email;
                playerDb.Club = player.Club;
                playerDb.Height = player.Height;
                playerDb.Weight = player.Weight;

            }
            context.SaveChanges();
        }
    }
}
