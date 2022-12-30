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
                {Id = 1,
                   Title ="C# tutorial",
                    Description="easy c# learning",
                    Author= new Author {Id = 2},
                    ImageUrl = "data.png"
                },
                new Book
                {Id = 2, Title ="java tutorial",
                    Description="easy j  learning" ,Author = new Author{},
                    ImageUrl = "data.png"
                },
                new Book
                {Id = 3, Title ="Python tutorial",
                    Description="easy  p learning" ,Author = new Author{},
                    ImageUrl = "data.png"
                },
                new Book
                {Id = 4, Title ="Angular tutorial",
                    Description="easy A learning" ,Author = new Author{},
                    ImageUrl = "data.png"
                }

            };
        }

        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
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
            book.Author = entity.Author;
            book.ImageUrl = entity.ImageUrl;
 

        }
    }
}