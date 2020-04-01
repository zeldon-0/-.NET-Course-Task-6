using System.Collections.Generic;
namespace DAL.Entities
{
    public class Order
    {
        public int Id {get; set;}
        public string Summary {get; set;}
        public ICollection<ProductOrder> ProductOrders {get; set;}
    } 
}