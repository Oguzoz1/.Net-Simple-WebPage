using Microsoft.EntityFrameworkCore;

namespace BooksApp.Models{

    public class DataContext : DbContext{

        public DataContext(DbContextOptions<DataContext> options) : base (options){}

        public DbSet<Product> Ürünler => Set<Product>();
        public DbSet<Category> Kategoriler => Set<Category>();
    }
}