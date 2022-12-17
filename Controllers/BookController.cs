using Bookstore.Models;
using Bookstore.Models.Repos;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookstoreRepository;

        private readonly IBookstoreRepository<Author> authorRepository;

        private readonly IHostingEnvironment hostingEnvironment;

        public BookController(IBookstoreRepository<Book> bookstoreRepository,
                              IBookstoreRepository<Author> authorRepository,
                              IHostingEnvironment hostingEnvironment)
        {
            this.bookstoreRepository = bookstoreRepository;
            this.authorRepository = authorRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: BookController
        public ActionResult Index()
        {
            var books = bookstoreRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookstoreRepository.GetEntity(id);

            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            
            return View(getViewModel());
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                string filename = string.Empty;
                if (model.File != null)
                {
                    string pathroot = Path.Combine(hostingEnvironment.WebRootPath,"Uploads");
                    filename = model.File.FileName;
                    string fullPath = Path.Combine(pathroot, filename);
                    model.File.CopyTo(new FileStream(fullPath,FileMode.Create));
                }
                try
                {
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "Please select author of your book... ";

                        return View(getViewModel());
                    }
                    var author = authorRepository.GetEntity(model.AuthorId);
                    var book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Autho = author,
                        ImageUrl = filename
                        
                    };
                    bookstoreRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
            ModelState.AddModelError("","you have to enter full fields required");
            return View(getViewModel());
            
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
          
            var book = bookstoreRepository.GetEntity(id);
            var authid = book.Autho == null ? book.Autho.Id = 0 : book.Autho.Id;
            var mode = new BookAuthorViewModel()
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId=  authid,
                Authors = authorRepository.List().ToList(),
                UrlImage= book.ImageUrl
                
            
            };
            return View(mode);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookAuthorViewModel model)
        {
            try
            {
                var author = authorRepository.GetEntity(model.AuthorId);
                var book = new Book
                {
                    Title = model.Title,
                    Description = model.Description,
                    Autho = author
                };
                bookstoreRepository.Update(model.BookId,book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookstoreRepository.GetEntity(id);
            
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book model)
        {
            try
            {

                bookstoreRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> fillselectlist()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0,new Author {Id = -1, Fullname = "....Please select an author" });
            return authors;

        }
        BookAuthorViewModel getViewModel()
        {
            var model = new BookAuthorViewModel
            {

                Authors = fillselectlist()

            };
            return model;
        }
    }
}