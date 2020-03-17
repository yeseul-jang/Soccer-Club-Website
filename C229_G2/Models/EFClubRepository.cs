using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class EFClubRepository : IClubRepository
    {
        private ApplicationDbContext context;

        public EFClubRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Club> Clubs => context.Clubs.Include(c=>c.Players);

        public Club Delete(int clubId)
        {
            Club club = context.Clubs.FirstOrDefault(c => c.ClubID == clubId);
            if (club != null)
            {
                context.Clubs.Remove(club);
                context.SaveChanges();
            }
            return club;
        }

        public void Save(Club club)
        {
            if (club.ClubID==0)
            {
                context.Clubs.Add(club);
            }
            else
            {
                Club clubDb = context.Clubs.FirstOrDefault(c => c.ClubID == club.ClubID);
                clubDb.Name = club.Name;
                clubDb.Address = club.Address;
                clubDb.Email = club.Email;
                clubDb.Phone = club.Phone;
            }
            context.SaveChanges();
        }
    }
}
