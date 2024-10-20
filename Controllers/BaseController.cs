using BooksApp.Data;
using BooksApp.Models;
using BooksApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class BaseController : Controller
{
    protected readonly ProductService _productService;
    protected readonly DataContext _context;

    public BaseController(DataContext context)
    {
        _context = context;
        _productService = new ProductService(_context);
    }
    protected async Task<ProductViewModel> GetProductViewModelAsync(string searchString, string category)
    {
        List<Product> products = await _productService.GetProductsAsync();

        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name!.ToLower().Contains(searchString.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        return new ProductViewModel
        {
            Products = products,
            Categories = await _context.Kategoriler.ToListAsync(),
            SelectedCategory = category
        };
    }
        protected ProductViewModel GetProductViewModel(string searchString, string category)
    {
        List<Product> products = _context.Ürünler.ToList();

        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name!.ToLower().Contains(searchString.ToLower())).ToList();
        }

        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
        }

        return new ProductViewModel
        {
            Products = products,
            Categories = _context.Kategoriler.ToList(),
            SelectedCategory = category
        };
    }
}
