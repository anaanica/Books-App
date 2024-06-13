using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Apartments.Controllers
{
    public class BookController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        // GET: Book
        ~BookController()
        {
            db.Dispose();
        }
        public ActionResult Index()
        {
            return View(db.Books);
        }

        // GET: Book/Details/5
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
            var book = db.Books
                .Include(a => a.UploadedFiles)
                .SingleOrDefault(a => a.IdBook == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            //List<string> authorNames = db.Authors.Select(a => a.AuthorName).ToList();
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book book, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                book.UploadedFiles = new List<UploadedFile>();
                AddFiles(book, files);
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            return CommonAction(id);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IEnumerable<HttpPostedFileBase> files)
        {
            var book = db.Books.Find(id);
            if (TryUpdateModel(
                book,
                "",
                new string[]
                {
                    nameof(Book.Title),
                    nameof(Book.Isbn),
                }))
            {
                AddFiles(book, files);

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        private void AddFiles(Book book, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var picture = new UploadedFile
                    {
                        ContentType = file.ContentType,
                        Name = file.FileName
                    };
                    using (var reader = new System.IO.BinaryReader(file.InputStream))
                    {
                        picture.Content = reader.ReadBytes(file.ContentLength);
                    }
                    book.UploadedFiles.Add(picture);
                }
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            return CommonAction(id);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.BookIdBook == id));
            db.Books.Remove(db.Books.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
