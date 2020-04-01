using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Entities;
using DAL.EF;
using System.Linq;
namespace DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private ShoppingContext _context;
        public OrderRepository(ShoppingContext context)
        {
            _context = context;
        }
        public Order Create(Order o)
        {
            if (o != null)
               _context.Orders.Add(o);
            Save();
            return o;
        }

        public Order Delete(int id)
        {
            Order o = _context.Orders.Find(id);
            if (o != null)
                _context.Orders.Remove(o);
            Save();
            return o;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            _context.ProductOrders.Load();
            _context.Products.Load();
            return _context.Orders
                    .Include(o => o.ProductOrders)
                    .SingleOrDefault(o => o.Id == id);
        }
        public void Update (Order o)
        {
            if (o != null)
            {
                var order = GetById(o.Id);
                if (order != null)
                {
                    _context.Entry(order).CurrentValues.SetValues(o);
                    _context.Entry(order).State = EntityState.Modified;
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