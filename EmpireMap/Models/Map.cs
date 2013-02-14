using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class Map
    {
        public int MapId { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}