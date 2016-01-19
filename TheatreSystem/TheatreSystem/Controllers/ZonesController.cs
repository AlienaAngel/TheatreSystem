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
    public class ZonesController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Zones/

        public ActionResult Index()
        {
            return View(db.Zones.ToList());
        }

        //
        // GET: /Zones/Details/5

        public ActionResult Details(int id = 0)
        {
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        //
        // GET: /Zones/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Zones/Create

        [HttpPost]
        public ActionResult Create(Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Zones.Add(zone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(zone);
        }

        //
        // GET: /Zones/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        //
        // POST: /Zones/Edit/5

        [HttpPost]
        public ActionResult Edit(Zone zone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(zone);
        }

        //
        // GET: /Zones/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);
        }

        //
        // POST: /Zones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Zone zone = db.Zones.Find(id);
            db.Zones.Remove(zone);
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