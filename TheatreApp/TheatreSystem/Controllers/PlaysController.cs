using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Filters;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Auth]
    public class PlaysController : Controller
    {
        private Context db = new Context();

        // GET: Plays
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var plays = db.Plays.Select(x => x);
            if (!String.IsNullOrEmpty(searchString))
            {
                plays = plays.Where(s => s.Name.Contains(searchString)
                                       || s.Info.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    plays = plays.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    //TODO
                    //plays = plays.OrderBy(s => s.DatePlay);
                    break;
                default:  // Name ascending 
                    plays = plays.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(plays.ToPagedList(pageNumber, pageSize));


            //return View(db.Plays.ToList());
        }

        //public ActionResult GetByDate()
        //{
        //    return View(db.Plays.Where();
        //}

        // GET: Plays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Play play = db.Plays.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        public ActionResult TicketsInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            //List<Ticket> tickets = db.Plays.Find(id).Tickets;



            //List<ZoneInfoViewModel> zoneInfos = new List<ZoneInfoViewModel>();
            //foreach (Zone z in db.Zones)
            //{
            //    int count = z.Places.Count();
            //    int ordered = tickets.Where(x => x.Place.ZoneId == z.Id).Count();
            //    //    int count = db.Plays.Find(id).Tickets.Where(x => x.ZoneId == z.Id).Count();
            //    //    int ordered = db.Plays.Find(id).Tickets.Where(x => x.Order != null && x.ZoneId == z.Id).Count();
            //    count = count - ordered;

            //    List<Place> or = tickets.Select(x => x.Place).Where(x=>x.Zone.Id==z.Id).ToList();
            //    List<Place> al = z.Places;

            //    List<Place> sel = al.Union(or).Except(al.Intersect(or)).ToList();

            //    ZoneInfoViewModel zoneInfo = new ZoneInfoViewModel(z.Name, count, ordered, sel );
            //    zoneInfos.Add(zoneInfo);
            //}

            //// TODO
            //ViewBag.Play = db.Plays.Find(id);
            //return View(zoneInfos);
            return View(new List<ZoneInfoViewModel>());
        }

        // GET: Plays/Create
        public ActionResult Create()
        {

            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Plays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DatePlay,Info,Name")] Play play)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Plays.Add(play);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(play);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Plays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Play play = db.Plays.Find(id);
                if (play == null)
                {
                    return HttpNotFound();
                }
                return View(play);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Plays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DatePlay,Info,Name")] Play play)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(play).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(play);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Plays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Play play = db.Plays.Find(id);
                if (play == null)
                {
                    return HttpNotFound();
                }
                return View(play);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Plays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                Play play = db.Plays.Find(id);
                db.Plays.Remove(play);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Orders/Create
        /*public ActionResult NewOrder(int ZoneId, DateTime date)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.UserRole, Session))
            {
                //ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }*/

        public ActionResult NewOrder(int? idPlay, int? idPlace)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.UserRole, Session))
            {
                ViewBag.Play = db.Plays.Find(idPlay);
                ViewBag.Place = db.Places.Find(idPlace);
                return View();
            }

            // TODO можно ридеректирь на страницу с успешным заказом
            //return Redirect("~/Plays");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ConfirmOrder(int? idPlay, int? idPlace)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.UserRole, Session))
            {
                Play p = db.Plays.Find(idPlay);
                Place pc = db.Places.Find(idPlace);

                if (p != null && pc != null)
                {
                    Order order = new Order();
                    order.UserId = AuthorizationHelper.CurrentUser(Session).Id;
                    db.Orders.Add(order);


                    //db.SaveChanges();

                    Ticket ticket = new Ticket();
                    ticket.Serial = Guid.NewGuid().ToString();
                    //TODO
                    //ticket.Play = p;
                    ticket.Place = pc;
                    ticket.OrderId = order.Id;
                    db.Tickets.Add(ticket);

                    db.SaveChanges();
                  
    //order.Tickets.Add(ticket

                }
                else
                {
                    RedirectToAction("Index");
                }

                //return View();

                // TODO message about success confirmation
                //if (ModelState.IsValid)
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
