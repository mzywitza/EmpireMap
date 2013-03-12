using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class Troup
    {
        public int TroupId { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }

        public int MapId { get; set; }
        public Map Map { get; set; }
        
        [Required]
        [Range(0,999999)]
        [Display(Name = "Leichte Verteidiger")]
        public int Deff { get; set; }

        [Required]
        [Range(0, 999999)]
        [Display(Name = "Schwere Verteidiger")]
        public int EnhancedDeff { get; set; }

        [Required]
        [Range(0, 999999)]
        [Display(Name = "Leichte Angreifer")]
        public int Off { get; set; }

        [Required]
        [Range(0, 999999)]
        [Display(Name = "Schwere Angreifer")]
        public int EnhancedOff { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}