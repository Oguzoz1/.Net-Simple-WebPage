using BooksApp.Models;

namespace BooksApp.Data
{
    public class ProductDetailsViewModel
    {
        public string? Name { get; set; }
        public decimal Pages {get; set;}
        public string? Image {get; set;}
        public Category? Category {get; set;}
        public int? Price {get; set;}
        public string? Description {get; set;}
    }

}