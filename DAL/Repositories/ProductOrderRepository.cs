using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;
namespace DAL.Repositories
{
    public class ProductOrderRepository : IRepository<ProductOrder>
    {
        private ShoppingContext _context;
        public ProductOrderRepository(ShoppingContext context)
        {
            _context = context;
        }
        public ProductOrder Create(ProductOrder po)
        {
            if (po != null)
               _context.ProductOrders.Add(po);
            Save();
            return po;
        }

        public ProductOrder Delete(int id)
        {
            ProductOrder po = _context.ProductOrders.Find(id);
            if (po != null)
                _context.ProductOrders.Remove(po);
            Save();
            return po;
        }

        public IEnumerable<ProductOrder> GetAll()
        {
            return _context.ProductOrders.ToList();
        }

        public ProductOrder GetById(int id)
        {
            return _context.ProductOrders
                    .SingleOrDefault(po => po.Id == id);
        }
        public void Update (ProductOrder po)
        {
            if (po != null)
            {
                var productOrder = GetById(po.Id);
                if (productOrder != null)
                {
                    _context.Entry(productOrder).CurrentValues.SetValues(po);
                    _context.Entry(productOrder).State = EntityState.Modified;
                }
            }
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}