using BLL.Models;
using System.Collections.Generic;
using System;
namespace BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
        OrderDTO Create(OrderDTO order);
        OrderDTO Delete(int id);
        void Update(OrderDTO order);
        OrderDTO GetById(int id);
        IEnumerable<OrderDTO> GetAll();
        OrderDTO AddToOrder(int orderId, ProductDTO product);
        
    }
}