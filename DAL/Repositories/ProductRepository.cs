using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;
namespace DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private ShoppingContext _context;
        public ProductRepository(ShoppingContext context)
        {
            _context = context;
        }
        public Product Create(Product p)
        {
            if (p != null)
                _context.Products.Add(p);
            Save();
            return p;
        }

        public Product Delete(int id)
        {
            Product p = _context.Products.Find(id);
            if (p != null)
                _context.Products.Remove(p);
            Save();
            return p;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products
                    .SingleOrDefault(p => p.Id == id);
        }
        public void Update (Product p)
        {
            if (p != null)
            {
                var product = GetById(p.Id);
                if (product != null)
                {
                    _context.Entry(product).CurrentValues.SetValues(p);
                    _context.Entry(product).State = EntityState.Modified;
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