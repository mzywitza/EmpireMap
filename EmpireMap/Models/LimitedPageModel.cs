using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class LimitedPageModel
    {
        public Player Player { get; set; }
        public List<Castle> Castles { get; set; }
    }
}