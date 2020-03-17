using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace C229_G2.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        [Required]
        public string Name { get; set; }
        public Club Club { get; set; }
        public ICollection<TeamPlayer> Players { get; set; }

    }
}