using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class DatePlay
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PlayId { get; set; }
        public Play Play { get; set; }
        public virtual List<Ticket> Tickets { get; set; }

        public DatePlay ()
        {
            Tickets = new List<Ticket>();
        }

        public DatePlay(int id, DateTime date)
        {
            Id = id;
            Date = date;
            Tickets = new List<Ticket>();
        }
    }
}