using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
            Users = new List<User>();
        }
    }
}