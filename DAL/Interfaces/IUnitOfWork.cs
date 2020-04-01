using System;
using DAL.Entities;
namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products {get;}
        IRepository<Order> Orders {get;}
        IRepository<ProductOrder> ProductOrders {get;}
    }
}