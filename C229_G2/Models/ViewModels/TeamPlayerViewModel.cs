using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models.ViewModels
{
    public class TeamPlayerViewModel
    {
        public TeamPlayer TeamPlayer { get; set; }
        public ICollection<Player> Players { get; set; }
        public int SelectedTeamPlayerID { get; set; }
    }
}
