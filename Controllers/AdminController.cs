using BooksApp.Data;
using BooksApp.Models;
using BooksApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Controllers;

[Route("Home/[controller]/[action]")]
public class AdminController : BaseController
{
    public AdminController(DataContext context) : base(context) {}

    public async Task<IActionResult> Index(string searchString, string category)
    {
        var model = await GetProductViewModelAsync(searchString, category);
        ViewData["ProductViewModel"] = model;
        return View("~/Views/Home/Admin/Index.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product model, IFormFile imageFile)
    {
        await _productService.Create(model, imageFile, ModelState);
        ViewBag.Categories = new SelectList(_context.Kategoriler, "CategoryId", "Name");
        return View("~/Views/Home/Admin/Create.cshtml", model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Kategoriler, "CategoryId", "Name");

        return View("~/Views/Home/Admin/Create.cshtml");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Product entity = await _productService.GetProductByIdAsync(id.Value);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(_context.Kategoriler, "CategoryId", "Name");
        return View("~/Views/Home/Admin/Edit.cshtml", entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product model, IFormFile? imageFile)
    {
        if (id != model.ProductId)
        {
            return NotFound();
        }

        await _productService.Edit(model, imageFile!, ModelState);

        ViewBag.Categories = new SelectList(_context.Kategoriler, "CategoryId", "Name");
        return RedirectToAction("index");
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _productService.Delete(id);

        return RedirectToAction("index");
    }

}