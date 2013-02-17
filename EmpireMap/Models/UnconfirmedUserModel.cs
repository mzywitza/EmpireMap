using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class UnconfirmedUserModel
    {
        public int UserId { get; set; }
        public int PlayerId { get; set; }
        public string UserName { get; set; }
        public string PlayerName { get; set; }
        public int Castles { get; set; }
        public bool Leadership { get; set; }
        public bool Confirm { get; set; }
    }
}