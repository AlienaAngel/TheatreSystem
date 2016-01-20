using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Filters;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    [Auth]
    public class TicketsController : Controller
    {
        private Context db = new Context();

        // GET: Tickets
        public ActionResult Index( string currentFilter, string searchString, int? page)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                //TODO
                //var tickets = db.Tickets.Include(t => t.Order).Include(t => t.Play).Include(t => t.Place.Zone);

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    tickets = tickets.Where(s => s.Place.Zone.Name.Contains(searchString));
                //}

                //tickets = tickets.OrderBy(s => s.Place.Zone.Name);


                //int pageSize = 10;
                //int pageNumber = (page ?? 1);
                //return View(tickets.ToPagedList(pageNumber, pageSize));
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Ticket ticket = db.Tickets.Find(id);
                if (ticket == null)
                {
                    return HttpNotFound();
                }
                return View(ticket);
            }
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Tickets/Create
        //public ActionResult Create()
        //{
        //    if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
        //    {
        //        ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id");
        //        ViewBag.PlayId = new SelectList(db.Plays, "Id", "Info");
        //        ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name");
        //        return View();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Serial,PlayId,ZoneId,OrderId")] Ticket ticket)
        //{
        //    if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Tickets.Add(ticket);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
        //        ViewBag.PlayId = new SelectList(db.Plays, "Id", "Info", ticket.PlayId);
        //        ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", ticket.Place.ZoneId);
        //        return View(ticket);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Ticket ticket = db.Tickets.Find(id);
                    if (ticket == null)
                    {
                        return HttpNotFound();
                    }
                    ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
                    //TODO
                    //ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name", ticket.PlayId);
                    ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", ticket.Place.ZoneId);
                    return View(ticket);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Serial,PlayId,ZoneId,OrderId")] Ticket ticket)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(ticket).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
                    //TODO
                    //ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name", ticket.PlayId);
                    ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", ticket.Place.ZoneId);
                    return View(ticket);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Ticket ticket = db.Tickets.Find(id);
                    if (ticket == null)
                    {
                        return HttpNotFound();
                    }
                    return View(ticket);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
                {
                    Ticket ticket = db.Tickets.Find(id);
                    db.Tickets.Remove(ticket);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("~/");
            }
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
