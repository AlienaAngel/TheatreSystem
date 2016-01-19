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
    public class RolesController : Controller
    {
        private Context db = new Context();

        // GET: Roles
        public ActionResult Index()
        {
            AuthorizationHelper.InitializationAuthVar(ViewBag, Session);
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                return View(db.Roles.ToList());
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Roles.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
            {
                return Redirect("~/Plays");
            }

        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                return View();
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Role role)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Roles.Add(role);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(role);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Roles.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Role role)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Entry(role).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Role role = db.Roles.Find(id);
                if (role == null)
                {
                    return HttpNotFound();
                }
                return View(role);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("~/Plays");
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
