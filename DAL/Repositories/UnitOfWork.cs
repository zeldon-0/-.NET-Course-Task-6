using DAL.Interfaces;
using DAL.EF;
using DAL.Entities;
using System;
namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ShoppingContext _context;
        public UnitOfWork(ShoppingContext context)
        {
            _context = context;
        }

        private ProductRepository productRepository;
        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(_context);
                return productRepository;
            }
        }

        private OrderRepository orderRepository;
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(_context);
                return orderRepository;
            }
        }

        private ProductOrderRepository productOrderRepository;
        public IRepository<ProductOrder> ProductOrders
        {
            get
            {
                if (productOrderRepository == null)
                    productOrderRepository = new ProductOrderRepository(_context);
                return productOrderRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed=true;

        } 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}