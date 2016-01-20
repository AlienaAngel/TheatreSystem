using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public virtual List<Play> Plays { get; set; }

        public Genre()
        {
            Plays = new List<Play>();
        }

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
            Plays = new List<Play>();
        }
    }
}