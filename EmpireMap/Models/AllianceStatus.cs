using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpireMap.Models
{
    public static class AllianceStatus
    {
        private static readonly Dictionary<string, string> StatusDict = new Dictionary<string, string>() {
            {"Member","Mitglied"},
            {"Allied","Verbündeter"},
            {"NAP","NAP"},
            {"Foe","Feind"},
            {"Victim","Opfer"}
        };

        public static IEnumerable<SelectListItem> GetSelectList(Player player) {
            return  StatusDict.Select(kvp=>new SelectListItem {Text = kvp.Value, Value = kvp.Key, Selected = kvp.Key == player.AllianceStatus});
        }

        public static string GetDisplay(string status)
        {
            return StatusDict.ContainsKey(status) ?
                StatusDict[status]:
                "";
        }

        public const string Member = "Member";
        public const string Allied = "Allied";
        public const string Nap = "NAP";
        public const string Foe = "Foe";
        public const string Victim = "Victim";

    }
}