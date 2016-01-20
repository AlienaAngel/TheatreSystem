using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Play> Plays { get; set; }

        public Author()
        {
            Plays = new List<Play>();
        }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
            Plays = new List<Play>();
        }

    }
}