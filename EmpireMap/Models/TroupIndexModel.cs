using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmpireMap.Models
{
    public class TroupIndexModel
    {
        public List<Troup> Troups { get; set; }
        public List<Map> Maps { get; set; }
        public Player Player { get; set; }

        public static TroupIndexModel Create(ApplicationContext context, int userId)
        {
            var model = new TroupIndexModel();
            model.Player = context.Players.Where(p => p.UserId == userId).SingleOrDefault();
            if (model.Player == null) return null;
            model.Maps = context.Maps.ToList();
            model.Troups = context.Troups.Include(t => t.Map).Where(t => t.PlayerId == model.Player.PlayerId).ToList();
            return model;
        }
    }
}