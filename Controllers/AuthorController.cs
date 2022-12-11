using Bookstore.Models;
using Bookstore.Models.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookstoreRepository<Author> authorRepository;

        public AuthorController(IBookstoreRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }
        // GET: AuthorController
        public ActionResult Index()
        {
            var authors = authorRepository.List();
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public ActionResult Details(int id)
        {
            var author = authorRepository.GetEntity(id);
            return View(author);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorRepository.Add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = authorRepository.GetEntity(id);
           
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                authorRepository.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = authorRepository.GetEntity(id);
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}