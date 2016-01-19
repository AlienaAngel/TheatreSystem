using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Serial { get; set; }

        public int PlayId { get; set; }
        public virtual Play Play { get; set; }

        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public Ticket() { }

        public Ticket(int id, string serial)
        {
            Id = id;
            Serial = serial;
        }
    }
}