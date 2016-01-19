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
    public class PlacesController : Controller
    {
        private Context db = new Context();

        // GET: Places
        public ActionResult Index()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                var places = db.Places.Include(p => p.Zone);
                return View(places.ToList());
            }
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Places/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Place place = db.Places.Find(id);
                if (place == null)
                {
                    return HttpNotFound();
                }
                return View(place);
            }
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Places/Create
        public ActionResult Create()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name");
                return View();
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: Places/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Number,ZoneId")] Place place)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
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
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Places/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Place place = db.Places.Find(id);
                if (place == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ZoneId = new SelectList(db.Zones, "Id", "Name", place.ZoneId);
                return View(place);
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Number,ZoneId")] Place place)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
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
            else
            {
                return Redirect("~/");
            }
        }

        // GET: Places/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Place place = db.Places.Find(id);
                if (place == null)
                {
                    return HttpNotFound();
                }
                return View(place);
            }
            else
            {
                return Redirect("~/");
            }
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                Place place = db.Places.Find(id);
                db.Places.Remove(place);
                db.SaveChanges();
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
