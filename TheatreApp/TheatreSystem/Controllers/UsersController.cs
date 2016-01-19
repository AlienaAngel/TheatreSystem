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
    public class UsersController : Controller
    {
        private Context db = new Context();

        // GET: Users
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
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

                var users = db.Users.Select(x => x);
                if (!String.IsNullOrEmpty(searchString))
                {
                    users = users.Where(s => s.Name.Contains(searchString)
                                           || s.Email.Contains(searchString));
                }

                users = users.OrderBy(s => s.Name);


                int pageSize = 50;
                int pageNumber = (page ?? 1);
                return View(users.ToPagedList(pageNumber, pageSize));




                return View(db.Users.ToList());
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Users/Create
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

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Password")] User user)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(user);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,Password")] User user, [Bind(Include = "Role")] int? role)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (ModelState.IsValid)
                {
                    user.Roles.Clear();

                    List<Role> roles = db.Roles.ToList();

                    //db.SaveChanges();

                    // TODO don't do it like that. never. NEVER. Did you understand me? Ok. I keep calm.
                    switch (role)
                    {
                        case 3:
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.AdminRole)).FirstOrDefault());
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.ModeratorRole)).FirstOrDefault());
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.UserRole)).FirstOrDefault());
                            break;
                        case 2:
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.ModeratorRole)).FirstOrDefault());
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.UserRole)).FirstOrDefault());
                            break;
                        default:
                            user.Roles.Add(roles.Where(x => x.Name.Equals(AuthorizationHelper.UserRole)).FirstOrDefault());
                            break;
                    }

                    //db.Entry(user).State = EntityState.Modified;
                    //User 
                    User userEntity = db.Users.Where(x => x.Id == user.Id).FirstOrDefault();
                    if(userEntity!=null)
                    {
                        userEntity.Name = user.Name;
                        userEntity.Password = user.Password;
                        userEntity.Email = user.Email;
                        userEntity.Roles.Clear();
                        userEntity.Roles.AddRange(user.Roles);
                        db.SaveChanges();
                    } 

                    
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Plays");
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (AuthorizationHelper.IsAccess(AuthorizationHelper.AdminRole, Session))
            {
                User user = db.Users.Find(id);
                db.Users.Remove(user);
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
