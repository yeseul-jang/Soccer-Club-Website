using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public interface IPlayerRepository
    {
        IQueryable<Player> Players { get; }
        void Save(Player player);
        Player Delete(int playerId);

    }
}
