using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheatreSystem.Models;

namespace TheatreSystem.Controllers
{
    public class TicketsController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Tickets/

        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Date).Include(t => t.Place).Include(t => t.Order);
            return View(tickets.ToList());
        }

        //
        // GET: /Tickets/Details/5

        public ActionResult Details(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // GET: /Tickets/Create

        public ActionResult Create()
        {
            ViewBag.DateId = new SelectList(db.Dates, "Id", "Id");
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Id");
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id");
            return View();
        }

        //
        // POST: /Tickets/Create

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DateId = new SelectList(db.Dates, "Id", "Id", ticket.DateId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Id", ticket.PlaceId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
            return View(ticket);
        }

        //
        // GET: /Tickets/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.DateId = new SelectList(db.Dates, "Id", "Id", ticket.DateId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Id", ticket.PlaceId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
            return View(ticket);
        }

        //
        // POST: /Tickets/Edit/5

        [HttpPost]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DateId = new SelectList(db.Dates, "Id", "Id", ticket.DateId);
            ViewBag.PlaceId = new SelectList(db.Places, "Id", "Id", ticket.PlaceId);
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Id", ticket.OrderId);
            return View(ticket);
        }

        //
        // GET: /Tickets/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Tickets/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}