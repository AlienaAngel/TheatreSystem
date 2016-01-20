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
        

        public int? GenreId { get; set; }
        public Genre Genre { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public virtual List<DatePlay> DatePlays { get; set; }
        

        public Play() {
            DatePlays = new List<DatePlay>();
        }

        public Play(int id, string info, string name, Genre genre, Author author)
        {
            Id = id;
            Info = info;
            Name = name;
            Genre = genre;
            Author = author;
            DatePlays = new List<DatePlay>();
        }

    }
}