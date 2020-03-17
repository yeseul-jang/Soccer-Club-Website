using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models.ViewModels
{
    public class MatchViewModel
    {
        public Match Match { get; set; }
        public MatchPlayer players { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public int SelectedHomeTeamID { get; set; }
        public int SelectedAwayTeamID { get; set; }
        public DateTime? Date { get; set; }
        public IEnumerable<MatchPlayer> HomeEvents { get; set; }
        public IEnumerable<MatchPlayer> AwayEvents { get; set; }

    }
}
