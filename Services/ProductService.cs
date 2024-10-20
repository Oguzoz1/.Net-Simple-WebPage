using BooksApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Services
{

    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(context);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product? product = await _context
                .Ürünler
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            return product!;
        }
        public async Task Create(Product model, IFormFile imageFile, ModelStateDictionary modelState)
        {
            long maxFileSize = 2 * 1024 * 1024;
            var allowedExtensions
                = new[] { ".jpg", ".png", ".jpeg" };

            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    modelState.AddModelError("", "Geçerli bir resim türü seçiniz.");
                }
                else if (imageFile.Length > maxFileSize)
                {
                    modelState.AddModelError("", "Dosya boyutu en fazla 2MB olabilir!");
                }
                else
                {
                    var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                    try
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        model.Image = randomFileName;
                    }
                    catch
                    {
                        modelState.AddModelError("", "Dosya yüklenirken bir hata oluştu!");
                    }
                }
            }
            else
            {
                modelState.AddModelError("", "Bir resim seçiniz!");
            }
            if (modelState.IsValid)
            {
                await _productRepository.Save(model);
            }
        }

        public async Task Edit(Product model, IFormFile imageFile, ModelStateDictionary modelState)
        {
            var existingProduct = await GetProductByIdAsync(model.ProductId);
            long maxFileSize = 2 * 1024 * 1024;
            var allowedExtensions
               = new[] { ".jpg", ".png", ".jpeg" };

            if (imageFile != null)
            {

                var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    modelState.AddModelError("", "Geçerli bir resim türü seçiniz.");
                }
                else if (imageFile.Length > maxFileSize)
                {
                    modelState.AddModelError("", "Dosya boyutu en fazla 2MB olabilir!");
                }
                else
                {

                    var filePath = Path.Combine("wwwroot/img", imageFile.FileName);
                    try
                    {
                        //Normally, send this to database.
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        existingProduct.Image = imageFile.FileName;
                    }
                    catch
                    {
                        modelState.AddModelError("", "Dosya yüklenirken bir hata oluştu!");
                    }

                }
            }
            existingProduct.Name = model.Name;
            existingProduct.Pages = model.Pages;
            existingProduct.CategoryId = model.CategoryId;
            existingProduct.IsActive = model.IsActive;
            existingProduct.Price = model.Price;
            existingProduct.Description = model.Description;

            _context.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            await _productRepository.DeleteAsync(id!.Value);
        }
    }
}