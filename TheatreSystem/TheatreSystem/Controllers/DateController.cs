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
    public class DateController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Date/

        public ActionResult Index()
        {
            var dates = db.Dates.Include(d => d.Play);
            return View(dates.ToList());
        }

        //
        // GET: /Date/Details/5

        public ActionResult Details(int id = 0)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        //
        // GET: /Date/Create

        public ActionResult Create()
        {
            ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name");
            return View();
        }

        //
        // POST: /Date/Create

        [HttpPost]
        public ActionResult Create(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Dates.Add(date);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name", date.PlayId);
            return View(date);
        }

        //
        // GET: /Date/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name", date.PlayId);
            return View(date);
        }

        //
        // POST: /Date/Edit/5

        [HttpPost]
        public ActionResult Edit(Date date)
        {
            if (ModelState.IsValid)
            {
                db.Entry(date).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayId = new SelectList(db.Plays, "Id", "Name", date.PlayId);
            return View(date);
        }

        //
        // GET: /Date/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Date date = db.Dates.Find(id);
            if (date == null)
            {
                return HttpNotFound();
            }
            return View(date);
        }

        //
        // POST: /Date/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Date date = db.Dates.Find(id);
            db.Dates.Remove(date);
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