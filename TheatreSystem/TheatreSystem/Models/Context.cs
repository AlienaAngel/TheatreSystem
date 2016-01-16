using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TheatreSystem.Models
{
    public class Context : DbContext
    {
        /*

    //Enable-Migrations
//Add-Migration AddDb
// Update-Database
*/

        public Context()
            : base("TheatreDB2")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Zone>()
                   .HasMany<Place>(s => s.Places)
                   .WithRequired(s => s.Zone)
                   .HasForeignKey(s => s.ZoneId);

            modelBuilder.Entity<Place>()
                   .HasMany<Ticket>(s => s.Tickets)
                   .WithRequired(s => s.Place)
                   .HasForeignKey(s => s.PlaceId);

            modelBuilder.Entity<Play>()
                   .HasMany<Date>(s => s.Dates)
                   .WithRequired(s => s.Play)
                   .HasForeignKey(s => s.PlayId);

            modelBuilder.Entity<Author>()
                   .HasMany<Play>(s => s.Plays)
                   .WithRequired(s => s.Author)
                   .HasForeignKey(s => s.AuthorId);

            modelBuilder.Entity<Date>()
                  .HasMany<Ticket>(s => s.Tickets)
                  .WithRequired(s => s.Date)
                  .HasForeignKey(s => s.DateId);

            modelBuilder.Entity<Ticket>()
                   .HasOptional<Order>(s => s.Order)
                   .WithMany(s => s.Tickets)
                   .HasForeignKey(s => s.OrderId);

            modelBuilder.Entity<User>()
                .HasMany<Role>(s => s.Roles)
                .WithMany(c => c.Users)
                .Map(cs =>
                {
                    cs.MapLeftKey("UserId");
                    cs.MapRightKey("RoleId");
                    cs.ToTable("UserRole");
                });

            modelBuilder.Entity<User>()
                  .HasMany<Order>(s => s.Orders)
                  .WithRequired(s => s.User)
                  .HasForeignKey(s => s.UserId);

        }

        public virtual DbSet<Zone> Zones { get; set; }
        public virtual DbSet<Play> Plays { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Date> Dates { get; set; }
        public virtual DbSet<Author> Authors { get; set; }



    }

}
