using Bookstore.Models;
using Bookstore.Models.Repos;
using Bookstore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                string filename = UploadFile(model.File) ?? string.Empty;
                
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
                        Author = author,
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
            //0994135019
          
            var book = bookstoreRepository.GetEntity(id);
            var authid = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
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
<<<<<<< HEAD
                string filename = UploadFile(model.File,model.UrlImage);
                
=======
                string filename = string.Empty;
                if(model.File != null)
                {
                    string rootpath = Path.Combine(hostingEnvironment.WebRootPath,"Uploads");
                    filename = model.File.FileName;
                    string fullpath = Path.Combine(rootpath,filename);
                    //delete old file 
                    string oldfilepath = bookstoreRepository.GetEntity(model.BookId).ImageUrl;
                    string fulloldpath = Path.Combine(rootpath,oldfilepath);
                    if (fulloldpath != fullpath)
                    {
                        System.IO.File.Delete(fulloldpath);

                        // save the new file
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                }
>>>>>>> 5d1c906842388b669b23cd2408486d226166797e
                var author = authorRepository.GetEntity(model.AuthorId);
                var book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
<<<<<<< HEAD
                    Author = author,
=======
                    Autho = author,
>>>>>>> 5d1c906842388b669b23cd2408486d226166797e
                    ImageUrl=filename
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

        string UploadFile(IFormFile File)
        {
            if (File != null)
            {
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                string fullPath = Path.Combine(uploads, File.FileName);
                File.CopyTo(new FileStream(fullPath, FileMode.Create));
                return File.FileName;
            }
            return null;
            
        }

        string UploadFile(IFormFile File, string imageUrl)
        {
            if (File != null)
            {
                string rootpath = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                string newpath = Path.Combine(rootpath, File.FileName);
                string oldpath = Path.Combine(rootpath, imageUrl);
                if (newpath != oldpath)
                {
                    System.IO.File.Delete(oldpath);

                    // save the new file
                    File.CopyTo(new FileStream(newpath, FileMode.Create));
                    
                }
                return File.FileName;
            }
            return imageUrl;
        }
    }
}