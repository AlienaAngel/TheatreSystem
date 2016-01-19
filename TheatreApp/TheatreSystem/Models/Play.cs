using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Play
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DatePlay { get; set; }
        public string Info { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public Play() {
            Tickets = new List<Ticket>();
        }

        public Play(int id, DateTime datePlay, string info, string name)
        {
            Id = id;
            DatePlay = datePlay;
            Info = info;
            Name = name;
            Tickets = new List<Ticket>();
        }

    }
}