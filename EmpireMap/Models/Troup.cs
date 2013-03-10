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
        [Range(1,int.MaxValue)]
        public int TroupCount { get; set; }

        public bool IsDeff { get; set; }
        public bool IsVeteran { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}