using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Filters;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Auth]
    public class AccountController : Controller
    {
        private Context db = new Context();

        // GET: Account
        public ActionResult Index()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.UserRole, Session))
            {
                User user = AuthorizationHelper.CurrentUser(Session);
                var orders = db.Orders.Where(x => x.User.Id == user.Id).AsEnumerable();

                //ViewBag.MyOrders = orders;

                return View(orders);
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}