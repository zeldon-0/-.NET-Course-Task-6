using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using BLL.Mappers;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
namespace BLL.Services
{
    public class OrderService:IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public OrderService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public OrderDTO Create(OrderDTO order)
        {
            var orderEntity = _uow.Orders.Create(_mapper.Map<Order>(order));
            return _mapper.Map<OrderDTO>(orderEntity);
        }

        public OrderDTO Delete(int id)
        {
            var orderEntity = _uow.Orders.Delete(id);
            return _mapper.Map<OrderDTO>(orderEntity);
        }


        public IEnumerable<OrderDTO> GetAll()
        {
            return _uow.Orders.GetAll()
                    .Select(order => _mapper.Map<OrderDTO>(order));
        }

        public OrderDTO GetById(int id)
        {

            return _mapper.Map<OrderDTO>
                    (_uow.Orders.GetById(id));
        }

        public void Update(OrderDTO order)
        {
            _uow.Orders.Update(_mapper.Map<Order>(order));
        }

        public OrderDTO AddToOrder(int orderId, ProductDTO product)
        {
            var orderProduct = _uow.ProductOrders
                .GetAll().Where(po => po.OrderId == orderId 
                && po.ProductId == product.Id).SingleOrDefault();
            if (orderProduct == null)
            {
                _uow.ProductOrders.Create(
                new ProductOrder()
                {
                    ProductId = product.Id,
                    OrderId = orderId
                }
            );

            }
            return GetById(orderId);

        }

        private bool disposed=false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _uow.Dispose();
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
        