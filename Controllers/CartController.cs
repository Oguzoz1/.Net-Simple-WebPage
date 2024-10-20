using Microsoft.AspNetCore.Mvc;
using BooksApp.Models;
using BooksApp.Data;
using System.Text.Json;

namespace BooksApp.Controllers
{
    public class CartController : BaseController
    {
        public CartController(DataContext context) : base(context) { }


        public IActionResult Index(string searchString, string category)
        {
            var cart = GetCartFromCookie() ?? new CartViewModel();
            return View(cart);
        }

        private void SetCartToCookie(CartViewModel cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Response.Cookies.Append("Cart", json, new CookieOptions { HttpOnly = true });
        }

        private CartViewModel GetCartFromCookie()
        {
            if (HttpContext.Request.Cookies.TryGetValue("Cart", out var json))
            {
                return JsonSerializer.Deserialize<CartViewModel>(json)!;
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var cart = GetCartFromCookie() ?? new CartViewModel();
            var product = await _productService.GetProductByIdAsync(productId);

            if (product != null)
            {
                if (cart.Products == null)
                {
                    cart.Products = new List<Product>();
                }

                var existingProduct = cart.Products.FirstOrDefault(p => p.ProductId == productId);
                cart.Products.Add(product);

                SetCartToCookie(cart);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromCookie() ?? new CartViewModel();

            if (cart.Products != null)
            {
                
                var productToRemove = cart.Products.FirstOrDefault(p => p.ProductId == productId);
                if (productToRemove != null)
                {
                    cart.Products.Remove(productToRemove); 
                }
            }

            SetCartToCookie(cart); 

            return RedirectToAction("Index"); 
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId, int quantity)
        {
            var cart = GetCartFromCookie() ?? new CartViewModel();
            var product = await _productService.GetProductByIdAsync(productId);
            cart.Products?.RemoveAll(p => p.ProductId == productId);

            if (cart.Products != null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    cart.Products.Add(product);
                }
            }

            SetCartToCookie(cart); 

            return RedirectToAction("Index","Cart"); 
        }
    }
}
