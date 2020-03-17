using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class Match
    { 
        public int MatchID { get; set; }
        public DateTime Date { get; set; }
        public String Location { get; set; }
        public String Referee { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        [Range(0, 1000)]
        public int HomeGoals { get; set; }
        [Range(0, 1000)]
        public int HomeFouls { get; set; }
        [Range(0, 1000)]
        public int HomeCorners { get; set; }
        [Range(0, 1000)]
        public int HomeShotsOnTarget { get; set; }

        [Range(0, 1000)]
        public int AwayGoals { get; set; }
        [Range(0, 1000)]
        public int AwayFouls { get; set; }
        [Range(0, 1000)]
        public int AwayCorners { get; set; }
        [Range(0, 1000)]
        public int AwayShotsOnTarget { get; set; }

        public ICollection<MatchPlayer> MatchPlayers { get; set; }

        public Match()
        {
            Date = DateTime.Now;
        }

    }
}
