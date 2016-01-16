using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    public class LoginController : Controller
    {

        private Context db = new Context();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(String email, String password)
        {
            User user = db.Users
                .Where(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();

            if (user != null)
            {
                if (user.Password == password)
                {

                    Session["user"] = user;
                    return Redirect("~/Play");
                }
            }


            return View();

        }


    }
}
