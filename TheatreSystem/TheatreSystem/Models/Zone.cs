using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public virtual List<Place> Places { get; set; }

        public Zone()
        {
            Places = new List<Place>();
        }

        public Zone(int id, decimal cost, string name, int amount)
        {
            Id = id;
            Cost = cost;
            Name = name;
            Places = new List<Place>();
        }

    }
}