using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models.ViewModels
{
    public class TeamViewModel
    {
        public Team Team { get; set; }
        public IEnumerable<Club> Clubs { get; set; }
        public int SelectedClubID { get; set; }
    }
}
