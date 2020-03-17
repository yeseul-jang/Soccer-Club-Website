using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace C229_G2.Models
{
    public class MatchPlayer
    {
        public int MatchPlayerID { get; set; }
        public Match Match { get; set; }
        public TeamPlayer TeamPlayer { get; set; }

        [Range(0, 1000)]
        public int RedCards { get; set; }
        [Range(0, 1000)]
        public int YellowCards { get; set; }
        [Range(0, 1000)]
        public int Goals { get; set; }
    }

}