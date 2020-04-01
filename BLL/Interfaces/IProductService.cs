using BLL.Models;
using System.Collections.Generic;
using System;
namespace BLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        ProductDTO Create(ProductDTO prod);
        ProductDTO Delete(int id);
        void Update(ProductDTO prod);
        ProductDTO GetById(int id);
        IEnumerable<ProductDTO> GetAll();
        IEnumerable<ProductDTO> GetByOrder(int orderId);
        ProductDTO CreateForOrder(int orderId, ProductDTO product);

        
    }
}