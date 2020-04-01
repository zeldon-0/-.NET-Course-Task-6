using System;
using System.Collections.Generic;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using BLL.Mappers;
using DAL.Entities;
using System.Linq;
using AutoMapper;
namespace BLL.Services
{
    public class ProductService : IProductService
    { 
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public ProductService (IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public ProductDTO Create(ProductDTO prod)
        {
            var product = _uow.Products.Create(_mapper.Map<Product>(prod));
            return _mapper.Map<ProductDTO>(product);
        }

        public ProductDTO Delete(int id)
        {
            var prodEntity = _uow.Products.Delete(id);
            return _mapper.Map<ProductDTO>(prodEntity);

        }
        public void Update(ProductDTO prod)
        {
            _uow.Products.Update(_mapper.Map<Product>(prod));

        }

        public IEnumerable<ProductDTO> GetAll()
        {
            return _uow.Products.GetAll()
                    .Select(product => _mapper.Map<ProductDTO>(product));
        }


        public ProductDTO GetById(int id)
        {
            return _mapper.Map<ProductDTO>
                    (_uow.Products.GetById(id));
        }

        public IEnumerable<ProductDTO> GetByOrder(int orderId)
        {

            OrderDTO order = _mapper.Map<OrderDTO>
                    (_uow.Orders.GetById(orderId));
            
            return order.Products;
        }
        public ProductDTO CreateForOrder(int orderId, ProductDTO product)
        {
            ProductDTO prod = GetById(product.Id); 
            var newProd = Create(product);
            _uow.ProductOrders.Create(
                new ProductOrder()
                {
                    ProductId = newProd.Id,
                    OrderId = orderId
                }
            );

            return newProd;
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