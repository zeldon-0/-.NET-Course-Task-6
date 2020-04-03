using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.EF;
using DAL.Entities;
using BLL.Interfaces;
using BLL.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IProductService _productService;
        private IOrderService _orderService;

        public OrderController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }


        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> GetOrders()
        {
            var result =  _orderService.GetAll();
            return  Ok(result);
        }


        [HttpGet("{id}")]
        public ActionResult<OrderDTO> GetOrder(int id)
        {
            var order =  _orderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        [HttpPut("{id}")]
        public IActionResult PutOrder(OrderDTO order)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var foundOrder = _orderService.GetById(order.Id);
            if(foundOrder == null)
            {
                return NotFound();
            }
            _orderService.Update(order);

            return NoContent();
        }


        [HttpPost]
        public ActionResult<OrderDTO> PostOrder(OrderDTO order)
        {
            if (!ModelState.IsValid || order.Id != 0)
            {
                return BadRequest();
            }
            var newOrder =  _orderService.Create(order);

            return CreatedAtAction("GetOrder", new { id = newOrder.Id }, newOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult<OrderDTO> DeleteOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
            {
                return NotFound();
            }

            _orderService.Delete(id);            

            return NoContent();
        }

        [HttpGet]
        [Route("{orderId}/Products/")]
        public ActionResult<IEnumerable<ProductDTO>> GetOrderProducts (int orderId)
        {
            var order = _orderService.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            var products = _productService.GetByOrder(orderId);
            if (!products.Any())
            {
                return NotFound();
            }

            return Ok(products);
        }
        [HttpGet]
        [Route("{orderId}/Products/{productId}")]
        public ActionResult<IEnumerable<ProductDTO>> GetProductByOrder (int orderId, int productId)
        {
            
            var order = _orderService.GetById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            var product = _productService.GetByOrder(orderId)
                .Where(prod => prod.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }

        [HttpPut]
        [Route("{orderId}/Products/")]
        public IActionResult PostProductByOrder(int orderId, ProductDTO product)
        {
            OrderDTO order = _orderService.GetById(orderId);
            if (order == null)
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            
            ProductDTO prod = _productService.GetById(product.Id);
            if (prod == null)
            {
                if (product.Id != 0)
                    return BadRequest();
        
                ProductDTO newProd = _productService.CreateForOrder(orderId, product);
                return CreatedAtAction("GetProduct", new {Controller = "Product", Id = newProd.Id}, newProd);
            }
            else
            {
                if (product.Name != prod.Name)
                    return BadRequest();
                OrderDTO newOrder = _orderService.AddToOrder(orderId, product);
                newOrder.Products = _productService.GetByOrder(orderId);
                return Ok(newOrder);
            }  
        }
    }
}
