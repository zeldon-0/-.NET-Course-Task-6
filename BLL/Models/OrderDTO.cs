using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace BLL.Models
{
    public class OrderDTO
    {
        public int Id {get; set;}
        [Required]
        public string Summary {get; set;}
        public IEnumerable<ProductDTO> Products {get; set;}
    }
}