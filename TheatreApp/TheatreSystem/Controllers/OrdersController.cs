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
    public class OrdersController : Controller
    {
        private Context db = new Context();

        // GET: Orders
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            AuthorizationHelper.InitializationAuthVar(ViewBag, Session);
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
                //var orders = db.Orders.Include(o => o.User).Include(o => o.Tickets.Select(x=>x.Play));

                //if (!String.IsNullOrEmpty(searchString))
                //{
                //    orders = orders.Where(s => s.User.Name.Contains(searchString));
                //}
                //orders = orders.OrderBy(s => s.Id);

                //int pageSize = 10;
                //int pageNumber = (page ?? 1);
                //return View(orders.ToPagedList(pageNumber, pageSize));
                return View();
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
                return View(order);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IsConfirmed,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = db.Orders.Find(id);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.ModeratorRole, Session))
            {
                Order order = db.Orders.Find(id);
                db.Tickets.RemoveRange(order.Tickets);
                db.Orders.Remove(order);
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
