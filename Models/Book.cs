namespace Bookstore.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        //image name or image path
        public string ImageUrl { get; set; }


        public Author Author { get; set; }
    }
}