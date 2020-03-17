using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public interface IClubRepository
    {
        IQueryable<Club> Clubs { get; }
        void Save(Club club);
        Club Delete(int clubId);

    }
}
