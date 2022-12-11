using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Bookstore.Models.Repos
{
    public class BookRepository :IBookstoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>() 
            { 
                new Book
                {Id = 1, Title ="C# tutorial", Description="easy c# learning"
                },
                new Book
                {Id = 1, Title ="java tutorial", Description="easy j  learning"
                },
                new Book
                {Id = 1, Title ="Python tutorial", Description="easy  p learning"
                },
                new Book
                {Id = 1, Title ="Angular tutorial", Description="easy A learning"
                }

            };
        }

        public void Add(Book entity)
        {
            books.Add(entity);
        }

        public void delete(int id)
        {
            var book = GetEntity(id);
            books.Remove(book);
        }

        public Book GetEntity(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id, Book entity)
        {
            var book = GetEntity(id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Autho = entity.Autho;
 

        }
    }
}