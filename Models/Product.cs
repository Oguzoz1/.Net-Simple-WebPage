using System.ComponentModel.DataAnnotations;

namespace BooksApp.Models
{

    public class Product
    {

        [Display(Name = "Ürün Id")]
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage = "İsim Gerekli Alan")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Display(Name = "Sayfa Sayısı")]
        [Required(ErrorMessage = "Sayfalar Gerekli Alan")]
        [Range(0, 5000)]
        public decimal Pages { get; set; }

        [Display(Name = "Görsel")]
        public string? Image { get; set; }
        public bool IsActive { get; set; }

        [Display(Name = "Kategori")]
        [Required(ErrorMessage = "Kategori Gerekli Alan")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat Gerekli Alan")]
        public int? Price { get; set; }

        [Display(Name = "Ürün Açıklaması")]
        [Required(ErrorMessage = "Ürün Açıklaması Gerekli Alan")]
        [StringLength(2000, ErrorMessage = "Ürün Açıklaması en fazla 2000 karakter olabilir.")]
        public string? Description { get; set; }

    }
}