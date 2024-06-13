using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apartments.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        // GET: Author
        ~AuthorController()
        {
            db.Dispose();
        }
        public ActionResult Index()
        {
            return View(db.Authors);
        }

        // GET: Author/Details/5
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
            var author = db.Authors
                .SingleOrDefault(a => a.IdAuthor == id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var author = db.Authors.Find(id);
            if (TryUpdateModel(
                author,
                "",
                new string[]
                {
                    nameof(Author.AuthorName)
                }))
            {

                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            foreach (var book in db.Authors.Find(id).Books.ToList())
            {
                db.Authors.Find(id).Books.Remove(book);
            }
            db.Authors.Remove(db.Authors.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
