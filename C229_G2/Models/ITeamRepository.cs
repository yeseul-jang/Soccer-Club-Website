using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public interface ITeamRepository
    {
        IQueryable<Team> Teams { get; }
        void Save(Team team);
        Team Delete(int teamId);

    }
}
