using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }

        public virtual List<Order> Orders { get; set; }

        public virtual List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
            Orders = new List<Order>();
        }

        public User(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Roles = new List<Role>();
            Orders = new List<Order>();
        }


        public bool HasRole(String role)
        {
            return Roles.Where(x => x.Name.Equals(role)).Count() > 0;
        }

    }
}