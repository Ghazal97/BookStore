using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Models.Repos
{
    public class AuthorDBRepository : IBookstoreRepository<Author>
    {
        BookStoreDbContext db;

        public AuthorDBRepository(BookStoreDbContext _db)
        {
           db = _db;
        }
        public void Add(Author entity)
        {
           
            db.Authors.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var author = GetEntity(id);
            db.Authors.Remove(author);
            db.SaveChanges();
        }

        public Author GetEntity(int id)
        {
            var author = db.Authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int id, Author entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

    }
}