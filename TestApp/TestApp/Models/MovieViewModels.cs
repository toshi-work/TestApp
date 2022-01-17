using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models
{
    // 型の定義を実施している
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        // decimal({整数値の桁数}, {小数点の桁数})
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
