using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repos
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        List<Author> authors;

        public AuthorRepository()
        {
            authors = new List<Author>()
            {
                new Author
                {   
                    Id = 1, Fullname = "Shekespeer Toin"
                },
                new Author
                {
                    Id = 2, Fullname = "Khaled Tawfik"
                },
                new Author
                {
                    Id = 3, Fullname = "Amal Daokool"
                },


            };

        }
        public void Add(Author entity)
        {
            entity.Id = authors.Max(a => a.Id) + 1;
            authors.Add(entity);
        }

        public void delete(int id)
        {
            var author = GetEntity(id);
            authors.Remove(author);
        }

        public Author GetEntity(int id)
        {
            var author = authors.SingleOrDefault(a => a.Id == id);
            return author;
        }

        public IList<Author> List()
        {
            return authors;
        }

        public void Update(int id, Author entity)
        {
            var author = GetEntity(id);
            author.Fullname = entity.Fullname;
        }
    }
}