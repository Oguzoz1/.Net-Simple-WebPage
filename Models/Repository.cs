using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Models{

    public class ProductRepository : IRepository<Product>{

        private readonly DataContext _context;
        public ProductRepository(DataContext context) => _context = context;

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Ürünler.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            var product = await _context.Ürünler.FirstOrDefaultAsync(p => p.ProductId == id);
            return product ?? throw new InvalidOperationException("Product Not Found");
        }

        public async Task<Product> Save(Product entity)
        {
            _context.Ürünler.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            Product product = await FindByIdAsync(id);
            _context.Ürünler.Remove(product);

            await _context.SaveChangesAsync();
        }

    }


    public class CategoryRepository : IRepository<Category>
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) => _context = context;

        public async Task<Category> Save(Category entity)
        {
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await FindByIdAsync(id);
            _context.Kategoriler.Remove(category);

            await _context.SaveChangesAsync();
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            var category = await _context.Kategoriler.FirstOrDefaultAsync(c => c.CategoryId == id);
            return category ?? throw new InvalidOperationException("Category Not Found");
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Kategoriler.ToListAsync();
        }
    }

    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> FindByIdAsync(int id);
        Task<T> Save(T entity);
        Task DeleteAsync(int id);
    }
}