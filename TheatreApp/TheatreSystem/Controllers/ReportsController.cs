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
    public class ReportsController : Controller
    {
        private Context db = new Context();

        // GET: Reports
        public ActionResult Index()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                dynamic orders = db
                .Orders
                .Where(x => !x.IsConfirmed)
                .Select(x => new
                {
                    Name = x.User.Name,
                    Email = x.User.Email,
                    Debt = x.Tickets.Select(y => y.Place.Zone.Cost).Sum()
                })
                .AsEnumerable()
                .Select(c => c.ToExpando());
                var query =
                    db
                    .Orders
                    .Where(x => !x.IsConfirmed).Select(
                        x => x.Tickets
                        .Select(y => y.Place.Zone.Cost)
                        .Sum()
                    );

                decimal? costNonConfirmed = query.Count()>0?query.Sum():0m;

                ViewBag.NotPaid = costNonConfirmed;

                return View(orders);
            }
            else
            {
               return Redirect("~/");
            } 
        }
    }


}