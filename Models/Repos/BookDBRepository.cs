
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repos
{
    public class BookDBRepository : IBookstoreRepository<Book>
    {
        BookStoreDbContext db;

        public BookDBRepository(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book GetEntity(int id)
        {
            return db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);

            return db.Books.SingleOrDefault(b => b.Id == id);

        }

        public IList<Book> List()
        {

            return db.Books.Include(a => a.Author).ToList();

            return db.Books.ToList();

        }

        public void Update(int id, Book entity)
        {
            db.Books.Update(entity);
            db.SaveChanges();
        }
    }
}