using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public interface IMatchPlayerRepository
    {
        IQueryable<MatchPlayer> MatchPlayers { get; }
        void Save(MatchPlayer matchPlayer);
        MatchPlayer Delete(int matchPlayerID);

    }
}
