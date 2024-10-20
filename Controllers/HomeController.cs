using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using BooksApp.Services;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using BooksApp.Data;

namespace BooksApp.Controllers;

public class HomeController : BaseController
{
    public HomeController(DataContext context) : base(context) { }

    public async Task<IActionResult> Index(string searchString, string category)
    {
        var model = await GetProductViewModelAsync(searchString, category);
        ViewData["ProductViewModel"] = model;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id, string searchString, string category)
    {
        var model = await GetProductViewModelAsync(searchString, category);
        ViewData["ProductViewModel"] = model;

        if (id == null)
        {
            return NotFound("Null product ID.");
        }

        try
        {
            Product product = await _productService.GetProductByIdAsync(id.Value);
            var detailsModel = new ProductDetailsViewModel
            {
                Name = product.Name,
                Pages = product.Pages,
                Image = product.Image,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };

            return View("~/Views/Home/Details.cshtml", detailsModel);
        }
        catch
        {
            return NotFound();
        }
    }
}

