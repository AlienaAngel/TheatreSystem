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
    public class PlayController : Controller
    {
        private Context db = new Context();

        //
        // GET: /Play/

        public ActionResult Index()
        {
            var plays = db.Plays.Include(p => p.Author).Include(p => p.Genre);
            return View(plays.ToList());
        }

        //
        // GET: /Play/Details/5

        public ActionResult Details(int id = 0)
        {
            Play play = db.Plays.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        //
        // GET: /Play/Create

        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        //
        // POST: /Play/Create

        [HttpPost]
        public ActionResult Create(Play play)
        {
            if (ModelState.IsValid)
            {
                db.Plays.Add(play);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", play.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", play.GenreId);
            return View(play);
        }

        //
        // GET: /Play/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Play play = db.Plays.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", play.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", play.GenreId);
            return View(play);
        }

        //
        // POST: /Play/Edit/5

        [HttpPost]
        public ActionResult Edit(Play play)
        {
            if (ModelState.IsValid)
            {
                db.Entry(play).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "Name", play.AuthorId);
            ViewBag.GenreId = new SelectList(db.Genres, "Id", "Name", play.GenreId);
            return View(play);
        }

        //
        // GET: /Play/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Play play = db.Plays.Find(id);
            if (play == null)
            {
                return HttpNotFound();
            }
            return View(play);
        }

        //
        // POST: /Play/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Play play = db.Plays.Find(id);
            db.Plays.Remove(play);
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