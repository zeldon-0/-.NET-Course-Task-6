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
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        private IOrderService _orderService;

        public ProductController(IProductService productService, IOrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts()
        {
            var result =  _productService.GetAll();
            return  Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProduct(int id)
        {
            var product =  _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("{id}")]
        public IActionResult PutProduct(ProductDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var prod = _productService.GetById(product.Id);
            if(prod == null)
            {
                return NotFound();
            }
            _productService.Update(product);


            return NoContent();
        }

        [HttpPost]
        public ActionResult<ProductDTO> PostProduct(ProductDTO product)
        {
            if (!ModelState.IsValid || product.Id != 0)
            {
                return BadRequest();
            }
            var newProduct =  _productService.Create(product);

            return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> DeleteProduct(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _productService.Delete(id);            

            return NoContent();
        }
    }
}
