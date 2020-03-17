using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [Range(0, 100)]
        public int Age { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [BindNever]
        public Club Club { get; set; }
        [Range(0.0, 1000)]
        public decimal Height { get; set; }
        [Range(0.0, 1000)]
        public decimal Weight { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
