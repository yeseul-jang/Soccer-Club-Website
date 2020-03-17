using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models.ViewModels
{
    public class MatchPlayerViewModel
    {
        public MatchPlayer MatchPlayer { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }
        public int SelectedTeamPlayerID { get; set; }
        public int TeamID { get; set; }

    }
}
