using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public bool IsConfirmed { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public Order()
        {
            Tickets = new List<Ticket>();
        }

        public Order(int id, bool isConfirmed)
        {
            Id = id;
            IsConfirmed = isConfirmed;
            Tickets = new List<Ticket>();
        }
    }
}