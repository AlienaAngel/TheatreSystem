using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Date
    {
        public int Id { get; set; }
        public int PlayId {get; set;}
        public virtual Play Play { get; set; }
        public DateTime DatePlay { get; set; }

        public virtual List<Ticket> Tickets { get; set; }

        public Date()
        {

        }

        public Date(int id, DateTime datePlay)
        {
            Id = id;
            DatePlay = datePlay;
        }


    }
}