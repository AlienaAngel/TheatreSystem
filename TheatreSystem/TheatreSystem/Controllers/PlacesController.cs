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
    public class PlacesController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Places/

        public ActionResult Index()
        {
            var places = db.Places.Include(p => p.Zone);
            return View(places.ToList());
        }

        //
        // GET: /Places/Details/5

        public ActionResult Details(int id = 0)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        //
        // GET: /Places/Create

        public ActionResult Create()
        {
            ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name");
            return View();
        }

        //
        // POST: /Places/Create

        [HttpPost]
        public ActionResult Create(Place place)
        {
            if (ModelState.IsValid)
            {
                db.Places.Add(place);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", place.ZoneId);
            return View(place);
        }

        //
        // GET: /Places/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", place.ZoneId);
            return View(place);
        }

        //
        // POST: /Places/Edit/5

        [HttpPost]
        public ActionResult Edit(Place place)
        {
            if (ModelState.IsValid)
            {
                db.Entry(place).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", place.ZoneId);
            return View(place);
        }

        //
        // GET: /Places/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Place place = db.Places.Find(id);
            if (place == null)
            {
                return HttpNotFound();
            }
            return View(place);
        }

        //
        // POST: /Places/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Place place = db.Places.Find(id);
            db.Places.Remove(place);
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