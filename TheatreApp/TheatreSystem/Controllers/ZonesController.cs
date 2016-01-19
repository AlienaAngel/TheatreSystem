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
    public class ZonesController : Controller
    {
        private Context db = new Context();

        // GET: Zones
        public ActionResult Index()
        {
            AuthorizationHelper.InitializationAuthVar(ViewBag, Session);
            return View(db.Zones.ToList());
            
            
        }

        // GET: Zones/Details/5
        public ActionResult Details(int? id)
        {
            AuthorizationHelper.InitializationAuthVar(ViewBag, Session);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zone zone = db.Zones.Find(id);
            if (zone == null)
            {
                return HttpNotFound();
            }
            return View(zone);


        }

        // GET: Zones/Create
        public ActionResult Create()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Zones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cost,Name,AmountPlaces")] Zone zone)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Zones.Add(zone);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(zone);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Zones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Zone zone = db.Zones.Find(id);
                if (zone == null)
                {
                    return HttpNotFound();
                }
                return View(zone);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cost,Name,AmountPlaces")] Zone zone)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(zone).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(zone);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Zones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Zone zone = db.Zones.Find(id);
                if (zone == null)
                {
                    return HttpNotFound();
                }
                return View(zone);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                Zone zone = db.Zones.Find(id);
                db.Zones.Remove(zone);
                db.SaveChanges();
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
