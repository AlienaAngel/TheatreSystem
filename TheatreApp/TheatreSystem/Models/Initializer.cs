using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TheatreSystem.Controllers;

namespace TheatreSystem.Models
{
    //public class Initializer : CreateDatabaseIfNotExists<Context>
        public class Initializer : DropCreateDatabaseAlways<Context>


    {
        protected override void Seed(Context context)
        {
            // Лучше AuthorizationHelper переместить в модель 
            context.Roles.Add(new Role(1, AuthorizationHelper.UserRole));
            context.Roles.Add(new Role(2, AuthorizationHelper.ModeratorRole));
            context.Roles.Add(new Role(3, AuthorizationHelper.AdminRole));
            context.SaveChanges();


            //base.Seed(context);
        }


    }
}