using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartments.Controllers
{
    public class GenreController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        // GET: Genre
        ~GenreController()
        {
            db.Dispose();
        }
        public ActionResult Index()
        {
            return View(db.Genres);
        }

        // GET: Genre/Details/5
        public ActionResult Details(int? id)
        {
            return CommonAction(id);
        }

        private ActionResult CommonAction(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var genre = db.Genres
                .SingleOrDefault(a => a.IdGenre == id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var genre = db.Genres.Find(id);
            if (TryUpdateModel(
                genre,
                "",
                new string[]
                {
                    nameof(Genre.GenreName)
                }))
            {

                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }

        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            foreach (var book in db.Genres.Find(id).Books.ToList())
            {
                db.Genres.Find(id).Books.Remove(book);
            }
            db.Genres.Remove(db.Genres.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
