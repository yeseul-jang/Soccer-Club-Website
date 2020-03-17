using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public interface IMatchRepository
    {
        IQueryable<Match> Matches { get; }
        void Save(Match match);
        Match Delete(int matchId);

    }
}
