using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class Skill
    {
        public int SkillID { get; set; }
        public Player Player { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

    }
}
