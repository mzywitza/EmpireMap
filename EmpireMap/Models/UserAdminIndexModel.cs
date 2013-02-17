using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class UserAdminIndexModel
    {
        [Display(Name="Anmeldename")]
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string ConfirmedBy { get; set; }
        [Display(Name = "Ingame-Name")]
        public string PlayerName { get; set; }
        [Display(Name = "Allianzführung")]
        public bool Leadership { get; set; }
    }
}