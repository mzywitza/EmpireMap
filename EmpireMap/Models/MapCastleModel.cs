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
        
        public string MapName
        {
            get
            {
                var defName = "unbekannt...";
                if (Maps == null) return defName;
                var map = Maps.SingleOrDefault(m => m.MapId == Id);
                if (map == null) return defName;
                return map.Name;
            }
        }
    }
}