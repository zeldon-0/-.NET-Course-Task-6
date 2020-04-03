using System.ComponentModel.DataAnnotations;
namespace BLL.Models
{
    public class ProductDTO
    {
        public int Id {get; set;}
        [Required]
        public string Name {get; set;}
    }
}