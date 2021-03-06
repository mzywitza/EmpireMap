﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Display(Name="Status")]
        public string AllianceStatus { get; set; }

        public int? UserId { get; set; }
        public UserProfile User { get; set; }

        public virtual List<Castle> Castles { get; set; }

        public virtual List<Troup> Troups { get; set; }
    }
}