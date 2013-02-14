using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class Castle
    {
        public int CastleId { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        
        [Required]
        public string Name { get; set; }
        public bool IsAp { get; set; }
        public int MapId { get; set; }
        public Map Map { get; set; }
        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}