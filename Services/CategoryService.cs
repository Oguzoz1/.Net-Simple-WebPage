using BooksApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BooksApp.Services{

    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepositry;
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
            _categoryRepositry = new CategoryRepository(context);    
        }

        public async Task<Category> GetCategoryByIdAsync(int id){
            return await _categoryRepositry.FindByIdAsync(id);
        }

    }
}