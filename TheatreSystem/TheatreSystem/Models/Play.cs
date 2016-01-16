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
        public string Info { get; set; }
        public virtual List<Ticket> Tickets { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual List<Date> Dates { get; set; }

        public Play()
        {
            Tickets = new List<Ticket>();
        }

        public Play(int id, string name, string info, int authorId, int genreId)
        {
            Id = id;
            Name = name;
            Info = info;
            Tickets = new List<Ticket>();
            AuthorId = authorId;
            GenreId = genreId;
        }

    }
}