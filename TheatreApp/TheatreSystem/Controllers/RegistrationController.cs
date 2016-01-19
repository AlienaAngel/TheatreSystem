using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TheatreSystem.Filters;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Auth]
    public class RegistrationController : Controller
    {
        private Context db = new Context();

        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Name,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {

                Role userRole = db.Roles.Where(x => x.Name == "USER_ROLE").FirstOrDefault();
                user.Roles.Add(userRole);
                db.Users.Add(user);
                db.SaveChanges();

                Session["user"] = user;
                return Redirect("~/Plays");
                //извлечь юзера и добавить в сессию
                // ридерект на главную
            }

            return View( user);
        }
    }
}