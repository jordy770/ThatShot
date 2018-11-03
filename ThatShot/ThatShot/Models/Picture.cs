using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThatShot.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string File { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        [StringLength(300, MinimumLength = 3)]
        [Required]
        public string Description { get; set; }

        public string Genre { get; set; }
        
        public string User { get; set; }

        public bool show { get; set; }

    }
}
