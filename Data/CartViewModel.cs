using BooksApp.Models;
using System.Linq;

namespace BooksApp.Data
{
    public class CartViewModel
    {
        public List<Product>? Products {get; set;}
        public int? TotalPrice {
            get
             {
                var total = 0;
                foreach(var product in Products!){
                    total += product.Price!.Value;
                }
                return total;
             }
        }

        public int CountProductById(int productId)
        {
            return Products?.Count(p => p.ProductId == productId) ?? 0;
        }
    }
}