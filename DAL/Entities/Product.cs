using System.Collections.Generic;
namespace DAL.Entities
{
    public class Product
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public ICollection<ProductOrder> ProductOrders {get; set;}
    } 
}