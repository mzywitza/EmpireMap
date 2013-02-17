using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class MapCastleModel
    {
        public int Id { get; set; }
        public List<Map> Maps { get; set; }
        public List<Castle> Castles { get; set; }
    }
}