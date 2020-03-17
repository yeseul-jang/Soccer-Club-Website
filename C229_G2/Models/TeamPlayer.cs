using System.ComponentModel.DataAnnotations;

namespace C229_G2.Models
{
    public class TeamPlayer
    {
        public int TeamPlayerID { get; set; }
        [Required]
        public Team Team { get; set; }
        public Player Player { get; set; }
        [Required]
        public string Position { get; set; }
    }
}