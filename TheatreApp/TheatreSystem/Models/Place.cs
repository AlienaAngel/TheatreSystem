using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Place
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public Place()
        {
            Tickets = new List<Ticket>();
        }

        public Place(int id, int number)
        {
            Id = id;
            Number = number;
            Tickets = new List<Ticket>();
        }
    }
}